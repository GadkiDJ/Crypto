namespace Crypto.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public ICollection<User> Users {get; set;} = new List<User>();
    }
}
