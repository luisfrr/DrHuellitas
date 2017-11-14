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
        UsuarioDAO objDAO = new UsuarioDAO();
        public ActionResult Index() //Este método lanza la página del inicio de sesión
        {
            int tipo = 0;
            int status = 0;
            
            string modulo = "";
            if(Session["id"] != null)
            {
                tipo = (int)Session["idtipo"];
                status = (int)Session["status"];

                if (tipo == 1)
                {
                    modulo = "~/Admin/Index";
                }
                else if (tipo == 2)
                {
                    modulo = (status == 1) ? "~/Usuario/Index": "~/Usuario/Continuar";
                }
                else if (tipo == 3)
                {
                    modulo = (status == 1) ? "~/Comercio/Index": "~/Comercio/Continuar";
                }
                else if (tipo == 4)
                {
                    modulo = (status == 1) ? "~/Vet/Index" : "~/Vet/Continuar";
                }
            }
            else
            {
                return View();
            }

            return Redirect(modulo);
        }
        public ActionResult Registrar(RegistroBO registro) //Este método es el realiza el registro
        {
            var r =  objDAO.RegistrarUsuario(registro);
            
            return Redirect("~/Inicio/Index");
        }


        public ActionResult IniciarSesion(RegistroBO registro) //Este método es el que válida el usuario(login)
        {
            string Modulo = "";
            var r = objDAO.IniciarSesion(registro.usuario, registro.contraseña);
            if (r != null)
            {
                Session["id"] = r.id;
                Session["nombre"] = r.nombre;
                Session["idtipo"] = r.idtipo;
                Session["foto"] = r.foto;
                Session["status"]= r.status;

                int status = r.status;
                int tipo = r.idtipo;
                if(tipo == 1)
                {
                    Modulo = "~/Admin/Index";
                }
                else if (tipo == 2)
                {
                    Modulo = (status == 0) ? "~/Usuario/Continuar" : "~/Usuario/Index";
                }
                else if (tipo == 3)
                {
                    Modulo = (status == 0) ? "~/Comercio/Continuar" : "~/Comercio/Index";
                }
                else if(tipo == 4)
                {
                    Modulo = (status == 0) ? "~/Vet/Continuar" : "~/Vet/Index";
                }
            }
            else
            {
                return Redirect("~/Inicio/Index");
            }

            return Redirect(Modulo);
        }
        public ActionResult CerrarSesion() //Este método se va a llamar en cada master para cerrar sesión
        {
            string modulo = "";
            Session.RemoveAll();
            if (Session["usuario"] == null)
            {
                modulo = "~/Inicio/Index";
            }

            return Redirect(modulo);
        }
    }
}