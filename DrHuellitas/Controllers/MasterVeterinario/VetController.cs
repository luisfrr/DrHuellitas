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
        ComercioDAO ObjDAO = new ComercioDAO();
        // GET: Vet
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ContinuarRegistro()
        {
            return View();
        }
        
    }
}