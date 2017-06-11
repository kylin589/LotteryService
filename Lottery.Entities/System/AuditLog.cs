using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;

namespace Lottery.Entities
{
    [Table("AuditLogs")]
    public class AuditLog : BaseEntity
    {
        public AuditLog() : base()
        {
            ExecutionTime = DateTime.Now;
        }

        public string UserId { get; set; }

        public string AccountName { get; set; }

        [MaxLength(50)]
        public string ApiAddress { get; set; }

        [MaxLength(10)]
        public string MethodName { get; set; }

        [MaxLength(500)]
        public string Parameters { get; set; }

        [Required]
        public DateTime ExecutionTime { get; set; }

        public int? ExecutionDuration { get; set; }

        public string ClientIpAddress { get; set; }

        [MaxLength(100)]
        public string ClientName { get; set; }

        [MaxLength(100)]
        public string BrowserInfo { get; set; }

        public string Exception { get; set; }

        [MaxLength(500)]
        public string CustomData { get; set; }

        [MaxLength(50)]
        public string ActionName { get; set; }

        [MaxLength(50)]
        public string ControllerName { get; set; }

        public bool? IsExecSuccess { get; set; }

    }
}
