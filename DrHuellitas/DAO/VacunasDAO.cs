using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;
using System.Data.SqlClient;
using System.Data;

namespace DrHuellitas.DAO
{
    public class VacunasDAO
    {
        ConexionSQL con = new ConexionSQL();

        public int AgregarVacuna(VacunasBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Vacuna (nombre) VALUES(@nombre)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarVacuna(VacunasBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Vacuna SET nombre=@nombre WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarVacuna(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Vacuna WHERE id=@id");
            cmd.Parameters.Add("id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<VacunasBO> ObtenerListaVacunas()
        {
            var vacunas = new List<VacunasBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Vacuna");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var v = new BO.VacunasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    vacunas.Add(v);
                }

            }
            con.CerrarConexion();
            return vacunas;
        }

        public List<VacunasBO> ObtenerVacuna(int id)
        {
            var vacuna = new List<VacunasBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Vacuna WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var v = new BO.VacunasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    vacuna.Add(v);
                }
            }
            con.CerrarConexion();
            return vacuna;
        }
    }
}