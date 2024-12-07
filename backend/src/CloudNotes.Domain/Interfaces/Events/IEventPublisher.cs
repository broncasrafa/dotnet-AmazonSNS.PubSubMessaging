using CloudNotes.Domain.Models;

namespace CloudNotes.Domain.Interfaces.Events;

public interface IEventPublisher
{
    Task PublishEventAsync(Event eventData);
}
