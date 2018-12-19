//https://www.nuget.org/packages/DwrUtility
//Install-Package DwrUtility
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// 文件夹操作
/// </summary>
public class DirectoryHelper
{
    /// <summary>
    /// 删除文件夹内容
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static bool DeleteDirectoryContent(string dir)
    {
        try
        {
            var dirs = new List<string>();
            var files = new List<string>();
            GetDirectoryFiles(dir, ref dirs, ref files);
            var success = true;
            foreach (var p in files)
            {
                success = DeleteFile(p);
                if (!success)
                {
                    break;
                }
            }

            if (success)
            {
                dirs = dirs.Where(p => !p.IsEquals(dir)).OrderByDescending(p => p.Length).ToList();
                foreach (var p in dirs)
                {
                    success = DeleteDir(p);
                    if (!success)
                    {
                        break;
                    }
                }
            }

            return success;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 删除文件夹
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static bool DeleteDirectory(string dir)
    {
        var newDir = dir.TrimEnd('/', '\\') + "Temp";
        try
        {
            Directory.Move(dir, newDir);
            dir = newDir;

            var dirs = new List<string>();
            var files = new List<string>();
            GetDirectoryFiles(dir, ref dirs, ref files);
            var success = true;
            foreach (var p in files)
            {
                success = DeleteFile(p);
                if (!success)
                {
                    break;
                }
            }

            if (success)
            {
                dirs = dirs.OrderByDescending(p => p.Length).ToList();
                foreach (var p in dirs)
                {
                    success = DeleteDir(p);
                    if (!success)
                    {
                        break;
                    }
                }
            }

            return success;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool DeleteFile(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return true;
        }

        if (!File.Exists(path))
        {
            return true;
        }

        try
        {
            File.Delete(path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool DeleteDir(string dir)
    {
        if (string.IsNullOrWhiteSpace(dir))
        {
            return true;
        }

        if (!Directory.Exists(dir))
        {
            return true;
        }

        try
        {
            Directory.Delete(dir);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 获取文件夹和文件，包含子文件夹和文件
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="dirs"></param>
    /// <param name="files"></param>
    public static void GetDirectoryFiles(string dir, ref List<string> dirs, ref List<string> files)
    {
        if (dirs == null)
        {
            dirs = new List<string>();
        }

        if (files == null)
        {
            files = new List<string>();
        }
        files.AddRange(Directory.GetFiles(dir));
        dirs = GetDirectorys(dir);
        dirs.Add(dir);
        foreach (var d in dirs)
        {
            files.AddRange(Directory.GetFiles(d));
        }
    }

    /// <summary>
    /// 获取文件夹所有文件，包含子文件夹文件
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static List<string> GetFiles(string dir)
    {
        if (!Directory.Exists(dir))
        {
            return new List<string>();
        }

        var paths = new List<string>();
        GetFiles(dir, ref paths);
        return paths;
    }

    private static void GetFiles(string dir, ref List<string> paths)
    {
        var files = Directory.GetFiles(dir);
        if (files.Length > 0)
        {
            paths.AddRange(files);
        }
        var dirs = Directory.GetDirectories(dir);
        if (dirs.Length == 0)
            return;
        foreach (var d in dirs)
        {
            GetFiles(d, ref paths);
        }
    }

    /// <summary>
    /// 获取文件夹所有子文件夹(不包含当前文件夹)
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public static List<string> GetDirectorys(string dir)
    {
        if (!Directory.Exists(dir))
        {
            return new List<string>();
        }

        var paths = new List<string>();
        GetDirectorys(dir, ref paths);
        return paths;
    }

    private static void GetDirectorys(string dir, ref List<string> paths)
    {
        var dirs = Directory.GetDirectories(dir);
        if (dirs.Length == 0)
            return;
        paths.AddRange(dirs);
        foreach (var d in dirs)
        {
            GetDirectorys(d, ref paths);
        }
    }
}