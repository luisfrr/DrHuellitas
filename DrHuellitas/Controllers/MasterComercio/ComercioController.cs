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
        FotoBO objFoto = new FotoBO();
        AgendaDAO objAgenda = new AgendaDAO();
        AgregarVeterinarioDAO objveterinario = new AgregarVeterinarioDAO();

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
            onda = objDAO.ComercioDatos(dato, id);
            Session["status"] = 1;
            return Redirect("~/Comercio/Index");

        }

        public ActionResult chat()
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

        public ActionResult Propaganda()
        {
            return View();
        }

        public ActionResult AgregarPropaganda(PropagandaBO obj)
        {
            int id = (int)Session["id"];
            var agregar = (obj.id > 0) ? objDAO.actualizar(obj) : objDAO.agregarpropaganda(obj, id);
            return Redirect("~/Comercio/Index");
        }
        public ActionResult eliminar(int id)
        {
            var agregar = objDAO.eliminarPropaganda(id);
            return Redirect("~/Comercio/Index");
        }

        public JsonResult Obtenerpropaganda()
        {
            int id = (int)Session["id"];
            List<PropagandaBO> Packpropaganda = objDAO.obtenerpropaganda(id).ToList();
            var json= Json(Packpropaganda, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult mostrar(int id)
        {
            List<PropagandaBO> obtenerlista = objDAO.unapropaganda(id).ToList();
            var json = Json(obtenerlista, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public JsonResult fotolista(int id)
        {
            List<PropagandaBO> obtenerfoto = objDAO.fotoperfil(id).ToList();
            var json = Json(obtenerfoto, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = Int32.MaxValue;
            return json;
        }

        public ActionResult modifcarfotoperfil(PropagandaBO obj)
        {
            int id = (int)Session["id"];
            var fotos = objDAO.modificarfoto(obj, id);
            Session["foto"] = null;
            String foto = "data:image/jpeg;base64," + Convert.ToBase64String(objFoto.ConvertirAFoto(obj.usuario.img));
            Session["foto"] =  foto;
            return Redirect("~/Comercio/Index");
        }

        public ActionResult Doctores()
        {
            return View();
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
                    modulo = ((int)Session["status"] == 1) ? "~/Usuario/Index" : "~/Usuario/Continuar";
                }
                else if ((int)Session["idtipo"] == 3)
                {
                    if ((int)Session["status"] == 1)
                    {
                        return View();
                    }
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

        public JsonResult GetEvents()
        {
            var events = objAgenda.GetEventsComercios((int)Session["id"]).ToList();
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


        public ActionResult agregarveterinario(RegistrosBO bO)
        {
            var e = objveterinario.agregarVeterinario(bO,(int)Session["id"]);
            
            return Redirect("~/Comercio/Doctores");
        }

    }
}