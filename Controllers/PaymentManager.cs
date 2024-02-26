using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SodaSeller.DAL;

namespace SodaSeller.Controllers
{
    public class PaymentManager : Controller
    {
        private readonly SodaSellerContext _context;


        public PaymentManager(SodaSellerContext context)
        {
            _context = context;
        }

        public async Task<int> MakePayment(Dictionary<string, int> insertedCoinsToPay, int productPrice)
        {
            int insertedBalance = SumOfInsertedCoins(insertedCoinsToPay);
            int changeSum = insertedBalance - productPrice;

            if (changeSum < 0)
                return changeSum;

            await InsertCoinsToMachine(insertedCoinsToPay);
            return changeSum;
        }
        public async Task<Dictionary<string, int>> GetChangeCoinsFromMachine(int changeSum)
        {
            var changeCoinsDictionary = CreateEmptyCoinsDictionary();

            var oneRublesCount = await _context.MachineCoins.FirstAsync(m => m.Name == "1ruble");
            var twoRublesCount = await _context.MachineCoins.FirstAsync(m => m.Name == "2ruble");
            var fiveRublesCount = await _context.MachineCoins.FirstAsync(m => m.Name == "5ruble");
            var tenRublesCount = await _context.MachineCoins.FirstAsync(m => m.Name == "10ruble");

            while (changeSum > 0)
            {
                if (changeSum >= 10 && tenRublesCount.Count > 1)
                {
                    changeSum -= 10;
                    tenRublesCount.Count--;
                    changeCoinsDictionary["10ruble"] = changeCoinsDictionary["10ruble"] + 1;
                    continue;
                }
                if (changeSum >= 5 && fiveRublesCount.Count > 1)
                {
                    changeSum -= 5;
                    fiveRublesCount.Count--;
                    changeCoinsDictionary["5ruble"] = changeCoinsDictionary["5ruble"] + 1;
                    continue;
                }
                if (changeSum >= 2 && twoRublesCount.Count > 1)
                {
                    changeSum -= 2;
                    twoRublesCount.Count--;
                    changeCoinsDictionary["2ruble"] = changeCoinsDictionary["2ruble"] + 1;
                    continue;
                }
                if (changeSum >= 1 && oneRublesCount.Count > 1)
                {
                    changeSum -= 1;
                    oneRublesCount.Count--;
                    changeCoinsDictionary["1ruble"] = changeCoinsDictionary["1ruble"] + 1;
                    continue;
                }
                return changeCoinsDictionary;
            }
            return changeCoinsDictionary;
        }

        private Dictionary<string, int> CreateEmptyCoinsDictionary()
        {
            var emptyCoinsDictioonary = new Dictionary<string, int>()
            {
                {"1ruble", 0 },
                {"2ruble", 0},
                {"5ruble", 0 },
                {"10ruble", 0 },
            };
            return emptyCoinsDictioonary;
        }
        private int SumOfInsertedCoins(Dictionary<string, int> insertedCoinsToPay)
        {
            int insertedBalance = 0;
            foreach (var coinType in insertedCoinsToPay)
            {
                switch (coinType.Key)
                {
                    case "1ruble":
                        insertedBalance += coinType.Value;
                        break;
                    case "2ruble":
                        insertedBalance += coinType.Value * 2;
                        break;
                    case "5ruble":
                        insertedBalance += coinType.Value * 5;
                        break;
                    case "10ruble":
                        insertedBalance += coinType.Value * 10;
                        break;
                }
            }
            return insertedBalance;
        }

        private async Task InsertCoinsToMachine(Dictionary<string, int> insertedCoins)
        {
            foreach (var typeOfInsertedCoin in insertedCoins)
            {
                var coinInMachine = await _context.MachineCoins.FirstAsync(m => m.Name == typeOfInsertedCoin.Key);
                coinInMachine.Count += typeOfInsertedCoin.Value;
            }
            await _context.SaveChangesAsync();
        }
    }
}
