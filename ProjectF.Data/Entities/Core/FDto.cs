using ProjectF.Data.Entities.Common;

namespace ProjectF.Data.Entities.Core
{
    public record FDto
    {
        public int Id { get; init; }
        public EntityStatus Status { get; init; }
    }
}
