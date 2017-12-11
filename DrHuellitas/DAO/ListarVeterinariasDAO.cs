using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class ListarVeterinariasDAO
    {
        ConexionSQL conex = new ConexionSQL();

        public List<RegistrosBO> listarveterinaria(int id)
        {
            var veterinaria = new List<RegistrosBO>();
            SqlCommand cmd = new SqlCommand("select c.id, c.nombreComercial,c.veterinaria,c.estetica,c.venderProducto,c.nombreFiscal,c.telefono1,c.email,u.id as usuario,u.nombre + ' ' + u.apellidos as gerente,u.foto from Comercio c join UsuarioComercio uc on c.id = uc.idsucursal join Usuario u on u.id =uc.idempresa where c.id ='" + id+"'");
            cmd.Connection = conex.establecerConexion();
            conex.AbrirConexion();
            var query = cmd;

            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {

                    var pack = new BO.RegistrosBO
                    {
                        comercio = new BO.ComercioBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombreComercial = dr["nombreComercial"].ToString(),
                            vet = Convert.ToInt32(dr["veterinaria"].ToString()),
                            est = Convert.ToInt32(dr["estetica"].ToString()),
                            prod = Convert.ToInt32(dr["venderProducto"].ToString()),
                            nombreFiscal = dr["nombreFiscal"].ToString(),
                            telefono1 = dr["telefono1"].ToString(),
                            emal = dr["email"].ToString(),
                        },
                        usuario = new BO.UsuarioBO
                        {
                            id = Convert.ToInt32(dr["usuario"].ToString()),
                            nombre = dr["gerente"].ToString(),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"])


                        }

                    };
                    veterinaria.Add(pack);
                }
            }
            return veterinaria;
        }
        public List<PropagandaBO> listar(int id)
        {
            var propaganda = new List<PropagandaBO>();

            SqlCommand cmd = new SqlCommand("select p.id, p.foto,p.descripcion,p.fecha,u.foto as fotousuario, c.nombreComercial as comercio from Propaganda p join Usuario u on p.idUsuario=u.id join UsuarioComercio uc on u.id=uc.idempresa join Comercio c on c.id=uc.idsucursal where p.status=1 and u.id='"+id+"' order by p.id desc");
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
        public List<comentariosBO>comentario(int id)
        {
            var lista = new List<comentariosBO>();
            SqlCommand cmd = new SqlCommand("select c.id,c.comentario,(select nombre + ' ' + apellidos from Usuario, Comentarios where Usuario.id = Comentarios.idemisor) as emisor,(select foto from Usuario, Comentarios where usuario.id = Comentarios.idemisor) as fotoemisor from Comentarios c where c.idreceptor = '"+id+"'");
            cmd.Connection = conex.establecerConexion();
            conex.AbrirConexion();

            var query = cmd;


            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {


                    var p = new BO.comentariosBO
                    {
                        id=Convert.ToInt32(dr["id"].ToString()),
                        comentario=dr["comentario"].ToString(),
                        emisor=dr["emisor"].ToString(),
                        foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["fotoemisor"])
                    };
                        lista.Add(p);
                }
            }
            return lista;
        }
    }
}