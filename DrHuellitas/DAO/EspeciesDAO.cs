using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;
using System.Data.SqlClient;
using System.Data;

namespace DrHuellitas.DAO
{
    public class EspeciesDAO
    {
        ConexionSQL con = new ConexionSQL();
        
        public int AgregarEspecie(EspeciesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Especie (nomCientifico, nomComun) VALUES(@nomCientifico,@nomComun)");
            cmd.Parameters.Add("@nomCientifico", SqlDbType.VarChar).Value = objBO.nomCientifico;
            cmd.Parameters.Add("@nomComun", SqlDbType.VarChar).Value = objBO.nomComun;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarEspecie(EspeciesBO objBO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Especie SET nomCientifico=@nomCientifico,nomComun=@nomComun WHERE id=@id");
            cmd.Parameters.Add("@nomCientifico", SqlDbType.VarChar).Value = objBO.nomCientifico;
            cmd.Parameters.Add("@nomComun", SqlDbType.VarChar).Value = objBO.nomComun;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarEspecie(int id)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Especie WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return con.EjecutarComando(cmd);
        }

        public List<EspeciesBO> ObteterListaEspecies()
        {
            var especies = new List<EspeciesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Especie");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var e = new BO.EspeciesBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nomCientifico = dr["nomCientifico"].ToString(),
                        nomComun = dr["nomComun"].ToString()
                    };
                    especies.Add(e);
                }
            }
            con.CerrarConexion();
            return especies;
        }

        public List<EspeciesBO> ObtenerEspecie(int id)
        {
            var especies = new List<EspeciesBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Especie WHERE id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var e = new BO.EspeciesBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nomCientifico = dr["nomCientifico"].ToString(),
                        nomComun = dr["nomComun"].ToString()
                    };
                    especies.Add(e);
                }
            }
            con.CerrarConexion();
            return especies;
        }

    }
}