//https://www.nuget.org/packages/Npoi.Mapper/
//http://donnytian.github.io/Npoi.Mapper/


//导入
public class ImportDemo
{
    public static void GetSheet()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory + "1.xlsx";

        var mapper = new Mapper(path);
        var sheet = mapper.Take<SampleClass>(0).ToList();
        var errorLists = sheet.Where(p => p.ErrorColumnIndex > -1).Select(m => $"第{++m.RowNumber}行第{++m.ErrorColumnIndex}列错误").ToList();
        var lists = sheet.Select(p => p.Value).ToList();
    }

    public class SampleClass
    {
        [Column(0)]
        public int Id { get; set; }
        [Column(1)]
        public string Name { get; set; }
        [Column(2)]
        public DateTime Date { get; set; }
    }
}