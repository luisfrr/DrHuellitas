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
                else if (tipo == 0)
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

            return Redirect(modulo);
        }
        public ActionResult Registrar(RegistrosBO registro) //Este método es el realiza el registro
        {
            var r =  objDAO.RegistrarUsuario(registro);
            
            return Redirect("~/Inicio/Index");
        }


        public ActionResult IniciarSesion(RegistrosBO registro) //Este método es el que válida el usuario(login)
        {
            string Modulo = "~/Inicio/Index";
            var r = objDAO.IniciarSesion(registro.usuario.usuario, registro.usuario.contraseña);
            if (r != null)
            {
                Session["id"] = r.usuario.id;
                Session["nombre"] = r.usuario.nombre;
                Session["idtipo"] = r.usuario.idtipo;
                Session["foto"] = r.usuario.foto;
                Session["status"]= r.usuario.status;

                int id = r.usuario.id;
                int status = r.usuario.status;
                int tipo = r.usuario.idtipo;
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
                else if(tipo == 0 && id == 0)
                {
                    Modulo = "~/Inicio/Index";
                }
            }
            else
            {
                Modulo = "~/Inicio/Index";
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