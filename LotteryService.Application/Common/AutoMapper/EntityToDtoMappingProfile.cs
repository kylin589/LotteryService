using AutoMapper;
using Lottery.Entities;
using LotteryService.Application.Account.Dtos;
using LotteryService.Application.Lottery.Dtos;

namespace LotteryService.Application.Common
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<LotteryData, LotteryDataOutput>();
            CreateMap<User, UserDto>();
        }
    }
}