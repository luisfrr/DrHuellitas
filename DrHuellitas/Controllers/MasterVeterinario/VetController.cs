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
        // GET: Vet
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
                    modulo = ((int)Session["status"] == 1) ? "~/Comercio/Index" : "~/Comercio/Continuar";
                }
                else if ((int)Session["idtipo"] == 4)
                {
                    if ((int)Session["status"] == 1)
                        return View();
                    else
                        modulo = "~/Vet/Continuar";
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
                    modulo = ((int)Session["status"] == 1) ? "~/Comercio/Index" : "~/Comercio/Continuar";
                }
                else if ((int)Session["idtipo"] == 4)
                {
                    if ((int)Session["status"] == 1)
                        modulo = "~/Vet/Index";
                    else
                        return View();
                }
            }
            else
            {
                modulo = "~/Inicio/Index";
            }

            return Redirect(modulo);
        }
    }
}