using System;

namespace Ordering.Api.Domain
{
    public abstract class Identity<T> : IEquatable<Identity<T>>
    {
        protected Identity(T id)
        {
            Id = id;
        }

        public T Id { get; private set; }

        public static bool operator ==(Identity<T> a, Identity<T> b)
        {
            if (ReferenceEquals(null, a))
            {
                return ReferenceEquals(null, b);
            }

            return a.Equals(b);
        }

        public static bool operator !=(Identity<T> a, Identity<T> b)
        {
            return !(a == b);
        }

        public bool Equals(Identity<T> other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Id.Equals(other.Id);
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

        public static implicit operator T(Identity<T> identity)
        {
            return identity.Id;
        }
    }
}