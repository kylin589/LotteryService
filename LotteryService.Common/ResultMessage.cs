using LotteryService.Common.Enums;

namespace LotteryService.Common
{
    /// <summary>
    /// 请求到的消息
    /// </summary>
    /// <typeparam name="TData">返回的消息数据</typeparam>
    public class ResultMessage<TData>
    //   where TData : class 
    {

        public ResultMessage()
        {
            Code = ResultCode.Fail;
            Msg = "Fail";
        }

        /// <summary>
        /// 正确返回结果集时使用
        /// </summary>
        /// <param name="data"></param>
        public ResultMessage(TData data)
        {
            Code = ResultCode.Success;
            Msg = "Success";
            Data = data;
        }

        /// <summary>
        /// 正确返回结果集，切附带消息时调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="msg"></param>
        public ResultMessage(TData data, string msg)
            : this(data)
        {
            Msg = msg;
        }


        /// <summary>  
        /// 有错误时,或是无消息数据返回时调用 
        /// </summary>  
        /// <param name="code"></param>  
        /// <param name="msg"></param>  
        public ResultMessage(ResultCode code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        public ResultMessage(ResultCode code, string msg, TData data)
            : this(code, msg)
        {
            Data = data;
        }


        /// <summary>  
        /// 异常时使用  
        /// </summary>  
        /// <param name="ex"></param>  
        public ResultMessage(System.Exception ex)
        {
            if (null != ex)
            {
                this.Code = ResultCode.ServiceError;
                this.Msg = ex.Message;
            }
        }

        /// <summary>    
        ///  附加数据
        /// </summary>    
        public TData Data { get; set; }

        /// <summary>    
        /// 消息码  
        /// </summary>
        /// <remarks>
        /// 200 : 成功
        /// 400 : 失败 
        /// 500 :  服务器内部错误   
        /// </remarks>    
        public ResultCode Code { get; set; }

        /// <summary>    
        /// 消息描述    
        /// </summary>    
        public string Msg { get; set; }

    }
}