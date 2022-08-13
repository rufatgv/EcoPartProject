using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class FaqsController : Controller
    {
        private readonly EcoPartsDbContext db;

        public FaqsController(EcoPartsDbContext db)
        {
            this.db = db;
        }


        [Authorize(Policy = "admin.faqs.index")]
        public async Task<IActionResult> Index()
        {
            return View(await db.Faqs.ToListAsync());
        }


        [Authorize(Policy = "admin.faqs.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }


        [Authorize(Policy = "admin.faqs.create")]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Policy = "admin.faqs.create")]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Add(faq);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }


        [Authorize(Policy = "admin.faqs.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize(Policy = "admin.faqs.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(faq);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }


        [Authorize(Policy = "admin.faqs.delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

   
        [HttpPost]

        [Authorize(Policy = "admin.faqs.delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            var entity = db.Faqs.FirstOrDefault(b => b.Id == id);
            if (entity == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Movcud Deyil"
                });
            }
            db.Faqs.Remove(entity);
            db.SaveChanges();
            return Json(new
            {
                error = false,
                message = "Ugurla Silindi"
            });

        }
        private bool FaqExists(int id)
        {
            return db.Faqs.Any(e => e.Id == id);
        }
    }
}
