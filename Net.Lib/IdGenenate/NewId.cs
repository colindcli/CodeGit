private static void Main(string[] args)
{
    //简单
    //var generator = new NewIdGenerator(new MockTickProvider(Convert.ToDateTime("2000-01-01").Ticks), new MockNetworkProvider(), new CurrentProcessIdProvider());

    //复杂，生成指定时间NewId
    var tickProvider = new MockTickProvider(Convert.ToDateTime("2000-01-01").Ticks);
    var workerIdProvider = new MockNetworkProvider(BitConverter.GetBytes(1234567890L)); //1234567890L是Network值，用于分布式，不同机器不同值
    var processIdProvider = new MockProcessIdProvider(BitConverter.GetBytes(10));
    var generator = new NewIdGenerator(tickProvider, workerIdProvider, processIdProvider);
    var dt = generator.Next().Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    Console.WriteLine(dt);

    Console.ReadKey();
}


private class MockTickProvider : ITickProvider
{
    public MockTickProvider(long ticks)
    {
        Ticks = ticks;
    }

    public long Ticks { get; }
}

private class MockNetworkProvider : IWorkerIdProvider
{
    private readonly byte[] _workerId;

    public MockNetworkProvider(byte[] workerId)
    {
        _workerId = workerId;
    }

    public byte[] GetWorkerId(int index)
    {
        return _workerId;
    }
}

private class MockProcessIdProvider : IProcessIdProvider
{
    private readonly byte[] _processId;

    public MockProcessIdProvider(byte[] processId)
    {
        _processId = processId;
    }

    public byte[] GetProcessId()
    {
        return _processId;
    }
}