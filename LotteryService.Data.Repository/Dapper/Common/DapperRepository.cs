using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LotteryService.Data.Repository.Dapper.Common
{
    public class DapperRepository: IDisposable
    {
        public IDbConnection LotteryDbConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
            }
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}