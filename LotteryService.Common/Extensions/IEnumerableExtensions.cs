using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotteryService.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static string ToSplitString(this IEnumerable<object> objects, string split = ",")
        {
            var sb = new StringBuilder();
            foreach (var item in objects)
            {
                sb.Append(item.ToString() + split);
            }
            sb.Remove(sb.Length - 1,1);
            return sb.ToString();
        }
    }
}