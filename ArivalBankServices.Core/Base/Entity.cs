namespace ArivalBankServices.Core.Base
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; protected set; }
    }
}
