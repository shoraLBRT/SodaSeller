namespace SodaSeller.Models
{
    public class SodaProducts
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public string? ProductImage { get; set; }

    }
}
