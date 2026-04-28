using Crypto.Entities;
using Crypto.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Crypto.Application.Auth;

public class AuthService
{
    private readonly AppDbContext _db;
    private readonly PasswordHasher _hasher;
    private readonly JwtService _jwt;

    public AuthService(
        AppDbContext db,
        PasswordHasher hasher,
        JwtService jwt)
    {
        _db = db;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<string> Register(RegisterRequest request)
    {
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            TenantId = tenant.Id,
            CreatedAt = DateTime.UtcNow
        };

        user.passwordHash =
            _hasher.Hash(user, request.Password);

        _db.Tenants.Add(tenant);
        _db.Users.Add(user);

        await _db.SaveChangesAsync();
        return _jwt.Generate(user);
    }

    public async Task<string?> Login(LoginRequest request)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user == null)
            return null;

        var ok = _hasher.Verify(
            user,
            user.passwordHash,
            request.Password
        );

        if (!ok)
            return null;

        return _jwt.Generate(user);
    }
}