using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace LotteryService.WebApi
{
    
    public sealed class EncryptAuditLogParamsAttribute : Attribute,IFilter
    {
        
        public bool AllowMultiple {
            get { return true; }
        }
    }
}