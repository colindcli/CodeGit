-- 实体层
public interface IAdminDa
{

}

-- 数据层
public class AdminDa : IAdminDa
{

}

-- 业务层
private static readonly IAdminDa Da = AutoFacHelper.Resolve<IAdminDa>();