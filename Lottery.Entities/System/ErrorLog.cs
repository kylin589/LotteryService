using System;
using System.ComponentModel.DataAnnotations.Schema;
using Lottery.Entities.Extend;

namespace Lottery.Entities
{
    [Table("ErrorLogs",Schema = Constant.SystemSchema)]
    public class ErrorLog : BaseEntity
    {

        public int? UserId { get; set; }

        public string Thread { get; set; }

        public string Level { get; set; }

        public string OperationType { get; set; }

        public string IP { get; set; }

        public DateTime CreateTime { get; set; }

        public string Logger { get; set; }

        public string MethodName { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }


    }
}
