using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class RegistrosBO
    {
        
        public UsuarioBO usuario { get; set; }
        public MascotasBO mascota { get; set; }
        public ComercioBO comercio { get; set; }
        public HorarioBO horario { get; set; }
        public DireccionBO direccion { get; set; }
        public CalificacionBO calificacion { get; set; }

    }
}