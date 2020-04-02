using System.ComponentModel.DataAnnotations;

namespace Corteva.ScratchAndFortune.Demo.Models
{
    public class CheckRewardViewModel
    {
        [Required]
        public string Voucher { get; set; }
    }
}
