using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Service
{
    public class HasPermissionFilterOptions
    {
        public string ServiceCode { get; set; }

        public string IdentityBaseURL { get; set; }

        public HasPermissionFilterOptions(string serviceCode, string identityBaseUrl)
        {
            ServiceCode = serviceCode;
            IdentityBaseURL = identityBaseUrl;
        }
    }
}
