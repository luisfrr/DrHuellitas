using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class comentariosBO
    {
        public int id { get; set; }
        public string comentario { get; set; }
        public string emisor { get; set; }
        public string foto { get; set; }
    }
}