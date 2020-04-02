using Corteva.ScratchAndFortune.Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Corteva.ScratchAndFortune.Demo.Services
{
    public interface IRewardService
    {
        void InitReward();
        Task<string> FindRewardByVoucher(string voucher);
        Task<List<Reward>> GetRewards();
    }
}
