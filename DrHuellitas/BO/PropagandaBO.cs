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
        public string fecha { get; set; }
        public byte status { get; set; }
        public string fotousuario { get; set; }
        public string nombrecomercio { get; set; }
        public UsuarioBO usuario { get; set; }
        public string email { get; set; }
        public string telefono { get; set;  }
        public int idcomercio { get; set; }
        public string gerente { get; set; }
    }
}