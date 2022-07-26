using EcoPart.Web.UI.AppCode.Providers;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Riode.WebUI.AppCode.Providers;

namespace EcoPart.Web.UI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(cfg => cfg.LowercaseUrls = true);
            services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());


                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));


            });

            services.AddDbContext<EcoPartsDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            });

            services.AddIdentity<EcoPartsUser, EcoPartsRole>()
                .AddEntityFrameworkStores<EcoPartsDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 3;
                cfg.SignIn.RequireConfirmedAccount = true;
                cfg.SignIn.RequireConfirmedEmail = true;
                cfg.User.RequireUniqueEmail = true;
                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.Lockout.DefaultLockoutTimeSpan = new System.TimeSpan(0, 3, 0);

            });

            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";

                cfg.ExpireTimeSpan = new System.TimeSpan(240, 24, 5, 22);
                cfg.Cookie.Name = "CookieWellCookedNiceDrilYeaaaaaaaaaaaaah";
            });

            services.AddAuthentication();
            services.AddAuthorization(cfg =>
            {
                foreach (var policyName in Program.principals)
                {
                    cfg.AddPolicy(policyName, p =>
                    {

                        p.RequireAssertion(handler =>
                        {
                            return handler.User.IsInRole("SuperAdmin")
                            || handler.User.HasClaim(policyName, "1");
                        });

                    });
                }

            });

            services.AddMediatR(this.GetType().Assembly);
            //services.AddFluentValidation(cfg =>
            //{
            //    cfg.RegisterValidatorsFromAssemblies(new[] { this.GetType().Assembly });
            //});
            services.AddScoped<UserManager<EcoPartsUser>>();
            services.AddScoped<SignInManager<EcoPartsUser>>();
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IClaimsTransformation, AppClaimProvider>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(cfg =>
            {


                cfg.MapAreaControllerRoute(
                    name: "defaultAdmin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}"
             );
                cfg.MapControllerRoute("default", pattern: "{controller=preloader}/{action=index}/{id?}");


            });
        }
    }
}
