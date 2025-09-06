

namespace GAC.Integration.Domain
{

    public class EntityBase : EntityBase<Guid>, IEntityBase, IEntityBase<Guid>
    {
    }

    public class EntityBase<T> : IEntityBaseLocalDate<T>, IEntityBase<T>
    {
        public virtual T ID { get; set; }

        public virtual DateTime CreatedAt { get; set; }

        public virtual string CreatedBy { get; set; }

        public virtual DateTime? UpdatedAt { get; set; }

        public virtual string UpdatedBy { get; set; }

    }
    public interface IEntityBaseLocalDate<T> : IEntityBase<T>
    {
        DateTime CreatedAt { get; set; }

        DateTime? UpdatedAt { get; set; }
    }
    public interface IEntityBase<T>
    {
        T ID { get; set; }

        string CreatedBy { get; set; }

        string UpdatedBy { get; set; }
    }
    public interface IEntityBase : IEntityBase<Guid>
    {
    }
}
