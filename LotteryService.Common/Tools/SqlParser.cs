using System.Text;
using LotteryService.Common.Enums;

namespace LotteryService.Common.Tools
{
    public class SqlParser
    {
        public static string PageSql(string tableName, string whereStr,string primaryKey, string orderFiled,int pageSize,int currentPageIndex, OrderType orderType)
        {
            var sqlStr = $"SELECT TOP {pageSize} * FROM {tableName} WHERE {primaryKey} NOT IN (SELECT TOP {(currentPageIndex - 1) * pageSize} {primaryKey}" +
                         $" FROM {tableName} WHERE 1=1 {whereStr} ORDER BY {orderFiled} {orderType}) ORDER BY {orderFiled} {orderType}"; 
      
            return sqlStr;
        }

        public static string TotalCount(string tableName, string whereStr)
        {
            var sqlStr = $"SELECT COUNT(*) FROM {tableName} WHERE 1=1 {whereStr}";

            return sqlStr;
        }
    }
}