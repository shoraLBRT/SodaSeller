using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaSeller.DAL;
using SodaSeller.Models;

namespace SodaSeller.Controllers
{
    public class MachineCoinsController : Controller
    {
        private readonly SodaSellerContext _context;

        public MachineCoinsController(SodaSellerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MachineCoins.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineCoins = await _context.MachineCoins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (machineCoins == null)
            {
                return NotFound();
            }

            return View(machineCoins);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var machineCoins = await _context.MachineCoins.FindAsync(id);
            if (machineCoins == null)
            {
                return NotFound();
            }
            return View(machineCoins);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CoinValue,Count,IsLocked,ImageLink")] MachineCoins machineCoins)
        {
            if (id != machineCoins.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(machineCoins);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MachineCoinsExists(machineCoins.Id))
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
            return View(machineCoins);
        }

        private bool MachineCoinsExists(int id)
        {
            return _context.MachineCoins.Any(e => e.Id == id);
        }
    }
}
