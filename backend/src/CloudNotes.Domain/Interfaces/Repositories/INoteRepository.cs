using CloudNotes.Domain.Entities;
using CloudNotes.Domain.Interfaces.Repositories.Common;

namespace CloudNotes.Domain.Interfaces.Repositories;

public interface INoteRepository : IGenericRepository<Note>
{
}
