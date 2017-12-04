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
            List<ComercioBO> Comercios = objAgenda.ObtenerComercios().ToList();
            ViewBag.ListaComercio = new SelectList(Comercios, "id", "nombreComercial");
            List<MascotasBO> Mascotas = objAgenda.ObtenerMisMascotas((int)Session["id"]).ToList();
            ViewBag.ListaMascotas = new SelectList(Mascotas, "id", "nombremascota");
            return View();
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
    }
}