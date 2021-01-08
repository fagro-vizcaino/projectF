using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectF.Data.Entities.Auth;
namespace ProjectF.Api.Infrastructure
{
    public class GetClaimsFromUser : IGetClaimsProvider
    {
        public string CompanyId { get; private set; }
        public string UserId { get; private set; }
        public GetClaimsFromUser(IHttpContextAccessor accessor)
        {
            CompanyId = accessor.HttpContext?
                .User.Claims.SingleOrDefault(x => x.Type == "companyId")?.Value ?? string.Empty;

            UserId = accessor.HttpContext?
            .User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value  ?? string.Empty;
        }
    }
}
