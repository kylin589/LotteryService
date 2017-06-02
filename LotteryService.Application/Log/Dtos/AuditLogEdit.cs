using System;

namespace LotteryService.Application.Log.Dtos
{
    public class AuditLogEdit
    {
        public string Id { get; set; }

        public int ExecutionDuration { get; set; }

        public string Exception { get; set; }

        public bool IsExecSuccess { get; set; }
    }
}