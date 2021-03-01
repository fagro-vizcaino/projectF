using System;

namespace ProjectF.Data.Entities.Common
{
    public abstract class _BaseEntity
    {
        public int Id { get; private set; }
        public EntityStatus Status { get; protected set; }
        public int CompanyId { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime? Modified { get; protected set; }
        protected _BaseEntity() { }

        protected _BaseEntity(int id, EntityStatus status) : this()
        {
            Id = id;
            Status = status;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is _BaseEntity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetRealType() != other.GetRealType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(_BaseEntity a, _BaseEntity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(_BaseEntity a, _BaseEntity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            Type type = GetType();

            if (type.ToString().Contains("Castle.Proxies."))
                return type?.BaseType ?? type;

            return type;
        }

        public void SetCompany(int id)
            => CompanyId = id;

        public void SetStatus(EntityStatus status)
            => Status = status;

    }

    public enum EntityStatus
    {
        Active = 1,
        Deleted = 2
    }
}