using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class SlipsPageModel
    {
        public IEnumerable<Slip> Slips { get; set; } = new List<Slip>();

        public List<Dock> Docks { get; set; } = new List<Dock>();

        public SlipFormModel Form { get; set; } = new SlipFormModel();
    }
}