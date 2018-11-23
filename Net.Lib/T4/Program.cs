using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace GenerateCodeByT4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var tableName = "Product";
            var templateFileName = $@"{AppDomain.CurrentDomain.BaseDirectory}GenerateCode.tt";
            var param = new TextTemplatingSession
                {
                    {"tableName", tableName},
                    {"varTableName", tableName.ToLower().First() + tableName.Substring(1)}
                };

            var host = new CustomCmdLineHost { TemplateFileValue = "test.tt" };
            var engine = new Engine();
            var input = File.ReadAllText(templateFileName);
            host.Session = param;
            var output = engine.ProcessTemplate(input, host);

            Console.WriteLine(output);
            foreach (CompilerError error in host.Errors)
            {
                Console.WriteLine(error.ToString());
            }
            Console.ReadLine();
        }
    }
}
