using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class GestionMascotaBO
    {
        public MascotasBO mascotas { get; set; }
        public EspeciesBO especies { get; set; }
        public RazasBO razas { get; set; }
    }
}