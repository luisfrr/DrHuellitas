using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class PropagandaBO
    {
        public int id { get; set; }
        public int idusuario { get; set; }
        public HttpPostedFileBase imagen { get; set; }
        public string descripcion { get; set; }
        public string foto { get; set; }

    }
}