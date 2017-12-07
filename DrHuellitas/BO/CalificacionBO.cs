using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class CalificacionBO
    {
        public int id { get; set; }
        public double valoracion { get; set; }
        public int emisor { get; set; }
        public int receptor { get; set; }
    }
}