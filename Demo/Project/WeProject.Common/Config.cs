using System;
using System.Configuration;
using System.Linq;
using WeProject.Entity.Model;

namespace WeProject.Common
{
    public class Config
    {
        private static string GetValue(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }

        private static FtpServerModel _FtpServer { get; set; }
        public static FtpServerModel FtpServer()
        {
            if (_FtpServer != null)
            {
                return _FtpServer;
            }
            var server = GetValue("FtpServer");
            if (string.IsNullOrWhiteSpace(server))
            {
                LogHelper.Error("请配置FtpServer");
                return null;
            }
            var array = server.Split(';');
            if (array.Length < 4)
            {
                LogHelper.Error("FtpServer配置不正确");
                return null;
            }
            try
            {
                var m = new FtpServerModel()
                {
                    Ip = array[0],
                    Port = int.Parse(array[1]),
                    User = array[2],
                    Pwd = array[3]
                };
                _FtpServer = m;
                return _FtpServer;
            }
            catch (Exception ex)
            {
                LogHelper.Error("FtpServer配置不正确", ex);
                return null;
            }
        }

        private static ThumbnailsModel _thumbnailsModel { get; set; }

        public static ThumbnailsModel Thumbnails
        {
            get
            {
                if (_thumbnailsModel != null)
                {
                    return _thumbnailsModel;
                }

                var value = GetValue("Thumbnails");
                if (string.IsNullOrWhiteSpace(value))
                {
                    LogHelper.Error("请配置Thumbnails");
                    return null;
                }
                var array = value.Split(';');
                if (array.Length < 4)
                {
                    LogHelper.Error("Thumbnails配置有误");
                    return null;
                }
                try
                {
                    var m = new ThumbnailsModel()
                    {
                        BigWidth = int.Parse(array[0]),
                        BigHeight = int.Parse(array[1]),
                        SmallWidth = int.Parse(array[2]),
                        SmallHeight = int.Parse(array[3])
                    };
                    _thumbnailsModel = m;
                    return _thumbnailsModel;
                }
                catch (Exception ex)
                {
                    LogHelper.Fatal("Thumbnails配置出错", ex);
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据扩展名判断是否是图片
        /// </summary>
        public static string[] ImageExtensionNames = GetValue("ImageExtensionName").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(p => $".{p.ToLower()}").ToArray();
    }
}
