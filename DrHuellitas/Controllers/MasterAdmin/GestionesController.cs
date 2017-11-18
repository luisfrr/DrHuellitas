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
        RegionesDAO objRegionesDAO = new RegionesDAO();

        // GET: Gestiones
        public ActionResult Usuarios()
        {
            return View();
        }


        //Mascotas
        public ActionResult Mascotas()
        {
            return View();
        }


        //Region
        public ActionResult Ciudades()
        {
            List<RegionesBO> Estados = objRegionesDAO.ListaEstados().ToList();
            ViewBag.ListaDeEstados = new SelectList(Estados, "id", "nombre");
            return View();
        }
        public JsonResult ObtenerListaCiudades()
        {
            List<RegionesBO> PackCiudades = objRegionesDAO.ListaCiudades().ToList();
            return Json(PackCiudades, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCiudad(int idCiudad)
        {
            List<RegionesBO> PackCiudades = objRegionesDAO.ObtenerCiudad(idCiudad).ToList();
            return Json(PackCiudades, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarCiudades(RegionesBO model)
        {
            var result = false;
            try
            {
                if (model.ciudad.id > 0)
                {
                    objRegionesDAO.ActualizarCiudad(model);
                    result = true;
                }
                else
                {
                    objRegionesDAO.AgregarCiudad(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarCiudad(int idCiudad)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarCiudad(idCiudad);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Propaganda
        public ActionResult Propaganda()
        {
            return View();
        }


        //Paquetes
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