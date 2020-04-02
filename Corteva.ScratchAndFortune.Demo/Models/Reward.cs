using System;
using System.Collections.Generic;

namespace Corteva.ScratchAndFortune.Demo.Models
{
    public class Reward
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string RewardName { get; set; }
        public string ColorHex { get; set; }
        public List<Voucher> UsedVouchers { get; set; } = new List<Voucher>();
        public List<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
