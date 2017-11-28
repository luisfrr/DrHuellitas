using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrHuellitas.BO
{
    public class ChatBO
    {
        public int id { get; set; }
        public int idenvia { get; set; }
        public int idrecibe { get; set; }
        
        public MensajesBO mensajes { get; set; }
        public UsuarioBO usuarios { get; set; }
    }
}