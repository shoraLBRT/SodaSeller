using SodaSeller.Models;

namespace SodaSeller.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<MachineCoins> MachineCoins { get; set; } = new List<MachineCoins>();
        public IEnumerable<SodaProducts> SodaProducts { get; set; } = new List<SodaProducts>();
    }
}
