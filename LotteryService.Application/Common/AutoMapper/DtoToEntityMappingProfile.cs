using AutoMapper;
using Lottery.Entities;
using LotteryService.Application.Log.Dtos;
using LotteryService.Application.Lottery.Dtos;

namespace LotteryService.Application.Common
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<AuditLogInput,AuditLog>();
            CreateMap<AuditLogEdit,AuditLog>();

            CreateMap<LotteryDataInput, LotteryData>();
        }
    }
}