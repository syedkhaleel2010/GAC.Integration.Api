using GAC.Integration.Domain.Dto;
using GAC.Integration.Service.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GAC.Integration.Service
{
    public class UserSession : IUserSession
    {
        private readonly HttpContext _context;

        private static HasPermissionFilterOptions _options;

        public UserSession(IHttpContextAccessor context, HasPermissionFilterOptions options)
        {
            _context = context.HttpContext;
            _options = options;
        }

        public User GetUser()
        {
            if (_context == null)
            {
                return null;
            }

            ClaimsIdentity claimsIdentity = _context.User?.Identity as ClaimsIdentity;
            Claim claim = claimsIdentity.Claims.FirstOrDefault((Claim x) => x.Type == "ORGANIZATIONUNITS");
            User result = null;
            if (claimsIdentity?.Claims != null && claimsIdentity.Claims.Any() && !string.IsNullOrEmpty(claimsIdentity.Name))
            {
                Claim claim2 = claimsIdentity.Claims.FirstOrDefault((Claim x) => x.Type == "USERGROUP");
                User user = new User();
                user.Name = claimsIdentity.Name;
                user.UserGroups = claim2?.Value.Split(new char[1] { ',' });
                user.Email = claimsIdentity.Claims.FirstOrDefault((Claim e) => e.Type == "EMAIL")?.Value;
                user.FullName = claimsIdentity.Claims.FirstOrDefault((Claim e) => e.Type == "FULLNAME")?.Value;
                
                result = user;
            }

            return result;
        }

      
    }
}
