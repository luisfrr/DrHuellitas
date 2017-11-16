using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class PaquetesDAO
    {
        ConexionSQL con = new ConexionSQL();

        public int AgregarPaquete(PaquetesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Paquetes (nombre,precio,descripcion) VALUES(@nombre,@precio,@descripcion)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;
            cmd.Parameters.Add("@precio", SqlDbType.Real).Value = objBO.precio;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = objBO.descripcion;

            return con.EjecutarComando(cmd);
        }

        public int ModificarPaquete(PaquetesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Paquetes SET nombre=@nombre,precio=@precio,descripcion=@precio WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.nombre;
            cmd.Parameters.Add("@precio", SqlDbType.Real).Value = objBO.precio;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = objBO.descripcion;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarPaquete(int idPaquete)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Paquetes WHERE id=@id");
            cmd.Parameters.Add("id", SqlDbType.Int).Value = idPaquete;

            return con.EjecutarComando(cmd);
        }

        public List<PaquetesBO> ListPaquetes()
        {
            var paquetes = new List<PaquetesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Paquetes");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.PaquetesBO
                    {
                        id = Convert.ToInt32(dr["id"]),
                        nombre = dr["nombre"].ToString(),
                        precio = Convert.ToDouble(dr["precio"].ToString()),
                        descripcion = dr["descripcion"].ToString()
                    };
                    paquetes.Add(p);
                }
            }
            return paquetes;
        }

        public List<PaquetesBO> ObtenerPaquete(int id)
        {
            var paquetes = new List<PaquetesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Paquetes WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.PaquetesBO
                    {
                        id = Convert.ToInt32(dr["id"]),
                        nombre = dr["nombre"].ToString(),
                        precio = Convert.ToDouble(dr["precio"].ToString()),
                        descripcion = dr["descripcion"].ToString()
                    };
                    paquetes.Add(p);
                }
            }
            return paquetes;
        }

        /*public DataTable ObtenerPaquete(int idPaquete)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Paquetes WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idPaquete;

            return con.EjecutarSentencia(cmd);
        }*/


    }
}