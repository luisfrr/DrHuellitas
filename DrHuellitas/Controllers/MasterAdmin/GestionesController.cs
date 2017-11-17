using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DrHuellitas.BO;
using DrHuellitas.DAO;
using System.Data;
using Newtonsoft.Json;

namespace DrHuellitas.Controllers.MasterAdmin
{
    public class GestionesController : Controller
    {
        PaquetesDAO objPaqueteDAO = new PaquetesDAO();

        // GET: Gestiones
        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Mascotas()
        {
            return View();
        }

        public ActionResult Region()
        {
            return View();
        }

        public ActionResult Propaganda()
        {
            return View();
        }

        public ActionResult Paquetes()
        {
            return View();
        }

        public JsonResult ObtenerListaPaquetes()
        {
            List<PaquetesBO> PackList = objPaqueteDAO.ListPaquetes().ToList();
            return Json(PackList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPaquete(int idPaquete)
        {
            List<PaquetesBO> PackList = objPaqueteDAO.ObtenerPaquete(idPaquete).ToList();
            return Json(PackList, JsonRequestBehavior.AllowGet);
        }

       /* public JsonResult ObtenerPaquete(int idPaquete)
        {
            DataTable dt = objPaqueteDAO.ObtenerPaquete(idPaquete);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(dt, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }*/

        public JsonResult GuardarPaquetes(PaquetesBO model)
        {
            var result = false;
            try
            {
                if (model.id > 0)
                {
                    objPaqueteDAO.ModificarPaquete(model);
                    result = true;
                }
                else
                {
                    objPaqueteDAO.AgregarPaquete(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarPaquete(int idPaquete)
        {
            bool result = false;

            int x = objPaqueteDAO.EliminarPaquete(idPaquete);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Comentarios()
        {
            return View();
        }
    }
}