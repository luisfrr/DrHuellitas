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
        UsuarioDAO objUsuariosDAO = new UsuarioDAO();
        EspeciesDAO objEspeciesDAO = new EspeciesDAO();
        RazasDAO objRazasDAO = new RazasDAO();
        VacunasDAO objVacunasDAO = new VacunasDAO();
        RegistrosBO model = new RegistrosBO();

        // GET: Gestiones

        //Usuarios
        public ActionResult Usuarios()
        {
            List<TipoUsuarioBO> TipoUser = objUsuariosDAO.DropDownTipoUs().ToList();
            ViewBag.ListaTipoUs = new SelectList(TipoUser, "id", "nombre");
            return View();
        }

        public JsonResult ObtenerListaUsuarios()
        {
            List<RegistrosBO> PackUsuarios = objUsuariosDAO.ObtenerListaUsuarios().ToList();
            return Json(PackUsuarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerUsuario(int idUsuario)
        {
            List<RegistrosBO> PackUsuarios = objUsuariosDAO.ObtenerUsuario(idUsuario).ToList();
            return Json(PackUsuarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarUsuario(RegistrosBO model)
        {
            var result = false;
            try
            {
                if (model.usuario.id > 0)
                {
                    objUsuariosDAO.ActualizarUsuario(model);
                    result = true;
                }
                else
                {
                    objUsuariosDAO.AgregarUsuario(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarUsuario(int idUsuario)
        {
            bool result = false;

            int x = objUsuariosDAO.EliminarUsuario(idUsuario);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Mascotas
        public ActionResult Mascotas()
        {
            return View();
        }

        //Especies 
        public ActionResult Especies()
        {
            return View();
        }

        public JsonResult ObtenerListaEspecies()
        {
            List<EspeciesBO> PackEspecies = objEspeciesDAO.ObteterListaEspecies().ToList();
            return Json(PackEspecies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEspecie(int idEspecie)
        {
            List<EspeciesBO> PackEspecies = objEspeciesDAO.ObtenerEspecie(idEspecie).ToList();
            return Json(PackEspecies, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarEspecie(EspeciesBO model)
        {
            var result = false;
            try
            {
                if (model.id > 0)
                {
                    objEspeciesDAO.ActualizarEspecie(model);
                    result = true;
                }
                else
                {
                    objEspeciesDAO.AgregarEspecie(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarEspecie(int idEspecie)
        {
            bool result = false;

            int x = objEspeciesDAO.EliminarEspecie(idEspecie);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Razas
        public ActionResult Razas()
        {
            List<EspeciesBO> Especies = objEspeciesDAO.ObteterListaEspecies().ToList();
            ViewBag.ListaDeEspecies = new SelectList(Especies, "id", "nomCientifico");
            return View();
        }

        public JsonResult ObtenerListaRazas()
        {
            List<RazasBO> PackRazas = objRazasDAO.ObtenerListaRazas().ToList();
            return Json(PackRazas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerRaza(int idRaza)
        {
            List<RazasBO> PackRazas = objRazasDAO.ObtenerRaza(idRaza).ToList();
            return Json(PackRazas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarRaza(RazasBO model)
        {
            var result = false;
            try
            {
                if (model.id > 0)
                {
                    objRazasDAO.ActulizarRazo(model);
                    result = true;
                }
                else
                {
                    objRazasDAO.AgregarRaza(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarRaza(int idRaza)
        {
            bool result = false;

            int x = objRazasDAO.EliminarRaza(idRaza);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Comercios
        public ActionResult Comercios()
        {
            return View();
        }


        //Vacunas
        public ActionResult Vacunas()
        {
            return View();
        }

        public JsonResult ObtenerListasVacunas()
        {
            List<VacunasBO> PackVacunas = objVacunasDAO.ObtenerListaVacunas().ToList();
            return Json(PackVacunas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerVacuna(int idVacuna)
        {
            List<VacunasBO> PackVacunas = objVacunasDAO.ObtenerVacuna(idVacuna).ToList();
            return Json(PackVacunas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarVacuna(VacunasBO model)
        {
            var result = false;
            try
            {
                if (model.id > 0)
                {
                    objVacunasDAO.ActualizarVacuna(model);
                    result = true;
                }
                else
                {
                    objVacunasDAO.AgregarVacuna(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarVacuna(int idVacuna)
        {
            bool result = false;

            int x = objVacunasDAO.EliminarVacuna(idVacuna);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Ciudades
        public ActionResult Ciudades()
        {
            List<EstadosBO> Estados = objRegionesDAO.DropDownEstado().ToList();
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


        //Estados
        public ActionResult Estados()
        {
            List<PaisesBO> Paises = objRegionesDAO.DropDownPais().ToList();
            ViewBag.ListaDePaises = new SelectList(Paises, "id", "nombre");
            return View();
        }

        public JsonResult ObtenerListaEstados()
        {
            List<RegionesBO> PackEstados = objRegionesDAO.ListaEstados().ToList();
            return Json(PackEstados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerEstado(int idEstado)
        {
            List<RegionesBO> PackEstados = objRegionesDAO.ObtenertEstado(idEstado).ToList();
            return Json(PackEstados, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarEstados(RegionesBO model)
        {
            var result = false;
            try
            {
                if (model.estado.id > 0)
                {
                    objRegionesDAO.ActualizarEstado(model);
                    result = true;
                }
                else
                {
                    objRegionesDAO.AgregarEstado(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarEstado(int idEstado)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarEstado(idEstado);
            if (x != 0)
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Paises
        public ActionResult Paises()
        {
            return View();
        }

        public JsonResult ObtenerListaPaises()
        {
            List<RegionesBO> PackPaises = objRegionesDAO.ListaPaises().ToList();
            return Json(PackPaises, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerPais(int idPais)
        {
            List<RegionesBO> PackPaises = objRegionesDAO.ObtenerPais(idPais).ToList();
            return Json(PackPaises, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarPaises(RegionesBO model)
        {
            var result = false;
            try
            {
                if (model.pais.id > 0)
                {
                    objRegionesDAO.ActualizarPais(model);
                    result = true;
                }
                else
                {
                    objRegionesDAO.AgregarPais(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EliminarPais(int idPais)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarPais(idPais);
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


        //Comentarios
        public ActionResult Comentarios()
        {
            return View();
        }
    }
}