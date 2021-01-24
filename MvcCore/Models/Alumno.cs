using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCorePaco.Models
{
    public class Alumno
    {
        public int idalumno { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public int nota { get; set; }
    }
}
