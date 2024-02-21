using SodaSeller.Models;

namespace SodaSeller.DAL
{
    public class DbInitializer
    {
        public static void Initialize(SodaProductContext productContext)
        {
            productContext.Database.EnsureCreated();

            if (productContext.SodaProducts.Any())
                return;
            var products = new SodaProducts[]
            {
                new SodaProducts{ProductName = "Coke", ProductPrice = 30, ProductCount = 15, ProductImage = "https://fikiwiki.com/uploads/posts/2022-02/1644920290_22-fikiwiki-com-p-krasivie-kartinki-koka-kola-25.jpg"},
                new SodaProducts{ProductName = "Pepsi", ProductPrice = 10, ProductCount = 10, ProductImage = "http://images.techinsider.ru/upload/img_cache/820/8205995aeb7d60c517275ae0a1241f1f_ce_1280x852x0x125_cropped_1332x888.jpg"},
                new SodaProducts{ProductName = "KVAS", ProductPrice = 50, ProductCount = 5, ProductImage = "https://klike.net/uploads/posts/2023-02/1677222058_3-95.jpg"},
                new SodaProducts{ProductName = "Mountain Dew", ProductPrice = 5, ProductCount = 20, ProductImage = "https://sun1-92.userapi.com/s/v1/ig2/KRd0cO7Ls5tm0YxXZYvlzhaJ5hf6cluE2LDZQYXneACjYR2ETemLZyazhUJ2Jv0q_Rcdm8MQgjMzYmyQqjSjEjDJ.jpg?size=632x632&quality=95&crop=88,0,632,632&ava=1"},
            };
            foreach (var product in products)
                productContext.SodaProducts.Add(product);
            productContext.SaveChanges();
        }
    }
}
