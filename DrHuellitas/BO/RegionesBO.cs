using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class RegionesBO
    {
        public PaisesBO pais { get; set; }
        public EstadosBO estado { get; set; }
        public CiudadesBO ciudad { get; set; }
    }
}