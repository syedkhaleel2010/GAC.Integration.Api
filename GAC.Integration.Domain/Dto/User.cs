using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAC.Integration.Domain.Dto
{
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string[] UserGroups { get; set; }

        public string FullName { get; set; }

        public string[] PortCodes { get; set; }
    }
}
