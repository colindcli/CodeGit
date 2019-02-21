/// <summary>
/// 树结构
/// </summary>
public class TreeStructure
{
    /// <summary>
    /// 生成树结构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TIdType"></typeparam>
    /// <param name="rows"></param>
    /// <param name="id"></param>
    /// <param name="parentId"></param>
    /// <param name="child">(item, list) => item.Child = list; 其中Child为子节点</param>
    /// <returns></returns>
    public static List<T> CreateTreeView<T, TIdType>(List<T> rows, Func<T, TIdType> id, Func<T, TIdType> parentId, Action<T, List<T>> child)
    {
        rows.ForEach(row => child.Invoke(row, rows.Where(item => parentId.Invoke(item).Equals(id.Invoke(row))).ToList()));
        return rows.Where(j => !rows.Exists(i => id.Invoke(i).Equals(parentId.Invoke(j)))).ToList();
    }
}
