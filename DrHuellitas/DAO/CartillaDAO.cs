using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;
using System.Data;
using System.Data.SqlClient;

namespace DrHuellitas.DAO
{
    public class CartillaDAO
    {
        ConexionSQL con = new ConexionSQL();

        public string NombreMascota(int id)
        {
            string resultado = "";
            SqlCommand cmd = new SqlCommand("SELECT mascota FROM CartillaMascota WHERE idmascota=@idmascota");
            cmd.Parameters.Add("@idmascota", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    resultado = dr["mascota"].ToString();
                }
            }
            con.CerrarConexion();
            return resultado;
        }

        public List<GestionMascotaBO> ObtenerCartilla(int idMascota, int idUsuario)
        {
            var cartilla = new List<GestionMascotaBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM CartillaMascota WHERE idmascota=@idmascota AND idusuario=@idusuario");
            cmd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@idmascota", SqlDbType.Int).Value = idMascota;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.GestionMascotaBO
                    {
                        mascotas = new BO.MascotasBO
                        {
                            id = Convert.ToInt32(dr["idmascota"].ToString()),
                            nombremascota = dr["mascota"].ToString(),
                            fnacimiento =Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy"),
                            sgenero = dr["genero"].ToString(),
                            foto = (dr["foto"].ToString() != "")?"data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]):"",
                            colorDominate = dr["color"].ToString()
                        },
                        especies = new BO.EspeciesBO
                        {
                            nomComun = dr["especie"].ToString()
                        },
                        razas = new BO.RazasBO
                        {
                            nombre = dr["raza"].ToString(),
                        },
                        usuarios = new BO.UsuarioBO
                        {
                            id = Convert.ToInt32(dr["idusuario"].ToString()),
                            nombre = dr["nomUsuario"].ToString(),
                            email = dr["email"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            usuario = dr["nickname"].ToString()
                        }
                    };
                    cartilla.Add(c);
                }
            }
            con.CerrarConexion();
            return cartilla;
        }


        public List<GestionMascotaBO> ObtenerHistorialClinico(int idMascota)
        {
            var historial = new List<GestionMascotaBO>();
            SqlCommand cmd = new SqlCommand("SELECT dc.id AS folio, dc.servicio, dc.notas, dc.firma, v.nombre AS vacuna, ci.fechaInicio AS fecha, comercio.nombreComercial AS comercio FROM DetalleCartilla dc JOIN Vacuna v ON v.id = dc.idvacuna JOIN Cartilla c ON c.id = dc.idcartilla JOIN CartillaMascotas cm ON cm.idCartilla = c.id JOIN Citas ci ON dc.idCita = ci.id JOIN UsuarioComercio uc ON uc.idempresa =ci.idComercio JOIN Comercio comercio ON comercio.id = uc.idsucursal WHERE cm.idMascota =@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idMascota;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.GestionMascotaBO
                    {
                        historial = new BO.HistorialClinicoBO
                        {
                            folio = Convert.ToInt32(dr["folio"].ToString()),
                            servicio = dr["servicio"].ToString(),
                            notas = dr["notas"].ToString(),
                            firma = (dr["firma"].ToString() != "") ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["firma"]) : "",
                            vacuna = dr["vacuna"].ToString(),
                            sfecha = Convert.ToDateTime(dr["fecha"]).ToString("dd/MM/yyyy"),
                            comercio = dr["comercio"].ToString()
                        }
                    };
                    historial.Add(p);
                }
            }
            con.CerrarConexion();
            return historial;
        }
        
    }
}