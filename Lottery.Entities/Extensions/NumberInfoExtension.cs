using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotteryService.Common.Enums;

namespace Lottery.Entities
{
    public static class NumberInfoExtension
    {
        public static int ComputeCriticalValue(this NumberInfo numberInfo, ThreeRegion threeRegion)
        {
            var countNumber = numberInfo.MaxValue - numberInfo.MinValue + 1;
            var part = (int)Math.Floor((decimal)countNumber / 3);
            int criticalValue;
            switch (threeRegion)
            {
                case ThreeRegion.FirstRegion:
                    criticalValue = part;
                    break;
                case ThreeRegion.SecondRegion:
                    criticalValue = part * 2;
                    break;
                case ThreeRegion.ThirdRegion:
                    criticalValue = numberInfo.MaxValue;
                    break;
                default:
                    throw new Exception("Not through this branch ");
            }
            return criticalValue;
        }
    }
}
