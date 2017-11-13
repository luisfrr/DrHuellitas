using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DrHuellitas.BO;
using DrHuellitas.DAO;

namespace DrHuellitas.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioDAO objDAO = new UsuarioDAO();
        // GET: Usuario
        public ActionResult Index()//Hola bebe
        {
            return View();
        }

        public ActionResult Continuar()
        {
            if ((int)Session["status"] == 1)
            {
                return Redirect("~/Usuario/Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ContinuarRegistro(RegistroBO objBO)
        {
            int id = (int)Session["id"];
            var r = objDAO.ContinuarRegistro(objBO, id);

            return Redirect("~/Usuario/Index");
        }

    }
}