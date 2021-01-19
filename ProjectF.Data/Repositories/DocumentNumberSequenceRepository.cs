using ProjectF.Data.Context;
using ProjectF.Data.Entities.Sequences;

namespace ProjectF.Data.Repositories
{
    public class DocumentNumberSequenceRepository : BaseRepository<DocumentNumberSequence>
    {
        public DocumentNumberSequenceRepository(_AppDbContext context) : base(context)
        {
        }
    }
}
