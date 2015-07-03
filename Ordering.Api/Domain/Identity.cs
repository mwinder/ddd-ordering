using System;

namespace Ordering.Api.Domain
{
    public class OrderId : Identity<int>
    {
        public OrderId() : base(0) { }
        public OrderId(int id) : base(id) { }
    }

    public abstract class Identity<T> : IEquatable<Identity<T>>
    {
        protected Identity(T id)
        {
            Id = id;
        }

        public T Id { get; private set; }

        public bool Equals(Identity<T> id)
        {
            if (ReferenceEquals(this, id)) return true;
            if (ReferenceEquals(null, id)) return false;
            return Id.Equals(id.Id);
        }

        public override bool Equals(object other)
        {
            return Equals(other as Identity<T>);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}