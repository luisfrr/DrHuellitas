using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrHuellitas.BO;
using DrHuellitas.DAO;

namespace DrHuellitas.Controllers.MasterComercio
{
    public class ComercioController : Controller
    {
        ComercioDAO objDAO = new ComercioDAO();
        // GET: Comercio
        public ActionResult Index()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    modulo = "~/Admin/Index";
                }
                else if ((int)Session["idtipo"] == 2)
                {
                    modulo = ((int)Session["status"] == 1) ? "~/Usuario/Index" : "~/Usuario/Continuar";
                }
                else if ((int)Session["idtipo"] == 3)
                {
                    if ((int)Session["status"] == 1)
                        return View();
                    else
                        modulo = "~/Comercio/Continuar";
                }
                else if ((int)Session["idtipo"] == 4)
                {
                    modulo = ((int)Session["status"] == 1) ? "~/Vet/Index" : "~/Vet/Continuar";
                }
            }
            else
            {
                modulo = "~/Inicio/Index";
            }

            return Redirect(modulo);
        }

        public ActionResult Continuar()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    modulo = "~/Admin/Index";
                }
                else if ((int)Session["idtipo"] == 2)
                {
                    modulo = ((int)Session["status"] == 1) ? "~/Usuario/Index" : "~/Usuario/Continuar";
                }
                else if ((int)Session["idtipo"] == 3)
                {
                    if ((int)Session["status"] == 1)
                        modulo = "~/Comercio/Index";
                    else
                        return View();
                }
                else if ((int)Session["idtipo"] == 4)
                {
                    modulo = ((int)Session["status"] == 1) ? "~/Vet/Index" : "~/Vet/Continuar";
                }
            }
            else
            {
                modulo = "~/Inicio/Index";
            }

            return Redirect(modulo);
        }
        public ActionResult completarregistro(RegistrosBO dato)
        {
            int id = (int)Session["id"];
            var onda = objDAO.ContinuarRegistroComercio(dato, id);
            var comercio = objDAO.ComercioDatos(dato, id);
            var direccion = objDAO.comercioDireccion(dato, id);
            Session["status"] = 1;
            return Redirect("~/Comercio/Index");

        }
    }
}