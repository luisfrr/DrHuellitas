using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DrHuellitas.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()//Hola bebe
        {
            return View();
        }

        public ActionResult Continuar()
        {
            return View();
        }

        private byte[] arr;//Array para mi imagen

        public byte[] FileUpload(HttpPostedFileBase file)
        {
            
            if (file!= null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    arr = ms.GetBuffer();
                    
                }
            }
            return arr;
        }

    }
}