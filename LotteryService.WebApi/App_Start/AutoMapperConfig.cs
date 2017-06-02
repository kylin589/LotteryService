using AutoMapper;
using LotteryService.Application.Common;

namespace LotteryService.WebApi
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<DtoToEntityMappingProfile>();
                cfg.AddProfile<EntityToDtoMappingProfile>();
            });
            Mapper.Configuration.CompileMappings();
        }
    }
}