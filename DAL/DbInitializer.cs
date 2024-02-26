using SodaSeller.Models;

namespace SodaSeller.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SodaSellerContext sellerContext)
        {
            sellerContext.Database.EnsureCreated();

            if (!sellerContext.SodaProducts.Any())
            {
                var products = new SodaProducts[]
                {
                new SodaProducts{ProductName = "Coke", ProductPrice = 30, ProductCount = 15, ProductImage = "https://fikiwiki.com/uploads/posts/2022-02/1644920290_22-fikiwiki-com-p-krasivie-kartinki-koka-kola-25.jpg"},
                new SodaProducts{ProductName = "Pepsi", ProductPrice = 10, ProductCount = 10, ProductImage = "http://images.techinsider.ru/upload/img_cache/820/8205995aeb7d60c517275ae0a1241f1f_ce_1280x852x0x125_cropped_1332x888.jpg"},
                new SodaProducts{ProductName = "KVAS", ProductPrice = 50, ProductCount = 5, ProductImage = "https://klike.net/uploads/posts/2023-02/1677222058_3-95.jpg"},
                new SodaProducts{ProductName = "Mountain Dew", ProductPrice = 5, ProductCount = 20, ProductImage = "https://sun1-92.userapi.com/s/v1/ig2/KRd0cO7Ls5tm0YxXZYvlzhaJ5hf6cluE2LDZQYXneACjYR2ETemLZyazhUJ2Jv0q_Rcdm8MQgjMzYmyQqjSjEjDJ.jpg?size=632x632&quality=95&crop=88,0,632,632&ava=1"},
                };

                foreach (var product in products)
                    sellerContext.SodaProducts.Add(product);
            }

            if (!sellerContext.MachineCoins.Any())
            {
                var coins = new MachineCoins[]
                {
                    new MachineCoins{Name="1ruble", Count=50, CoinValue = 1, IsLocked = false, ImageLink = "https://thumbs.dreamstime.com/b/одна-монетка-рубля-изолированная-в-белой-предпосылке-русская-149367856.jpg"},
                    new MachineCoins{Name="2ruble", Count=50, CoinValue = 2, IsLocked = false, ImageLink = "https://ru-moneta.ru/upload/monety-21/2021-02rub-r42.jpg"},
                    new MachineCoins{Name="5ruble", Count=50, CoinValue = 5, IsLocked = false, ImageLink = "https://sun9-23.userapi.com/impg/Cb-sL8aoVmmfvC8cV7tefi42YxuqALTnE3hsaQ/LzJAqzYPxOY.jpg?size=807x807&quality=96&sign=22ba79fca745bd84b1e0b2090453fc62&c_uniq_tag=B0IJvcwGy3Yr6FM6xPrfuKypBjIL-7_uaR-qKX1cJ_o&type=album"},
                    new MachineCoins{Name="10ruble", Count=50, CoinValue = 10, IsLocked = false, ImageLink = "https://imperial-mag.ru/upload/shop_3/2/1/2/item_21255/item_image21255.jpg"},
                };
                foreach (var coin in coins)
                {
                    sellerContext.MachineCoins.Add(coin);
                }
            }
            sellerContext.SaveChanges();
        }
    }
}
