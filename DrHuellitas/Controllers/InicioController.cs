using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrHuellitas.DAO;
using DrHuellitas.BO;

namespace DrHuellitas.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        UsuarioDAO objdao = new UsuarioDAO();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login2()
        {
            return View();
        }
        public ActionResult Guardar(RegistroBO registro)
        {
          var r=  objdao.agregarUsuario(registro);

            return Redirect("~/Inicio/login2");
        }
    }
}