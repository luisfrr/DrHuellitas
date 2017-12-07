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
            SqlCommand cmd = new SqlCommand("select c.id, c.nombreComercial,c.veterinaria,c.estetica,c.venderProducto,c.nombreFiscal,c.telefono1,c.email,u.nombre + ' ' + u.apellidos as gerente,u.foto from Comercio c join UsuarioComercio uc on c.id = uc.idsucursal join Usuario u on u.id =uc.idempresa where c.id ='" + id+"'");
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
                            veterinaria = Convert.ToBoolean(dr["veterinaria"]),
                            estetica = Convert.ToBoolean(dr["estetica"]),
                            venderproducto = Convert.ToBoolean(dr["venderProducto"]),
                            nombreFiscal = dr["nombreFiscal"].ToString(),
                            telefono1 = dr["telefono1"].ToString(),
                            emal = dr["email"].ToString(),
                        },
                        usuario = new BO.UsuarioBO
                        {
                            nombre = dr["gerente"].ToString(),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"])


                        }

                    };
                    veterinaria.Add(pack);
                }
            }
            return veterinaria;
        }
    }
}