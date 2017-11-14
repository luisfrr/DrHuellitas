using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class DireccionBO
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string calle { get; set; }
        public string numero { get; set; }
        public string cruzamiento { get; set; }
        public string longitud { get; set; }
        public string latitud { get; set; }
        public string colonia { get; set; }
        public string CP { get; set; }
        public string ubicacion { get; set; }
        public int idCiudad { get; set; }
        public string nombreCiudad { get; set; }
        public int idEstado { get; set; }
        public string nombreEstado { get; set; }
        public int idPais { get; set; }
        public string nombrePais { get; set; }
    }
}