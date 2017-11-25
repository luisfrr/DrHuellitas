using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrHuellitas.Controllers.MasterAdmin
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    return View();
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
                    modulo = ((int)Session["status"] == 1) ? "~/Vet/Index" : "~/Vet/Continuar";
                }
            }
            else
            {
                modulo = "~/Inicio/Index";
            }

            return Redirect(modulo);
        }


        public ActionResult Chat()
        {
            return View();
        }
        
    }
}