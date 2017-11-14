using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrHuellitas.Controllers.MasterAdmin
{
    public class GestionesController : Controller
    {
        // GET: Gestiones
        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Mascotas()
        {
            return View();
        }

        public ActionResult Region()
        {
            return View();
        }

        public ActionResult Propaganda()
        {
            return View();
        }

        public ActionResult Paquetes()
        {
            return View();
        }
    }
}