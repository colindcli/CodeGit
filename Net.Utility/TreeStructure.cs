/// <summary>
/// 树结构
/// </summary>
public class TreeStructure
{
    /// <summary>
    /// 生成树结构
    /// </summary>
    /// <param name="rows"></param>
    /// <returns></returns>
    public static List<Row> CreateTreeView(List<Row> rows)
    {
        rows.ForEach(row => row.Groups = rows.Where(item => item.ParentId == row.Id).ToList());
        return rows.Where(j => !rows.Exists(i => i.Id == j.ParentId)).ToList();
    }
}

public class Row
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public List<Row> Groups { get; set; }
}
