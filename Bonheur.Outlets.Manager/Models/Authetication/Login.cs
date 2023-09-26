using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bonheur.Outlets.Manager.Models.Authetication
{
    public class Login
    {
        public string user_query{ get; set; }
        public string password { get; set; }
    }
}
