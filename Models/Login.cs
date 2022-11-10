using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_personeros2.Models
{
    public class Login
    {
        public string id { get; set; }
        public double nombre { get; set; }
        public string estado { get; set; }
        public string rol { get; set; }
        public string contrasena { get; set; }
    }
}
