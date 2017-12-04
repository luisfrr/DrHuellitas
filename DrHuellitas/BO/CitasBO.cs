using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class CitasBO
    {
        public MascotasBO mascotas { get; set; }
        public ComercioBO comercio { get; set; }
        public UsuarioBO propietario { get; set; }

        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string color { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }


        public string sInicio { get; set; }
        public string sFin { get; set; }

    }
}