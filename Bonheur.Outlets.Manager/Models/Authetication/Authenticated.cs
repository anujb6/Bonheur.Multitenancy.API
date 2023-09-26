using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.Authetication
{
    public class Authenticated
    {
        public object user { get; set; }

        public bool authenticated { get; set; }

        public string refresh_token { get; set; }

        public string access_token { get; set; }

        public DateTime valid_until { get; set; }
    }
}
