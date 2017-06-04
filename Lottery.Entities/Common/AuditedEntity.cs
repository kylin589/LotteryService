using System;

namespace Lottery.Entities
{
    public abstract class AuditedEntity : BaseEntity
    {
        protected AuditedEntity()
        {
            CreatTime = DateTime.Now;
        }

        public DateTime CreatTime { get; set; }

        public DateTime? ModifyTime { get; set; }
    }
}