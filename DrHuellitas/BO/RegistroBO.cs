using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class RegistroBO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string nombre { get; set; }
        public int id_tipo { get; set; }
        public string rep_contraseña { get; set; }

    }
}