using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AddEvent(Event eventValue)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(eventValue);
        }

        public void RemoveEvent(Event eventValue)
        {
            _notifications?.Remove(eventValue);
        }

        public void ClearEvents()
        {
            _notifications?.Clear();
        }

    }
}
