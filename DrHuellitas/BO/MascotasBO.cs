using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class MascotasBO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int idusuario { get; set; }
        public string colorDominate { get; set; }
        public string colorPreDominante { get; set; }
        public string colorAlternativo { get; set; }
        public int genero { get; set; }
        public DateTime fechaNaci { get; set; }
        public HttpPostedFileBase img { get; set; }
    }
}