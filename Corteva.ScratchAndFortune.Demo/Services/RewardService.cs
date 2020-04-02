using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corteva.ScratchAndFortune.Demo.Models;

namespace Corteva.ScratchAndFortune.Demo.Services
{
    public class RewardService : IRewardService
    {
        public RewardService()
        {
            if (_rewards.Count == 0)
            {
                InitReward();
            }
        }
        private const string ZonkCode = "00000000-0000-0000-0000-000000000000";
        private const string UsedCode = "11111111-1111-1111-1111-111111111111";
        private static List<Reward> _rewards = new List<Reward>();
        public Task<string> FindRewardByVoucher(string voucher)
        {
            string rewardCode = string.Empty;
            foreach (Reward reward in _rewards)
            {
                if (reward.Vouchers.Any(c => c.VoucherCode.Equals(voucher)))
                {
                    Voucher voucherItem = reward.Vouchers.First(c => c.VoucherCode.Equals(voucher));
                    rewardCode = reward.Id;
                    reward.UsedVouchers.Add(voucherItem);
                    reward.Vouchers.Remove(voucherItem);
                    break;
                }
                else if (reward.UsedVouchers.Any(c => c.VoucherCode.Equals(voucher)))
                {
                    rewardCode = UsedCode;
                    break;
                }
            }
            if (string.IsNullOrEmpty(rewardCode))
            {
                rewardCode = ZonkCode;
            }
            return Task.FromResult(rewardCode);
        }

        public Task<List<Reward>> GetRewards()
        {
            return Task.FromResult(_rewards);
        }

        public void InitReward()
        {
            _rewards.Clear();
            _rewards.AddRange(new[]
            {
                new Reward
                {
                    RewardName = "Prize 1",
                    ColorHex = "#f44242",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("089CD-E6309-DF495-99758F"),
                        new Voucher("6BA38-49CCC-4C435-D81B23"),
                        new Voucher("784B9-12BEC-32420-89B6BE")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 2",
                    ColorHex = "#ab3434",

                    Vouchers = new List<Voucher>
                    {
                        new Voucher("221D3-D2F93-E4475-6873EA"),
                        new Voucher("4D3BC-CA89A-32442-DA15B7"),
                        new Voucher("08796-2094E-554F3-EB1D84")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 3",
                    ColorHex = "#ed5565",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("C07EE-2D802-654C4-28321F"),
                        new Voucher("4BC87-1117B-EA418-AA095E"),
                        new Voucher("F16A3-A0B82-2C40B-AAB5B2")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 4",
                    ColorHex = "#fba257",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("8A9AC-B0F08-2E46A-9AF781"),
                        new Voucher("78F92-C235D-7C465-E8BE2B"),
                        new Voucher("A3CA0-A4562-9E495-3BA3BE")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 5",
                    ColorHex = "#f7d95c",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("9E1AC-12E63-25434-5B2381"),
                        new Voucher("1B2EB-44C34-B2475-BBE07B"),
                        new Voucher("23BB7-A2280-9A415-790964")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 6",
                    ColorHex = "#23c6c8",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("069D1-3B059-484BF-DB0E82"),
                        new Voucher("5D715-5CFD5-DB488-1B2C50"),
                        new Voucher("73B43-4F0FC-2B40B-3A8FB7")
                    }
                },
                new Reward
                {
                    RewardName = "Prize 7",
                    ColorHex = "#4874bf",
                    Vouchers = new List<Voucher>
                    {
                        new Voucher("4395E-6B323-FC45E-6B207B"),
                        new Voucher("DEAB9-628D4-1E474-593304"),
                        new Voucher("7CB84-7C788-AB4C7-98C8BC")
                    }
                },
                new Reward
                {
                    Id = "00000000-0000-0000-0000-000000000000",
                    RewardName = "Zonk",
                    ColorHex = "#0e367c",
                    Vouchers = new List<Voucher>()
                }
            });

        }
    }
}
