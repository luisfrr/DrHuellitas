using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;

namespace DrHuellitas.BO
{
    public class PuntosdeUbicacionBO
    {
        public UsuarioBO usuarios { get; set; }
        public DireccionBO direccion { get; set; }
        public ComercioBO comercio { get; set; }
    }
}