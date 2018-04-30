/// <summary>
/// 转相对路径
/// </summary>
/// <param name="staticFile"></param>
/// <param name="relativePath"></param>
public static string ToRelativePath(this string staticFile, string relativePath)
{
    var arrayStaticFile = staticFile.Split('/');
    var path2Array = relativePath.Split('/');
    var s = arrayStaticFile.Length >= path2Array.Length ? path2Array.Length : arrayStaticFile.Length;
    //两个目录最底层的共用目录索引
    var closestRootIndex = -1;
    for (var i = 0; i < s; i++)
    {
        if (arrayStaticFile[i] == path2Array[i])
        {
            closestRootIndex = i;
        }
        else
        {
            break;
        }
    }
    //由path1计算 ‘../'部分
    var path1Depth = "";
    for (var i = 0; i < arrayStaticFile.Length; i++)
    {
        if (i > closestRootIndex + 1)
        {
            path1Depth += "../";
        }
    }
    //由path2计算 ‘../'后面的目录
    var path2Depth = "";
    for (var i = closestRootIndex + 1; i < path2Array.Length; i++)
    {
        path2Depth += "/" + path2Array[i];
    }
    if (path2Depth.Length > 0)
        path2Depth = path2Depth.Substring(1);
    return path1Depth + path2Depth;
}