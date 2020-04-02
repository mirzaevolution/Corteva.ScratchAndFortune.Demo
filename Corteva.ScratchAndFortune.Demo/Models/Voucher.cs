using System;
namespace Corteva.ScratchAndFortune.Demo.Models
{
    public class Voucher
    {
        public Voucher() { }
        public Voucher(string voucherCode)
        {
            VoucherCode = voucherCode;
        }
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string VoucherCode { get; set; }
    }
}
