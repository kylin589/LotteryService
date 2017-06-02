
using AutoMapper;
using Lottery.Entities;
using LotteryService.Application.Lottery.Dtos;

namespace LotteryService.WebApi
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<LotteryData, LotteryDataOutput>();
        }
    }
}