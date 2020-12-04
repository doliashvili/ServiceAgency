using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceAgency.Api.Swagger
{
    public class LangHeaderRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "lang",
                Required = false,
                Schema = new OpenApiSchema()
                {
                    Type = "string",
                    Default = new OpenApiString("en")
                }
            });
        }
    }
}
