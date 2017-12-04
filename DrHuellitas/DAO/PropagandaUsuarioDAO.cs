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
    public class PropagandaUsuarioDAO
    {
        ConexionSQL conex = new ConexionSQL();

        public List<PropagandaBO> listar()
        {
            var propaganda = new List<PropagandaBO>();

            SqlCommand cmd = new SqlCommand("select p.id, p.foto,p.descripcion,p.fecha,u.foto as fotousuario, c.nombreComercial as comercio from Propaganda p join Usuario u on p.idUsuario=u.id join UsuarioComercio uc on u.id=uc.idempresa join Comercio c on c.id=uc.idsucursal where p.status=1 order by p.id desc");
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
                            fecha =Convert.ToDateTime( dr["fecha"]).ToString("dd-MM-yyyy"),
                            fotousuario = "",
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
                            fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy"),
                            fotousuario ="data:image/jpeg;base64,"+Convert.ToBase64String((byte[])dr["foto"]),
                            nombrecomercio = dr["comercio"].ToString()
                        };
                        propaganda.Add(p);
                    }

                }
            }
            return propaganda;
        }

        public List<PropagandaBO> listarconid(int id)
        {
            var propaganda =new List<PropagandaBO>();

            SqlCommand cmd = new SqlCommand("select p.id, p.foto,p.descripcion,p.fecha,u.foto as fotousuario, c.nombreComercial as comercio from Propaganda p join Usuario u on p.idUsuario=u.id join UsuarioComercio uc on u.id=uc.idempresa join Comercio c on c.id=uc.idsucursal where p.status=1 and p.id='"+id+"' order by p.id desc");
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
                            fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy"),
                            fotousuario = "",
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
                            fecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy"),
                            fotousuario = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["fotousuario"]),
                            nombrecomercio = dr["comercio"].ToString()
                        };
                        propaganda.Add(p);
                    }

                }
            }
            return propaganda;
        }
    }
}