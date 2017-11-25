using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class UsuarioBO
    {
        public int id { get; set; }
        public int idtipo { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public DateTime fecharegistro { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string fnacimiento { get; set; }
        public string fregistro { get; set; }
        public int status { get; set; }
        public string tipoUs { get; set; }
        public string apellidos { get; set; }
        public String foto { get; set; } //Para el HTML 
        public HttpPostedFileBase img { get; set; } //Para la BD
    }
}