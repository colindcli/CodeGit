using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenerateCodeByT4
{
    public class CustomCmdLineHost : ITextTemplatingEngineHost, ITextTemplatingSessionHost
    {
        public string TemplateFileValue;
        public string TemplateFile => TemplateFileValue;

        public string FileExtensionValue = ".txt";
        public string FileExtension => FileExtensionValue;

        public Encoding FileEncodingValue = Encoding.UTF8;
        public Encoding FileEncoding => FileEncodingValue;

        public CompilerErrorCollection Errors => ErrorsValue;

        public IList<string> StandardAssemblyReferences => new[]
        {
            typeof(Uri).Assembly.Location
        };

        public IList<string> StandardImports => new[]
        {
            "System"
        };

        public ITextTemplatingSession Session { get; set; }
        public CompilerErrorCollection ErrorsValue { get; set; }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;

            if (!File.Exists(requestFileName))
                return false;
            content = File.ReadAllText(requestFileName);
            return true;

        }
        public object GetHostOption(string optionName)
        {
            object returnObject;
            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;
                default:
                    returnObject = null;
                    break;
            }
            return returnObject;
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            var candidate = Path.Combine(Path.GetDirectoryName(TemplateFile), assemblyReference);
            return File.Exists(candidate) ? candidate : "";
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            if (string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase) == 0)
            {
            }
            throw new Exception("Directive Processor not found");
        }
        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }
            if (File.Exists(fileName))
            {
                return fileName;
            }
            string candidate = Path.Combine(Path.GetDirectoryName(TemplateFile), fileName);
            if (File.Exists(candidate))
            {
                return candidate;
            }
            return fileName;
        }
        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return string.Empty;
        }
        public void SetFileExtension(string extension)
        {
            FileExtensionValue = extension;
        }
        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            FileEncodingValue = encoding;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            ErrorsValue = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public ITextTemplatingSession CreateSession()
        {
            throw new NotImplementedException();
        }
    }
}
