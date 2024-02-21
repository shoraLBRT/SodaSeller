using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaSeller.DAL;
using SodaSeller.Models;
using System.Diagnostics;

namespace SodaSeller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SodaProductContext _productContext;

        public HomeController(ILogger<HomeController> logger, SodaProductContext context)
        {
            _logger = logger;
            _productContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productContext.SodaProducts.ToListAsync());
        }

        public async Task<IActionResult> BuyProduct(int? id)
        {
            if (id == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var purchasedProduct = await _productContext.SodaProducts.FirstOrDefaultAsync(m => m.Id == id);
                    if (purchasedProduct != null)
                        if (purchasedProduct.ProductCount > 0)
                            purchasedProduct.ProductCount--;
                    _productContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult AdminPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private bool SodaProductsExists(int id)
        {
            return _productContext.SodaProducts.Any(e => e.Id == id);
        }
    }
}
