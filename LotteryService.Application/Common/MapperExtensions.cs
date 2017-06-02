using AutoMapper;
using Lottery.Entities;

namespace LotteryService.Application
{
    public static class MapperExtensions
    {
        public static TEntity DtoConvertEntity<TEntity>(this IDto dto,TEntity entity)
           where TEntity:BaseEntity,new ()
        {
           return (TEntity) Mapper.Map(dto, entity, dto.GetType(), entity.GetType());
        }

        public static TEntity DtoConvertEntity<TEntity>(this IDto dto)
          where TEntity : BaseEntity, new()
        {
            return dto.DtoConvertEntity(new TEntity());
        }

        public static TDto EntityConvertDto<TEntity,TDto>(this TEntity entity,TDto dto)
           where TEntity : BaseEntity, new()
        {
            return (TDto)Mapper.Map(entity, dto, entity.GetType(), dto.GetType());
        }

        public static TDto EntityConvertDto<TEntity, TDto>(this TEntity entity)
          where TEntity : BaseEntity, new() where TDto :IDto, new()
        {
            return entity.EntityConvertDto(new TDto());
        }
    }
}