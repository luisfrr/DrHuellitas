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
        PropagandaDAO obpropagandadao = new PropagandaDAO();
        EspeciesDAO objEspeciesDAO = new EspeciesDAO();
        RazasDAO objRazasDAO = new RazasDAO();
        VacunasDAO objVacunasDAO = new VacunasDAO();
        RegistrosBO model = new RegistrosBO();
        MascotasDAO objMascotasDAO = new MascotasDAO();
        ComercioDAO objcomercio = new ComercioDAO();

        // GET: Gestiones

        //Usuarios
        public ActionResult Usuarios()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    List<TipoUsuarioBO> TipoUser = objUsuariosDAO.DropDownTipoUs().ToList();
                    ViewBag.ListaTipoUs = new SelectList(TipoUser, "id", "nombre");
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

        public JsonResult ObtenerListaUsuarios()
        {
            List<RegistrosBO> PackUsuarios = objUsuariosDAO.ObtenerListaUsuarios().ToList();
            var json = Json(PackUsuarios, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerUsuario(int idUsuario)
        {
            List<RegistrosBO> PackUsuarios = objUsuariosDAO.ObtenerUsuario(idUsuario).ToList();
            var json=  Json(PackUsuarios, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        string modulo = "";
        public ActionResult SubirImagen(RegistrosBO model)
        {
            
            int resultado = objUsuariosDAO.ActualizarFoto(model);
            
            modulo = "~/Gestiones/Usuarios";

            return Redirect(modulo);
        }

        public JsonResult EliminarUsuario(int idUsuario)
        {
            bool result = false;

            int x = objUsuariosDAO.EliminarUsuario(idUsuario);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Mascotas
        public ActionResult Mascotas()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    List<UsuarioBO> Usuario = objMascotasDAO.DropDownUsuario().ToList();
                    ViewBag.ListaUsuario = new SelectList(Usuario, "id", "nombre");
                    List<RazasBO> Raza = objMascotasDAO.DropDownRaza().ToList();
                    ViewBag.ListaRaza = new SelectList(Raza, "id", "nombre");
                    List<EspeciesBO> Especie = objMascotasDAO.DropDownEspecie().ToList();
                    ViewBag.ListaEspecie = new SelectList(Especie, "id", "nomCientifico");
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

        public JsonResult ObtenerDropDownRaza(int id)
        {
            List<RazasBO> Dropdown = objMascotasDAO.DropDownRaza(id).ToList();
            var listaraza = new SelectList(Dropdown, "id", "nombre");
            var json = Json(listaraza, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerListaMascotas()
        {
            List<GestionMascotaBO> PackMascotas = objMascotasDAO.ObtenerListaMascotas().ToList();
            var json = Json(PackMascotas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerMascota(int idMascota)
        {
            List<GestionMascotaBO> PackMascotas = objMascotasDAO.ObtenerMascota(idMascota).ToList();
            var json = Json(PackMascotas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        [HttpPost]
        public ActionResult ImagenMascota(GestionMascotaBO model)
        {

            int resultado = objMascotasDAO.ActualizarFoto(model);
            if (resultado != 0)
            {
                modulo = "~/Gestiones/Mascotas";
                ViewBag.ImagenBien = true;
            }
            else
            {
                modulo = "~/Gestiones/Mascotas";
                ViewBag.ImagenMal = true;
            }

            return Redirect(modulo);
        }

        public JsonResult GuardarMascota(GestionMascotaBO model)
        {
            var result = false;
            try
            {
                if (model.mascotas.id > 0)
                {
                    objMascotasDAO.ActualizarMascotas(model, (int)Session["id"]);
                    result = true;
                }
                else
                {
                    objMascotasDAO.AgregarMascotas(model);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarMascota(int idMascota,int idUsuario)
        {
            bool result = false;

            int x = objMascotasDAO.EliminarMascotas(idMascota,idUsuario);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Especies 
        public ActionResult Especies()
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

        public JsonResult ObtenerListaEspecies()
        {
            List<EspeciesBO> PackEspecies = objEspeciesDAO.ObteterListaEspecies().ToList();
            var json = Json(PackEspecies, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerEspecie(int idEspecie)
        {
            List<EspeciesBO> PackEspecies = objEspeciesDAO.ObtenerEspecie(idEspecie).ToList();
            var json = Json(PackEspecies, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarEspecie(int idEspecie)
        {
            bool result = false;

            int x = objEspeciesDAO.EliminarEspecie(idEspecie);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        //Razas
        public ActionResult Razas()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    List<EspeciesBO> Especies = objEspeciesDAO.ObteterListaEspecies().ToList();
                    ViewBag.ListaDeEspecies = new SelectList(Especies, "id", "nomCientifico");
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

        public JsonResult ObtenerListaRazas()
        {
            List<RazasBO> PackRazas = objRazasDAO.ObtenerListaRazas().ToList();
            var json = Json(PackRazas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerRaza(int idRaza)
        {
            List<RazasBO> PackRazas = objRazasDAO.ObtenerRaza(idRaza).ToList();
            var json = Json(PackRazas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarRaza(int idRaza)
        {
            bool result = false;

            int x = objRazasDAO.EliminarRaza(idRaza);
            if (x != 0)
            {
                result = true;
            }

            var json= Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Vacunas
        public ActionResult Vacunas()
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

        public JsonResult ObtenerListasVacunas()
        {
            List<VacunasBO> PackVacunas = objVacunasDAO.ObtenerListaVacunas().ToList();
            var json = Json(PackVacunas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerVacuna(int idVacuna)
        {
            List<VacunasBO> PackVacunas = objVacunasDAO.ObtenerVacuna(idVacuna).ToList();
            var json = Json(PackVacunas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarVacuna(int idVacuna)
        {
            bool result = false;

            int x = objVacunasDAO.EliminarVacuna(idVacuna);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        //Ciudades
        public ActionResult Ciudades()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    List<EstadosBO> Estados = objRegionesDAO.DropDownEstado().ToList();
                    ViewBag.ListaDeEstados = new SelectList(Estados, "id", "nombre");
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

        public JsonResult ObtenerListaCiudades()
        {
            List<RegionesBO> PackCiudades = objRegionesDAO.ListaCiudades().ToList();
            var json = Json(PackCiudades, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerCiudad(int idCiudad)
        {
            List<RegionesBO> PackCiudades = objRegionesDAO.ObtenerCiudad(idCiudad).ToList();
            var json = Json(PackCiudades, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarCiudad(int idCiudad)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarCiudad(idCiudad);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Estados
        public ActionResult Estados()
        {
            string modulo = "";
            if (Session["id"] != null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    List<PaisesBO> Paises = objRegionesDAO.DropDownPais().ToList();
                    ViewBag.ListaDePaises = new SelectList(Paises, "id", "nombre");
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

        public JsonResult ObtenerListaEstados()
        {
            List<RegionesBO> PackEstados = objRegionesDAO.ListaEstados().ToList();
            var json = Json(PackEstados, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerEstado(int idEstado)
        {
            List<RegionesBO> PackEstados = objRegionesDAO.ObtenertEstado(idEstado).ToList();
            var json = Json(PackEstados, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarEstado(int idEstado)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarEstado(idEstado);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Paises
        public ActionResult Paises()
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

        public JsonResult ObtenerListaPaises()
        {
            List<RegionesBO> PackPaises = objRegionesDAO.ListaPaises().ToList();
            var json = Json(PackPaises, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerPais(int idPais)
        {
            List<RegionesBO> PackPaises = objRegionesDAO.ObtenerPais(idPais).ToList();
            var json = Json(PackPaises, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarPais(int idPais)
        {
            bool result = false;

            int x = objRegionesDAO.EliminarPais(idPais);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Paquetes
        public ActionResult Paquetes()
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

        public JsonResult ObtenerListaPaquetes()
        {
            List<PaquetesBO> PackList = objPaqueteDAO.ListPaquetes().ToList();
            var json = Json(PackList, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerPaquete(int idPaquete)
        {
            List<PaquetesBO> PackList = objPaqueteDAO.ObtenerPaquete(idPaquete).ToList();
            var json = Json(PackList, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
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

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult EliminarPaquete(int idPaquete)
        {
            bool result = false;

            int x = objPaqueteDAO.EliminarPaquete(idPaquete);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        //Comentarios
        public ActionResult Comentarios()
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

        //Propaganda
        public ActionResult Propaganda()
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

        public JsonResult propagandaadmin()
        {
            List<PropagandaBO> packlista = obpropagandadao.adminpropaganda().ToList();
            var json = Json(packlista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
        public ActionResult propagandaactualizar(int id)
        {
            var actualizar = obpropagandadao.actualizarpropaganda(id);
            return Redirect("~/Gestiones/Propaganda");
        }
        public JsonResult mostarrr(int id)
        {
            List<PropagandaBO> packlista = objcomercio.unapropaganda(id).ToList();
            var json = Json(packlista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        //propagandas propagandas
        public ActionResult propagandades()
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

        public JsonResult propagandasaprovadas()
        {
            List<PropagandaBO> packlista = obpropagandadao.admin().ToList();
            var json = Json(packlista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
        public JsonResult mostra(int id)
        {
            List<PropagandaBO> packlista = objcomercio.unapropaganda(id).ToList();
            var json = Json(packlista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
        public ActionResult desprovar(int id)
        {
            var desaprovar = obpropagandadao.adminActualizar(id);
            return Redirect("~/Gestiones/propagandades");
        }
    }
}