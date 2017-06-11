using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using LotteryService.Common;

namespace LotteryService.WebApi
{
    class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {     
            var allowAnonymous = Enumerable.Any(apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>());

            if (!allowAnonymous)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();
                operation.parameters.Add(new Parameter
                {
                    name = LsConstant.LOTTERY_SERVICE_TICKET,
                    @in = "header",
                    description = "access token",
                    required = true,
                    type = "string"
                });
            }

        }
    }

}