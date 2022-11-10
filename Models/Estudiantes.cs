using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_personeros2.Models
{
    public class Estudiantes
    {
        public string id { get; set; }
        public double  grado { get; set; }
        public string grupo { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string estado { get; set; }
        public int codInst { get; set; }
        public string sexo { get; set; }
        public string rol { get; set; }
        public string contrasena { get; set; }
    }
}
