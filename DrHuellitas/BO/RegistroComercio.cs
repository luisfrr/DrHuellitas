using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class RegistroComercio
    {      
        public UsuarioBO usuario { get; set; }
        public ComercioBO comercio { get; set; }
        public DireccionBO direccion { get; set; }
    }
}