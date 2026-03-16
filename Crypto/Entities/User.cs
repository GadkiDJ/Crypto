namespace Crypto.Entities
{
    public class User
    {
        public Guid Id {  get; set; }
        public string Email { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public Guid TenantId { get; set; }
    }
}

