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
        public ActionResult Index() //Este método lanza la página del inicio de sesión
        {
            return View();
        }
        public ActionResult Registrar(RegistroBO registro) //Este método es el realiza el registro
        {
          var r=  objdao.agregarUsuario(registro);

            return Redirect("~/Inicio/Index");
        }
        public ActionResult IniciarSesion(RegistroBO registro) //Este método es el que válida el usuario(login)
        {
            var r = objdao.BuscarUsuario(registro.usuario, registro.contraseña);
            if (r != null)
            {
                Session["usuario"] = r;
                ViewBag.Usuario = (RegistroBO)Session["usuario"];
                return Redirect("~/Cliente/cliente");
            }
            else
            {
                return Redirect("~/Inicio/login2");
            }

        }
        public ActionResult CerrarSesion() //Este método se va a llamar en cada master para cerrar sesión
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