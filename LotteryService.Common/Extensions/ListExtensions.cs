using System.Collections.Generic;

namespace LotteryService.Common.Extensions
{
    public static class ListExtensions
    {
        public static string ToSplitString<T>(this IList<T> list, char separator)
        {
            string strLine = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                if (i < list.Count - 1)
                {
                    strLine += item.ToString() + separator;
                }
                else
                {
                    strLine += item.ToString();
                }

            }
            return strLine;
        }
    }
}