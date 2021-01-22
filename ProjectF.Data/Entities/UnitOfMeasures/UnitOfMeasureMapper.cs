﻿using ProjectF.Data.Entities.Common.ValueObjects;

namespace ProjectF.Data.Entities.UnitOfMeasures
{
    public static class UnitOfMeasureMapper
    {
        public static UnitOfMeasure FromDto(UnitOfMeasureDto dto)
            => new UnitOfMeasure(new Name(dto.Name)
                , dto.Value
                , dto.Created
                , dto.Status);

        public static UnitOfMeasureDto FromEntity(UnitOfMeasure entity)
            => (new UnitOfMeasureDto(entity.Name.Value
                , entity.Value
                , entity.Created
                , entity.Modified)) with { Id = entity.Id, Status = entity.Status };
    }
}
