using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class PuntosVeterinariaDAO
    {
        ConexionSQL conex = new ConexionSQL();

        public List<PuntosdeUbicacionBO> mostarpuntos()
        {
            var puntos = new List<PuntosdeUbicacionBO>();
            SqlCommand cmd = new SqlCommand("select d.longitud,d.latitud,d.ubicacion,d.id,u.nombre + '' + u.apellidos as datos, u.foto, c.nombreComercial, c.nombreFiscal, c.email, c.telefono1 from Usuario u join UsuarioComercio uc on uc.idempresa = u.id join Comercio c on c.id = uc.idsucursal join Direccion d on u.id = d.idUsuario");

            cmd.Connection = conex.establecerConexion();
            conex.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.PuntosdeUbicacionBO
                    {
                        direccion = new BO.DireccionBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            longitud = dr["longitud"].ToString(),
                            latitud = dr["latitud"].ToString(),
                            ubicacion = dr["ubicacion"].ToString(),
                        },

                        usuarios = new UsuarioBO
                        {
                            nombre = dr["datos"].ToString(),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),

                        },
                        comercio = new ComercioBO
                        {
                            nombreComercial = dr["nombreComercial"].ToString(),
                            nombreFiscal = dr["nombreFiscal"].ToString(),
                            emal = dr["email"].ToString(),
                            telefono1 = dr["telefono1"].ToString()
                        }

                    };


                    puntos.Add(p);

                }
            }
            return puntos;
        }
    }
}