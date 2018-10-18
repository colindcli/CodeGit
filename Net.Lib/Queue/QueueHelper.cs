using Priority_Queue;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 列队
/// </summary>
/// <typeparam name="T"></typeparam>
public class QueueHelper<T>
{
    private static readonly SimplePriorityQueue<T> PriorityQueue = new SimplePriorityQueue<T>();

    private static QueueHelper<T> Obj { get; set; }

    public static QueueHelper<T> InitQueue(Action<T> action)
    {
        if (Obj != null)
        {
            return Obj;
        }
        Obj = new QueueHelper<T>();
        Obj.Dequeue(action);
        return Obj;
    }

    public void AddQueue(T item)
    {
        PriorityQueue.Enqueue(item, DateTime.Now.Ticks);
    }

    private void Dequeue(Action<T> action)
    {
        Task.Run(() =>
        {
            while (true)
            {
                while (PriorityQueue.Count != 0)
                {
                    try
                    {
                        var item = PriorityQueue.Dequeue();
                        action?.Invoke(item);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }

                Thread.Sleep(50);
            }
        });
    }
}

/*
//安装
Install-Package OptimizedPriorityQueue -Version 4.1.1

//调用
var queue = QueueHelper<string>.InitQueue(m =>
{
    Console.WriteLine(m);
});
queue.AddQueue("test");
*/