namespace Dsw2026Ej15.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity(Guid? guid = null)
        {
            Id = guid ?? Guid.NewGuid();
        }

        public Guid Id { get; }
    }
}
