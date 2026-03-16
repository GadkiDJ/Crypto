using System.Security.Claims;

namespace Crypto.Application.Common
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }
        public Guid? UserId
        {
            get
            {
                var id = _http.HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (id == null)
                    return null;

                return Guid.Parse(id);
            }
        }
        public Guid? TenantId
        {
            get
            {
                var id = _http.HttpContext?
                    .User?
                    .FindFirstValue("tenantId");

                if (id == null)
                    return null;

                return Guid.Parse(id);

            }
        }
    }
}
