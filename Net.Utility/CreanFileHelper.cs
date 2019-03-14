// 定时删除文件

using DwrUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class CreanFileHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class ParamModel
    {
        /// <summary>
        /// 文件创建后超过多长时间做处理
        /// </summary>
        public TimeSpan DeleteTime { get; set; } = TimeSpan.FromDays(30);
        /// <summary>
        /// 处理的文件夹集合
        /// </summary>
        public List<string> Directories { get; set; } = new List<string>();
        /// <summary>
        /// 日志
        /// </summary>
        public Action<Exception> Log { get; set; }
        /// <summary>
        /// 定时器Timer参数：延时启动，默认TimeSpan.Zero立即启动
        /// </summary>
        public TimeSpan DueTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// 定时器Timer参数：时间间隔，默认一小时
        /// </summary>
        public TimeSpan Period { get; set; } = TimeSpan.FromHours(1);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    public static void Start(ParamModel model)
    {
        #region 检查参数
        if (model == null)
        {
            throw new Exception("没有传参");
        }

        if (model.Directories == null || model.Directories.Count == 0)
        {
            model.Log?.Invoke(new Exception("没有设置文件夹路径"));
            return;
        }

        var dirs = new List<string>();
        foreach (var dir in model.Directories)
        {
            if (Directory.Exists(dir))
            {
                dirs.Add(dir);
            }
            else
            {
                model.Log?.Invoke(new Exception($"文件夹路径设置有误：{dir}"));
            }
        }

        if (dirs.Count == 0)
        {
            return;
        }

        model.Directories = dirs;
        #endregion

        Timer timer = null;
        try
        {
            timer = new Timer(state =>
            {
                var obj = (ParamModel)state;
                Executes(obj);
            }, model, model.DueTime, model.Period);
        }
        catch (Exception ex)
        {
            model.Log?.Invoke(ex);
            timer?.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    private static void Executes(ParamModel model)
    {
        try
        {
            model.Directories.ForEach(dir =>
            {
                Execute(model, dir);
            });
        }
        catch (Exception ex)
        {
            model.Log?.Invoke(ex);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="dir"></param>
    private static void Execute(ParamModel model, string dir)
    {
        try
        {
            var dirs = new List<string>();
            var files = new List<string>();
            DirectoryHelper.GetDirectoryFiles(dir, ref dirs, ref files);

            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                var dt = fi.CreationTime;

                var tp = (DateTime.Now - dt);
                if (tp <= model.DeleteTime)
                {
                    continue;
                }

                if (!DirectoryHelper.DeleteFile(file))
                {
                    model.Log?.Invoke(new Exception($"文件删除失败：{file}"));
                }
            }
        }
        catch (Exception ex)
        {
            model.Log?.Invoke(ex);
        }
    }
}