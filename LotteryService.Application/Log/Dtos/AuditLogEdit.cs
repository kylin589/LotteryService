
namespace LotteryService.Application.Log.Dtos
{
    public class AuditLogEdit : IDto
    {
        public string Id { get; set; }

        public int ExecutionDuration { get; set; }

        public string Exception { get; set; }

        public bool IsExecSuccess { get; set; }
    }
}