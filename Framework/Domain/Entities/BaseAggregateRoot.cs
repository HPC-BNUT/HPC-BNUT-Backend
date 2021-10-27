using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Domain.Events;

namespace Framework.Domain.Entities
{
    public abstract class BaseAggregateRoot<TId> where TId:IEquatable<TId>
    {
        private readonly List<IDomainEvent> _events;
        public TId Id { get; protected set; }
        protected BaseAggregateRoot() => _events = new List<IDomainEvent>();
        protected void HandleEvent(IDomainEvent domainEvent)
        {
            SetStateByEvent(domainEvent);
            ValidateInvariants();
            _events.Add(domainEvent);
        }
        protected abstract void SetStateByEvent(IDomainEvent domainEvent);
        public IEnumerable<object> GetEvents() => _events.AsEnumerable();
        public void ClearEvents() => _events.Clear();
        protected abstract void ValidateInvariants();


        public override bool Equals(object obj)
        {
            var other = obj as BaseAggregateRoot<TId>;

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

        public static bool operator ==(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseAggregateRoot<TId> a, BaseAggregateRoot<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}