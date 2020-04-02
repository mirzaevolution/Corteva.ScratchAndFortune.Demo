using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corteva.ScratchAndFortune.Demo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Corteva.ScratchAndFortune.Demo.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index(string voucher = "")
        {
            return View(new DataProfileViewModel
            {
                VoucherCode = voucher
            });
        }
        [HttpPost]
        public IActionResult Index(DataProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Trace.WriteLine(model);
                string voucher = model.VoucherCode;
                string base64Voucher = Convert.ToBase64String(Encoding.UTF8.GetBytes(voucher));
                return RedirectToAction("Index", "Spinner", new { q = base64Voucher });
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult GetCities(string provinceId)
        {
            List<City> list = new List<City>();
            switch (provinceId?.ToUpper())
            {
                case "P-JB":
                    {
                        list = new List<City>
                        {
                            new City
                            {
                                ProvinceId = "P-JB",
                                Id = "C-BDG",
                                Name = "Bandung"
                            },
                            new City
                            {
                                ProvinceId = "P-JB",
                                Id = "C-BGR",
                                Name = "Bogor"
                            }
                        };
                        return Ok(new { cities = list });
                    }
                case "P-LPG":
                    {
                        list = new List<City>
                        {
                            new City
                            {
                                ProvinceId = "P-LPG",
                                Id = "C-BDL",
                                Name = "Bandar Lampung"
                            },
                            new City
                            {
                                ProvinceId = "P-LPG",
                                Id = "C-MTR",
                                Name = "Kota Metro"
                            }
                        };
                        return Ok(new { cities = list });
                    }
                default:
                    {
                        return Ok(new { cities = list });
                    }
            }
        }

        [HttpPost]
        public IActionResult GetStores(string cityId)
        {
            List<Store> stores = new List<Store>();
            switch (cityId)
            {
                case "C-BDG":
                    {
                        stores = new List<Store>
                        {
                            new Store
                            {
                                CityId = "C-BDG",
                                Name = "Toko Makmur Jaya"
                            },
                            new Store
                            {
                                CityId = "C-BDG",
                                Name = "Toko Abadi"
                            }
                        };
                        return Ok(new { stores });
                    }
                case "C-BGR":
                    {
                        stores = new List<Store>
                        {
                            new Store
                            {
                                CityId = "C-BGR",
                                Name = "Toko Pupuk Maju Jaya"
                            }
                        };
                        return Ok(new { stores });
                    }
                case "C-BDL":
                    {
                        stores = new List<Store>
                        {
                            new Store
                            {
                                CityId = "C-BDL",
                                Name = "Toko Pupuk Pak Jono"
                            },
                            new Store
                            {
                                CityId = "C-BDL",
                                Name = "Toko Lengkap Bu Desi"
                            }
                        };
                        return Ok(new { stores });
                    }
                case "C-MTR":
                    {
                        stores = new List<Store>
                        {
                            new Store
                            {
                                CityId = "C-MTR",
                                Name = "Toko Pupuk Ilir 5"
                            },
                            new Store
                            {
                                CityId = "C-MTR",
                                Name = "Toko Pupuk Bagus Asri"
                            }
                        };
                        return Ok(new { stores });
                    }
                default:
                    {
                        return Ok(new { stores });
                    }
            }

        }
    }
}