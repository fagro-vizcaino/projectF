using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectF.Data.Entities.Auth;
namespace ProjectF.Api.Infrastructure
{
    public class GetClaimsFromUser : IGetClaimsProvider
    {
        public string CompanyId { get; private set; }
        public GetClaimsFromUser(IHttpContextAccessor accessor)
        {
            CompanyId = accessor.HttpContext?
                .User.Claims.SingleOrDefault(x => x.Type == "companyId")?.Value ?? string.Empty;
        }
    }
}
