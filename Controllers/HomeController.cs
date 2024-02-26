using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaSeller.DAL;
using SodaSeller.Models;
using SodaSeller.ViewModels;
using System.Diagnostics;

namespace SodaSeller.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SodaSellerContext _productContext;
        private readonly PaymentManager _paymentManager;
        public HomeController(ILogger<HomeController> logger, SodaSellerContext context, PaymentManager paymentManager)
        {
            _logger = logger;
            _productContext = context;
            _paymentManager = paymentManager;
        }

        public async Task<IActionResult> Index(int? changeSum)
        {
            if (changeSum.HasValue)
            {
                ViewBag.changeSum = changeSum;
                if (changeSum.Value > 0)
                    ViewBag.changeCoins = await _paymentManager.GetChangeCoinsFromMachine(changeSum.Value);
            }

            var viewModel = await FormViewModel();

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> BuyProduct(int? productId, int oneRubles, int twoRubles, int fiveRubles, int tenRubles)
        {
            if (productId == null)
                return NotFound();
            var insertedCoins = FormDictionaryFromValues(oneRubles, twoRubles, fiveRubles, tenRubles);
            int changeSum = 0;
            try
            {
                var purchasedProduct = await _productContext.SodaProducts.FirstOrDefaultAsync(m => m.Id == productId);
                if (purchasedProduct != null)
                    if (purchasedProduct.ProductCount > 0)
                    {
                        changeSum = await _paymentManager.MakePayment(insertedCoins, purchasedProduct.ProductPrice);
                        if (changeSum < 0)
                            return RedirectToAction("Index", new { changeSum });
                        purchasedProduct.ProductCount--;
                    }
                _productContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction("Index", new { changeSum });

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
        private Dictionary<string, int> FormDictionaryFromValues(int oneRubles, int twoRubles, int fiveRubles, int tenRubles)
        {
            var insertedCoins = new Dictionary<string, int>()
            {
                {"1ruble", oneRubles },
                {"2ruble", twoRubles },
                {"5ruble", fiveRubles },
                {"10ruble", tenRubles },
            };
            return insertedCoins;
        }
        private async Task<IndexViewModel> FormViewModel()
        {
            List<SodaProducts> productsModel = await _productContext.SodaProducts.ToListAsync();
            List<MachineCoins> machineCoins = await _productContext.MachineCoins.ToListAsync();
            IndexViewModel viewModel = new() { SodaProducts = productsModel, MachineCoins = machineCoins };
            return viewModel;
        }
    }
}
