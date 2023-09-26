using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Dataservice.Models.Authetication
{
    public class AppTokens
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime valid_until { get; set; }
    }
}
