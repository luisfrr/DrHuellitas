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

        public List<PropagandaBO> obtenerpropaganda(int idusuario)
        {
            var propaganda = new List<PropagandaBO>();
            SqlCommand cmd = new SqlCommand("SELECT id,idUsuario,foto,descripcion, fecha FROM Propaganda where status=1 and idUsuario='" + idusuario + "' order by id desc");

            cmd.Connection = conex.establecerConexion();
            conex.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    String fotos = Convert.ToBase64String((byte[])dr["foto"]);
                    if (fotos == "0")
                    {

                        var p = new BO.PropagandaBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            idusuario = Convert.ToInt32(dr["iduUsuario"].ToString()),
                            foto = " ",
                            descripcion = dr["descripcion"].ToString(),
                            fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy")


                        };
                        propaganda.Add(p);
                    }
                    else
                    {
                        var p = new BO.PropagandaBO
                        {


                            id = Convert.ToInt32(dr["id"].ToString()),
                            idusuario = Convert.ToInt32(dr["idUsuario"].ToString()),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            descripcion = dr["descripcion"].ToString(),
                            fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy")
                        };
                        propaganda.Add(p);
                    }

                }
            }
            return propaganda;
        }
    }
}