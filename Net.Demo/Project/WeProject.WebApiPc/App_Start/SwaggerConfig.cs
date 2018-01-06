using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebActivatorEx;
using WeProject.WebApiPc;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WeProject.WebApiPc
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalHttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter { name = "Id", @in = "header", description = "Id", required = false, type = "string", @default = "0" });
        }
    }

    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "WeProject.WebAPI");
                    c.IncludeXmlComments(GetXmlCommentsPath(thisAssembly.GetName().Name));
                    c.IncludeXmlComments(GetXmlCommentsPath("WeProject.Entity"));

                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    c.SchemaId(t => t.FullName.Contains('`') ? t.FullName.Substring(0, t.FullName.IndexOf('`')) : t.FullName);

                    c.IgnoreObsoleteProperties();

                    c.DescribeAllEnumsAsStrings();

                    c.OperationFilter<GlobalHttpHeaderFilter>();


                })
                .EnableSwaggerUi(c =>
                {
                });
        }

        private static string GetXmlCommentsPath(string fileName)
        {
            return string.Format("{0}/bin/{1}.XML", System.AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
    }
}
