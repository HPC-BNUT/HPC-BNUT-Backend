using System;
using Framework.Domain.Events;

namespace Framework.Domain.Entities
{
    public abstract class BaseEntity<TId> where TId : IEquatable<TId>
    {
        public TId Id { get; private set; }
        private Action<IDomainEvent> _applier;

        public BaseEntity(Action<IDomainEvent> applier)
        {
            _applier = applier;
        }

        protected BaseEntity()
        {
        }

        public void HandleEvent(IDomainEvent domainEvent)
        {
            SetStateByEvent(domainEvent);
            _applier(domainEvent);
        }

        protected abstract void SetStateByEvent(IDomainEvent domainEvent);

        public override bool Equals(object obj)
        {
            var other = obj as BaseEntity<TId>;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(BaseEntity<TId> a, BaseEntity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntity<TId> a, BaseEntity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

    }
}