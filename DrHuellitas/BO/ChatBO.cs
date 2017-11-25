using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class ChatBO
    {
        public int id { get; set; }
        public string mensaje { get; set; }
        public int idremitente { get; set; }
        public int idemisor { get; set; }
        public int status { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public string sfecha { get; set; }
        public string shora { get; set; }
    }
}