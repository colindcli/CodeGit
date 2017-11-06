private static T GetSqlMapper<T>(string xmlFileName) where T : new()
{
    return (T)new YAXLib.YAXSerializer(typeof(T)).DeserializeFromFile($@"{AppDomain.CurrentDomain.BaseDirectory}App_Data/SqlMapper/{xmlFileName}");
}
