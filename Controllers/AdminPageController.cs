using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaSeller.DAL;
using SodaSeller.Models;

namespace SodaSeller.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly SodaSellerContext _context;

        public AdminPageController(SodaSellerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.SodaProducts.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductPrice,ProductCount,ProductImage")] SodaProducts sodaProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sodaProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sodaProducts);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sodaProducts = await _context.SodaProducts.FindAsync(id);
            if (sodaProducts == null)
            {
                return NotFound();
            }
            return View(sodaProducts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice,ProductCount,ProductImage")] SodaProducts sodaProducts)
        {
            if (id != sodaProducts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sodaProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SodaProductsExists(sodaProducts.Id))
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
            return View(sodaProducts);
        }

        public async Task<IActionResult> RefillProducts()
        {
            foreach (var item in _context.SodaProducts)
            {
                item.ProductCount = 15;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sodaProducts = await _context.SodaProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sodaProducts == null)
            {
                return NotFound();
            }

            return View(sodaProducts);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sodaProducts = await _context.SodaProducts.FindAsync(id);
            if (sodaProducts != null)
            {
                _context.SodaProducts.Remove(sodaProducts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SodaProductsExists(int id)
        {
            return _context.SodaProducts.Any(e => e.Id == id);
        }
    }
}
