using com.split.backend.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace com.split.backend.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent :IEvent
{
    
}