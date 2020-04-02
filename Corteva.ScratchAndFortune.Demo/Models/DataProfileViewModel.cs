using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Corteva.ScratchAndFortune.Demo.Models
{
    public class DataProfileViewModel
    {
        [Required(ErrorMessage = "No Voucher harus diisi")]
        [Display(Name = "No. Voucher")]
        public string VoucherCode { get; set; }

        [Required(ErrorMessage = "Nama harus diisi")]
        [Display(Name = "Nama")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "No KTP harus diisi")]
        [Display(Name = "No. KTP")]
        public string IdentityCardNumber { get; set; }

        [Display(Name = "Propinsi")]
        public string Province { get; set; }

        [Display(Name = "Kota")]
        public string City { get; set; }

        [Required(ErrorMessage = "No HP harus diisi")]
        [Display(Name = "No. HP")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Toko")]
        public string Store { get; set; }
        public List<SelectListItem> Provinces { get; set; } = new List<SelectListItem>();

        public List<Product> SubmittedProducts { get; set; } = new List<Product>();

        private void InitData()
        {
            Provinces.Add(new SelectListItem
            {
                Value = "P-JB",
                Text = "Jawa Barat"
            });
            Provinces.Add(new SelectListItem
            {
                Value = "P-LPG",
                Text = "Lampung"
            });
        }
        public DataProfileViewModel()
        {
            InitData();
        }
    }

    public class Product
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
    public class Province
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class City
    {
        public string ProvinceId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Store
    {
        public string CityId { get; set; }
        public string Name { get; set; }
    }
}
