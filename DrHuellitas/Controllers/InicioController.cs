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
            if (Session["usuario"] != null)
            {
                return Redirect("~/Inicio/index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Guardar(RegistroBO registro)
        {
          var r=  objdao.agregarUsuario(registro);

            return Redirect("~/Inicio/login2");
        }
        public ActionResult login(RegistroBO registro)
        {
            var r = objdao.BuscarUsuario(registro.usuario, registro.contraseña);
            if (r != null)
            {
                Session["usuario"] = r;
                ViewBag.Usuario = (RegistroBO)Session["usuario"];
                return Redirect("~/Inicio/index");
            }
            else
            {
                return Redirect("~/Inicio/login2");
            }

        }
        public ActionResult CerrarSesion()
        {
            Session.Remove("usuario");
            Session.Abandon();
            if (Session == null)
            {
                return Redirect("~/Inicio/login2");
            }
            return Redirect("~/Inicio/login2");
        }
    }
}