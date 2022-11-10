using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_personeros2.Models
{
    public class Registro_Votos
    {
        public string idEstudiante { get; set; }
        public int numero { get; set; }
        public string Anio { get; set; }
        public DateTime fecha_reg { get; set; }
    }
}
