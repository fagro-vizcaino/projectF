using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectF.Data.Entities.Auth;
namespace ProjectF.Api.Infrastructure
{
    public class GetClaimsFromUser : IGetClaimsProvider
    {
        readonly ILogger _logger;

        public string CompanyId { get; private set; }
        public string UserId { get; private set; }
        public GetClaimsFromUser(IHttpContextAccessor accessor, ILoggerFactory logger)
        {
            CompanyId = accessor.HttpContext?
                .User.Claims.SingleOrDefault(x => x.Type == "companyId")?.Value ?? string.Empty;

            _logger = logger.CreateLogger("Authorization");
            _logger.LogInformation($"company id {CompanyId}");
            
            UserId = accessor.HttpContext?
            .User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value  ?? string.Empty;
            
        }
    }
}
