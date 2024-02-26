namespace SodaSeller.Models
{
    public class MachineCoins
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CoinValue { get; set; }
        public required int Count { get; set; }
        public required bool IsLocked { get; set; }
        public required string ImageLink { get; set; }

    }
}
