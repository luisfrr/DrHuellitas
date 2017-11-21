using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class RazasDAO
    {
        ConexionSQL con = new ConexionSQL();

        public int AgregarRaza(RazasBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Raza(nombre, idEspecie) VALUES(@nombre,@idEspecie)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;
            cmd.Parameters.Add("@idEspecie", SqlDbType.Int).Value = objBO.idEspecie;

            return con.EjecutarComando(cmd);
        }

        public int ActulizarRazo(RazasBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Raza SET nombre=@nombre,idEspecie=@idEspecie WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;
            cmd.Parameters.Add("@idEspecie", SqlDbType.Int).Value = objBO.idEspecie;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarRaza(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Raza WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<RazasBO> ObtenerListaRazas()
        {
            var razas = new List<RazasBO>();
            SqlCommand cmd = new SqlCommand("SELECT r.id, r.nombre, r.idEspecie, e.nomCientifico AS nomEspecie FROM Raza r JOIN Especie e ON e.id = r.idEspecie");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var r = new BO.RazasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        idEspecie = Convert.ToInt32(dr["idEspecie"].ToString()),
                        nomEspecie = dr["nomEspecie"].ToString()
                    };
                    razas.Add(r);
                }

            }
            con.CerrarConexion();
            return razas;
        }


        public List<RazasBO> ObtenerRaza(int id)
        {
            var raza = new List<RazasBO>();
            SqlCommand cmd = new SqlCommand("SELECT r.id, r.nombre, r.idEspecie, e.nomCientifico AS nomEspecie FROM Raza r JOIN Especie e ON e.id = r.idEspecie WHERE r.id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var r = new BO.RazasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString(),
                        idEspecie = Convert.ToInt32(dr["idEspecie"].ToString()),
                        nomEspecie = dr["nomEspecie"].ToString()
                    };
                    raza.Add(r);
                }
            }
            con.CerrarConexion();
            return raza;
        }

        

    }
}