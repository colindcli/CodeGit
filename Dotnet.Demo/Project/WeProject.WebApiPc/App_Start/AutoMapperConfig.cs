using AutoMapper;

namespace WeProject.WebApiPc
{
    public class AutoMapperConfig
    {
        public static void RegisterMapper()
        {
            Mapper.Initialize(m =>
            {
                //m.CreateMap<T1, T2>();
            });
        }
    }
}