using AutoMapper;
using System;

class Program
{
    static void Main(string[] args)
    {
        Mapper.Initialize(m =>
        {
            m.CreateMap<A, AA>();
        });

        var a = new A()
        {
            Id = 100
        };

        var aa = Mapper.Map<AA>(a);

        Console.ReadKey();
    }
}

public class A
{
    public int Id { get; set; }
}

public class AA : A
{
    public string Name { get; set; }
}
