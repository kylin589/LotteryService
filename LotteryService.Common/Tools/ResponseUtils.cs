using System;
using LotteryService.Common.Enums;

namespace LotteryService.Common.Tools
{
    public static class ResponseUtils
    {
        /// <summary>
        /// 异常时调用
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns></returns>
        public static ResultMessage<T> ErrorResult<T>(Exception ex)
        {
            return new ResultMessage<T>(ex);
        }

        /// <summary>
        /// 异常时调用
        /// </summary>
        /// <param name="errMsg">错误信息</param>
        /// <returns></returns>
        public static ResultMessage<T> ErrorResult<T>(string errMsg)
        {
            return new ResultMessage<T>(ResultCode.ServiceError,errMsg);
        }

        /// <summary>
        /// 返回数据时调用
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static ResultMessage<T> DataResult<T>(T data)
        {
            return new ResultMessage<T>(data);
        }

     


    }
}
