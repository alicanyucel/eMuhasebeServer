namespace eMuhasebeServer.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public bool IsDeleted { get; set; }
    }
}
