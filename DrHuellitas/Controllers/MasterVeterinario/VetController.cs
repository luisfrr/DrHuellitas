using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrHuellitas.BO;
using DrHuellitas.DAO;

namespace DrHuellitas.Controllers.MasterVeterinario
{
    public class VetController : Controller
    {
        DireccionBO objDireccion = new DireccionBO();

        

        ComercioDAO ObjDAO = new ComercioDAO();
        // GET: Vet
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ContinuarRegistro()
        {
            objDireccion.longitud = "-89.6232412";
                objDireccion.latitud = "20.9675914";
            return View();
        }
        
    }
}