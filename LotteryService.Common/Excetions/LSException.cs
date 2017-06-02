using System;

namespace LotteryService.Common.Excetions
{
    public class LSException : Exception
    {
        private LSException() : base()
        {
        }

        public LSException(string exMsg) : base(exMsg)
        {
        }
        public LSException(string exMsg,Exception ex) : base(exMsg, ex)
        {
        }

        public LSException(Exception ex) : base(ex.Message,ex)
        {
        }
    }
}