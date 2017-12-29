using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using DrHuellitas.BO;
using DrHuellitas.DAO;
using Newtonsoft.Json;

namespace DrHuellitas.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioDAO objDAO = new UsuarioDAO();
        PuntosVeterinariaDAO obtener = new PuntosVeterinariaDAO();
        PropagandaUsuarioDAO obtnerpropaganda = new PropagandaUsuarioDAO();
        FotoBO objFoto = new FotoBO();
        AgendaDAO objAgenda = new AgendaDAO();
        ListarVeterinariasDAO objlistar = new ListarVeterinariasDAO();
        MascotasDAO objMascotas = new MascotasDAO();
        ComercioDAO objcomercio = new ComercioDAO();
        MascotasDAO objMascotasDAO = new MascotasDAO();
        CartillaDAO objCartillaDAO = new CartillaDAO();


        // GET: Usuario
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
                    if ((int)Session["status"] == 1)
                        return View();
                    else
                        modulo = "~/Usuario/Continuar";
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

        public ActionResult Continuar()
        {
            string modulo = "";
            if(Session["id"]!= null)
            {
                if ((int)Session["idtipo"] == 1)
                {
                    modulo = "~/Admin/Index";
                }
                else if ((int)Session["idtipo"] == 2)
                {
                    if ((int)Session["status"] == 1)
                        modulo = "~/Usuario/Index";
                    else
                        return View();
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

        public ActionResult ContinuarRegistro(RegistrosBO objBO)
        {
            int id = (int)Session["id"];
            var r = objDAO.ContinuarRegistro(objBO, id);
            Session["foto"] = "data:image/jpeg;base64," + Convert.ToBase64String(objFoto.ConvertirAFoto(objBO.usuario.img));
            Session["nombre"] = objBO.usuario.nombre;
            Session["status"] = 1;
            return Redirect("~/Usuario/Index");
        }

        public JsonResult jsonpropaganda()
        {
            List<PropagandaBO> lista = obtnerpropaganda.listar().ToList();
            var json = Json(lista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public ActionResult Comercio()
        {
            
            return View(obtener.mostarpuntos());
        }

        public JsonResult mostrardescripcion(int id)
        {
            List<PropagandaBO> lista = obtnerpropaganda.listarconid(id).ToList();
            var json = Json(lista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        public ActionResult Agenda()
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
                    if ((int)Session["status"] == 1)
                    {
                        List<ComercioBO> Comercios = objAgenda.ObtenerComercios().ToList();
                        ViewBag.ListaComercio = new SelectList(Comercios, "id", "nombreComercial");
                        List<MascotasBO> Mascotas = objAgenda.ObtenerMisMascotas((int)Session["id"]).ToList();
                        ViewBag.ListaMascotas = new SelectList(Mascotas, "id", "nombremascota");
                        return View();
                    }
                    else
                        modulo = "~/Usuario/Continuar";
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

        public JsonResult GetEvents()
        {
            var events = objAgenda.GetEventsUser((int)Session["id"]).ToList();
            var json = new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        [HttpPost]
        public JsonResult SaveEvent(CitasBO e)
        {
            var status = false;

            if (e.id > 0)
            {
                //Update the event
                objAgenda.ActualizarCita(e, (int)Session["id"]);
                status = true;
            }
            else
            {
                objAgenda.AgregarCita(e, (int)Session["id"]);
                status = true;
            }

            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;

            var c = objAgenda.EliminarCita(eventID);
            status = true;
                

            return new JsonResult { Data = new { status = status } };
        }

        public ActionResult PerfilComercio(RegistrosBO obj)
        {
            int id = obj.comercio.id;
            return View(objlistar.listarveterinaria(id));
        }



        public ActionResult Mascotas()
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
                    if ((int)Session["status"] == 1)
                    {
                        List<RazasBO> Raza = objMascotasDAO.DropDownRaza().ToList();
                        ViewBag.ListaRaza = new SelectList(Raza, "id", "nombre");
                        List<EspeciesBO> Especie = objMascotasDAO.DropDownEspecie().ToList();
                        ViewBag.ListaEspecie = new SelectList(Especie, "id", "nomCientifico");
                        return View();
                    }
                    else
                        modulo = "~/Usuario/Continuar";
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

        public JsonResult ObtenerListaMascotas()
        {
            List<GestionMascotaBO> PackMascotas = objMascotasDAO.ObtenerListaMascotasUsuario((int)Session["id"]).ToList();
            var json = Json(PackMascotas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerMascota(int id)
        {
            List<GestionMascotaBO> PackMascotas = objMascotasDAO.ObtenerMascota(id).ToList();
            var json = Json(PackMascotas, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        string modulo = "";
        [HttpPost]
        public ActionResult ImagenMascota(GestionMascotaBO model)
        {

            int resultado = objMascotasDAO.ActualizarFoto(model);
            if (resultado != 0)
            {
                modulo = "~/Usuario/Mascotas";
                
            }
            else
            {
                modulo = "~/Usuario/Mascotas";
                
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
                    objMascotasDAO.ActualizarMascotasUsuario(model);
                    result = true;
                }
                else
                {
                    objMascotasDAO.AgregarMascotasUsuarios(model);
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

        public JsonResult EliminarMascota(int idMascota)
        {
            bool result = false;

            int x = objMascotasDAO.EliminarMascotas(idMascota, (int)Session["id"]);
            if (x != 0)
            {
                result = true;
            }

            var json = Json(result, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }


        public ActionResult Cartilla(int id)
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
                    if ((int)Session["status"] == 1)
                    {
                        ViewBag.idMascota = id;
                        ViewBag.nombremascota = objCartillaDAO.NombreMascota(id);
                        return View();
                    }
                    else
                        modulo = "~/Usuario/Continuar";
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

        public JsonResult ObtenerCartilla(int id)
        {
            List<GestionMascotaBO> PackCartilla = objCartillaDAO.ObtenerCartilla(id,(int)Session["id"]).ToList();
            var json = Json(PackCartilla, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult ObtenerHistorial(int id)
        {
            List<GestionMascotaBO> PackHistorial = objCartillaDAO.ObtenerHistorialClinico(id).ToList();
            var json = Json(PackHistorial, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
        public JsonResult Obtenerpropaganda(int id)
        {
            List<PropagandaBO> Packpropaganda = objlistar.listar(id).ToList();
            var json = Json(Packpropaganda, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
        public JsonResult comentario(int id)
        {
            List<comentariosBO> packcomentario = objlistar.comentario(id).ToList();
            var json = Json(packcomentario, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }
    }
}