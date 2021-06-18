namespace ProjectF.Data.Entities.GoodsTypes
{
    public static class GoodsTypeMapper
    {
        public static GoodsType FromDto(GoodsTypeDto dto)
            => new GoodsType(dto.Id
                , dto.Code
                , dto.Description
                , dto.Status);

        public static GoodsTypeDto FromEntity(GoodsType entity)
            => new GoodsTypeDto(entity.Id
                , entity.Code
                , entity.Description
                , entity.Status);
    }
}
