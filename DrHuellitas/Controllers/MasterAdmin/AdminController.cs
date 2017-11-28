using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrHuellitas.BO;
using DrHuellitas.DAO;

namespace DrHuellitas.Controllers.MasterAdmin
{
    public class AdminController : Controller
    {
        ChatDAO objChatDAO = new ChatDAO();
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

        public ActionResult ChatA()
        {
            return View();
        }

        public ActionResult Reporte()
        {
            return View();
        }

        public JsonResult ListarChatRecibe(int idUsuario)
        {
            List<ChatBO> PackChat = objChatDAO.ObtenerListaChatsRecibe(idUsuario).ToList();
            var json = Json(PackChat, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult AbrirChat(int idChat)
        {
            List<ChatBO> PackUser = objChatDAO.AbrirChat(idChat).ToList();
            var json = Json(PackUser, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult AbrirChatRecibe(int idChat, int idSession)
        {
            var status = objChatDAO.ActulizarStatus(idChat, idSession);
            List<ChatBO> PackChat = objChatDAO.VerChatRecibe(idChat).ToList();
            var json = Json(PackChat, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EnviarMensaje(ChatBO objBO)
        {
            var result = false;
            try
            {
                    objChatDAO.EnviarMensaje(objBO);
                    result = true;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

    }
}