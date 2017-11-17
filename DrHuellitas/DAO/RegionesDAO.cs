using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class RegionesDAO
    {
        ConexionSQL con = new ConexionSQL();

        //Metodos para paises

        public int AgregarPais(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Pais (nombre) VALUES (@nombre)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.pais.nombre;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarPais(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Pais SET nombre=@nombre WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.pais.nombre;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.pais.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarPais(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Pais WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<RegionesBO> ListaPaises()
        {
            var paises = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Pais");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        pais = new PaisesBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombre = dr["nombre"].ToString()
                        }
                    };
                    paises.Add(p);
                }
            }
            return paises;
        }

        public List<RegionesBO> ObtenerPais(int id)
        {
            var pais = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Pais WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        pais = new PaisesBO
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString()
                        }
                    };
                    pais.Add(p);
                }
            }
            return pais;
        }

        //Metodos para Estados

        public int AgregarEstado(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Estado (nombre, idPais) VALUES (@nombre,@idPais)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.estado.nombre;
            cmd.Parameters.Add("@idPais", SqlDbType.Int).Value = objBO.estado.idPais;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarEstado(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Estado SET nombre=@nombre, idPais=@idPais WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.estado.nombre;
            cmd.Parameters.Add("@idPais", SqlDbType.Int).Value = objBO.estado.idPais;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.estado.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarEstado(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Estado WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<RegionesBO> ListaEstados()
        {
            var estados = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Estado");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        estado = new EstadosBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            idPais = Convert.ToInt32(dr["idPais"].ToString())
                        }
                    };
                    estados.Add(p);
                }
            }
            return estados;
        }

        public List<RegionesBO> ObtenertEstado(int id)
        {
            var estado = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Estado WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        estado = new EstadosBO
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString(),
                            idPais = Convert.ToInt32(dr["idPais"].ToString())
                        }
                    };
                    estado.Add(p);
                }
            }
            return estado;
        }

        //Medotos para ciudades

        public int AgregarCiudad(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Ciudad (nombre, idestado) VALUES (@nombre,@idestado)");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.estado.nombre;
            cmd.Parameters.Add("@idestado", SqlDbType.Int).Value = objBO.ciudad.idEstado;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarCiudad(RegionesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Ciudad SET nombre=@nombre, idestado=@isestado WHERE id=@id");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.ciudad.nombre;
            cmd.Parameters.Add("@idestado", SqlDbType.Int).Value = objBO.ciudad.idEstado;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.ciudad.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarCiudad(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Ciudad WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<RegionesBO> ListaCiudades()
        {
            var ciudades = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Ciudad");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        ciudad = new CiudadesBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            idEstado = Convert.ToInt32(dr["idestado"].ToString())
                        }
                    };
                    ciudades.Add(p);
                }
            }
            return ciudades;
        }

        public List<RegionesBO> ObtenerCiudad(int id)
        {
            var ciudad = new List<RegionesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Ciudad WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegionesBO
                    {
                        ciudad = new CiudadesBO
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre = dr["nombre"].ToString(),
                            idEstado = Convert.ToInt32(dr["idestado"].ToString())
                        }
                    };
                    ciudad.Add(p);
                }
            }
            return ciudad;
        }
    }
}