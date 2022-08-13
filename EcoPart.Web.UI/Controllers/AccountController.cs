using EcoPart.Web.UI.AppCode.Extensions;
using EcoPart.Web.UI.Models.Entities.Membership;
using EcoPart.Web.UI.Models.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EcoParts.Web.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<EcoPartsUser> signInManager;
        private readonly UserManager<EcoPartsUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(SignInManager<EcoPartsUser> signInManager, UserManager<EcoPartsUser> userManager, IConfiguration configuration)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        
        public IActionResult SignIn()
        {



            return View();
        }

        [HttpGet]
        [Route("/register.html")]
        public IActionResult Register()
        {



            return View();
        }

        [Route("/logout.html")]
        public async Task<IActionResult> LogOut()
        {

            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "home");
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [Route("/register.html")]
        public async Task<IActionResult> Register(RegisterFormModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new EcoPartsUser();
                user.UserName = $"{model.Name}.{model.Surname}";
                user.Email = model.Email;
                user.Name = model.Name;
                user.Surname = model.Surname;



                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);



                    string path = $"{Request.Scheme}://{Request.Host}/registration-confirm.html?email={user.Email}&token={token}";

                    var emailResponse = configuration.SendEmail(user.Email, "Riode User Registration", $"Zehmet Olmasa <a href=\"{path}\">link</a> vasitesile qeydiyyati tamamlayasiniz ");

                    if (emailResponse)
                    {

                        ViewBag.Message = "Tebrikler qeydiyyat tamamlandi";
                    }
                    else
                    {

                        ViewBag.Message = "E-maile gondererken sehv ashkar olundu , yeniden cehd edin";
                    }


                    return RedirectToAction(nameof(SignIn));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }



            }

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(LoginFormModel user)
        {
            if (ModelState.IsValid)
            {
                EcoPartsUser foundeduser = null;

                if (user.UserName.IsEmail())
                {
                    foundeduser = await userManager.FindByEmailAsync(user.UserName);
                }

                else
                {
                    foundeduser = await userManager.FindByNameAsync(user.UserName);
                }

                if (foundeduser == null)
                {
                    ViewBag.Message = "Istifadechi adiniz ve ya shifreniz yalnishdir";

                    goto end;
                }

                var singInResult = await signInManager.PasswordSignInAsync(foundeduser, user.Password, true, true);

                if (!singInResult.Succeeded)
                {
                    ViewBag.Message = "Xaish olunur yeniden yoxlayin";
                }

                var callbackUrl = Request.Query["ReturnUrl"];

                if (!string.IsNullOrWhiteSpace(callbackUrl))
                {
                    return Redirect(callbackUrl);
                }

                return RedirectToAction("Index", "Home");
            }

        end:
            return View(user);
        }
        [Route("/registration-confirm.html")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterConfirm(string email, string token)
        {
            var foundedUser = await userManager.FindByEmailAsync(email);


            if (foundedUser == null)
            {
                ViewBag.Message = "Xetali Token";
                goto end;

            }

            token = token.Replace(" ", "+");

            var result = await userManager.ConfirmEmailAsync(foundedUser, token);

            if (!result.Succeeded)
            {
                ViewBag.Message = "Xeta!";
                goto end;
            }

            ViewBag.Message = "Hesabniz Tesdiglendi";

        end:
            return RedirectToAction(nameof(SignIn));
        }

        [Route("/accessdenied.html")]
        [AllowAnonymous]

        public IActionResult AccesDeny()
        {
            return View();
        }
    }
}
