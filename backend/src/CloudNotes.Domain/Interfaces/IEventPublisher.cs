using CloudNotes.Domain.Models;

namespace CloudNotes.Domain.Interfaces;

public interface IEventPublisher
{
    Task PublishEventAsync(Event eventData);
}
