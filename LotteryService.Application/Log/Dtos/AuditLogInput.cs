
namespace LotteryService.Application.Log.Dtos
{
    public class AuditLogInput : IDto
    {
        public string UserId { get; set; }

        public string ApiAddress { get; set; }

        public string MethodName { get; set; }

        public string Parameters { get; set; }

        public string ClientIpAddress { get; set; }

        public string ClientName { get; set; }

        public string BrowserInfo { get; set; }

        public string CustomData { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public bool IsExecSuccess { get; set; }

        public int ExecutionDuration { get; set; }

    }
}