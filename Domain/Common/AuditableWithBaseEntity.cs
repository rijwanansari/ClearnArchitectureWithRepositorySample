namespace Domain.Common
{
    public abstract class AuditableWithBaseEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; }
        public long Author { get; set; }
        public DateTime? Modified { get; set; }
        public long Editor { get; set; }
    }
}
