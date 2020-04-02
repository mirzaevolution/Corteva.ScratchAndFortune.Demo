using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corteva.ScratchAndFortune.Demo.Models;
using Corteva.ScratchAndFortune.Demo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corteva.ScratchAndFortune.Demo.Controllers
{
    public class SpinnerController : Controller
    {
        private readonly IRewardService _rewardService;
        public SpinnerController(IRewardService rewardService)
        {
            _rewardService = rewardService;

        }
        public IActionResult Index(string q = "")
        {
            try
            {
                ViewBag.DecodedQ = Encoding.UTF8.GetString(Convert.FromBase64String(q));
            }
            catch { ViewBag.DecodedQ = ""; }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckReward(CheckRewardViewModel model)
        {
            bool success = true;
            string rewardCode = string.Empty;
            if (!ModelState.IsValid)
            {
                success = false;
                return Ok(new { success, rewardCode });
            }
            rewardCode = await _rewardService.FindRewardByVoucher(model.Voucher);
            return Ok(new { success, rewardCode });
        }
        [HttpPost]
        public async Task<IActionResult> GetRewards()
        {
            var rewards = await _rewardService.GetRewards();
            return Ok(new
            {
                rewards = rewards.Select(c => new { id = c.Id, name = c.RewardName, color = c.ColorHex })
            });
        }
        [HttpPost]
        public IActionResult ResetReward()
        {
            _rewardService.InitReward();
            return Ok(new { success = true });
        }

    }
}