using log4net;
using log4net.Repository;
using System;
using System.Diagnostics;
using System.Reflection;

public class LogHelper
{
    private static ILoggerRepository Repository => LogManager.GetRepository("NetCoreRepository");
    private static ILog _Fatallog { get; set; }
    private static ILog Fatallog
    {
        get
        {
            if (_Fatallog != null)
            {
                return _Fatallog;
            }

            _Fatallog = LogManager.GetLogger(Repository.Name, "Fatallog");
            return _Fatallog;
        }
    }
    private static ILog _Errorlog { get; set; }
    private static ILog Errorlog
    {
        get
        {
            if (_Errorlog != null)
            {
                return _Errorlog;
            }

            _Errorlog = LogManager.GetLogger(Repository.Name, "Errorlog");
            return _Errorlog;
        }
    }
    private static ILog _Warnlog { get; set; }
    private static ILog Warnlog
    {
        get
        {
            if (_Warnlog != null)
            {
                return _Warnlog;
            }

            _Warnlog = LogManager.GetLogger(Repository.Name, "Warnlog");
            return _Warnlog;
        }
    }
    private static ILog _Infolog { get; set; }
    private static ILog Infolog
    {
        get
        {
            if (_Infolog != null)
            {
                return _Infolog;
            }

            _Infolog = LogManager.GetLogger(Repository.Name, "Infolog");
            return _Infolog;
        }
    }
    private static ILog _Debuglog { get; set; }
    private static ILog Debuglog
    {
        get
        {
            if (_Debuglog != null)
            {
                return _Debuglog;
            }

            _Debuglog = LogManager.GetLogger(Repository.Name, "Debuglog");
            return _Debuglog;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    private static string GetName(MethodBase method)
    {
        var name = (method.ReflectedType != null ? "类名:[" + method.ReflectedType.Name + "]; " : "") + "方法:[" +
                    method.Name + "];\r\n";
        return name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Fatal(object message, Exception ex = null)
    {
        if (!Fatallog.IsFatalEnabled)
        {
            return;
        }

        var method = new StackTrace().GetFrame(1).GetMethod();
        Fatallog.Fatal(GetName(method) + message, ex);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Error(object message, Exception ex = null)
    {
        if (!Errorlog.IsErrorEnabled)
        {
            return;
        }

        var method = new StackTrace().GetFrame(1).GetMethod();
        Errorlog.Error(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Warn(object message, Exception ex = null)
    {
        if (!Warnlog.IsWarnEnabled)
        {
            return;
        }

        var method = new StackTrace().GetFrame(1).GetMethod();
        Warnlog.Warn(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Info(object message, Exception ex = null)
    {
        if (!Infolog.IsInfoEnabled)
        {
            return;
        }

        var method = new StackTrace().GetFrame(1).GetMethod();
        Infolog.Info(GetName(method) + message, ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ex"></param>
    public static void Debug(object message, Exception ex = null)
    {
        if (!Debuglog.IsDebugEnabled)
        {
            return;
        }

        var method = new StackTrace().GetFrame(1).GetMethod();
        Debuglog.Debug(GetName(method) + message, ex);
    }
}