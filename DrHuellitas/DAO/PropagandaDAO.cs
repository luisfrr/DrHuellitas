using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DrHuellitas.BO;
using Newtonsoft.Json;

namespace DrHuellitas.DAO
{
    public class PropagandaDAO
    {
        ConexionSQL conex = new ConexionSQL();
        //lista de propagandas no aprovadas
        public List<PropagandaBO> adminpropaganda()
        {
            var propaganda =new List<PropagandaBO>();

            SqlCommand cmd = new SqlCommand("select p.id, p.foto,p.descripcion, c.nombreComercial as comercio from Propaganda p join Usuario u on p.idUsuario=u.id join UsuarioComercio uc on u.id=uc.idempresa join Comercio c on c.id=uc.idsucursal where p.status=0");
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
                            foto = " ",
                            descripcion = dr["descripcion"].ToString(),
                            nombrecomercio = dr["comercio"].ToString()


                        };
                        propaganda.Add(p);
                    }
                    else
                    {
                        var p = new BO.PropagandaBO
                        {


                            id = Convert.ToInt32(dr["id"].ToString()),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            descripcion = dr["descripcion"].ToString(),
                            nombrecomercio = dr["comercio"].ToString()
                        };
                        propaganda.Add(p);
                    }

                }
            }
            return propaganda;

        }
        public int actualizarpropaganda(int id)
        {
            SqlCommand cmd =new SqlCommand("update Propaganda set status=1 where id='" +id+ "'");
            return conex.EjecutarComando(cmd);
        }







        //lista de propagandas aprovadas
        public List<PropagandaBO> admin()
        {
            var propaganda = new List<PropagandaBO>();

            SqlCommand cmd = new SqlCommand("select p.id, p.foto,p.descripcion, c.nombreComercial as comercio from Propaganda p join Usuario u on p.idUsuario=u.id join UsuarioComercio uc on u.id=uc.idempresa join Comercio c on c.id=uc.idsucursal where p.status=1");
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
                            foto = " ",
                            descripcion = dr["descripcion"].ToString(),
                            nombrecomercio = dr["comercio"].ToString()


                        };
                        propaganda.Add(p);
                    }
                    else
                    {
                        var p = new BO.PropagandaBO
                        {


                            id = Convert.ToInt32(dr["id"].ToString()),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            descripcion = dr["descripcion"].ToString(),
                            nombrecomercio = dr["comercio"].ToString()
                        };
                        propaganda.Add(p);
                    }

                }
            }
            return propaganda;

        }


        public int adminActualizar(int id)
        {
            SqlCommand cmd = new SqlCommand("update Propaganda set status=0 where id='" + id + "'");
            return conex.EjecutarComando(cmd);
        }

    }
}