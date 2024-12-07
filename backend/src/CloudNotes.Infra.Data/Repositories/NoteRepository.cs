using CloudNotes.Domain.Entities;
using CloudNotes.Domain.Interfaces.Repositories;
using CloudNotes.Infra.Data.Context;
using CloudNotes.Infra.Data.Repositories.Common;

namespace CloudNotes.Infra.Data.Repositories;

internal class NoteRepository : GenericRepository<Note>, INoteRepository
{
    public NoteRepository(ApplicationDbContext context) : base(context)
    {
    }
}
