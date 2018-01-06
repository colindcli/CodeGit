using System;
using System.Configuration;

namespace FileService.Common
{
    public class Config
    {
        private static string GetValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        private static string _FileRoot { get; set; }

        /// <summary>
        /// FTP根目录，后面没有“/”
        /// </summary>
        public static string FileRoot
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_FileRoot))
                {
                    return _FileRoot;
                }
                var fileRoot = GetValue("FileRoot");
                _FileRoot = (!string.IsNullOrWhiteSpace(fileRoot) ? fileRoot : AppDomain.CurrentDomain.BaseDirectory).TrimEnd('/', '\\');
                return _FileRoot;
            }
        }
    }
}
