internal class Program
{
    private static void Main(string[] args)
    {
        var res = Translate.TranslateText("The best way to learn how to use C is to follow the tutorials in our developer guide:", lang.auto, lang.zh_CN);
        res.Wait();

        var text = res.Result.IsSuccess ? res.Result.Result.sentences[0].trans : null;

        Console.WriteLine(text);
        Console.ReadKey();
    }
}