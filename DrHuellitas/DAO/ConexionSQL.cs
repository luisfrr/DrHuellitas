using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DrHuellitas.DAO
{
    public class ConexionSQL
    {
        SqlCommand ComandoSQL;
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter adaptador;

        public ConexionSQL()
        {
            //Luis
           string cadena = @"Data Source=DESKTOP-LMFOHUK\LFRR;Initial Catalog=PHuellitas;Integrated Security=True";
            //Gerardo
            //string cadena = "Data Source=LAPTOP-P4OEGADG\\SQLEXPRESS;Initial Catalog=PHuellitas;Integrated Security=True";
            //Willy
            //string cadena = "Data Source=LAPTOP-ELH53H70\\WILLYSERVER;Initial Catalog=PHuellitas;Integrated Security=True";
            con = new SqlConnection(cadena);
            adaptador = new SqlDataAdapter();
            ComandoSQL = new SqlCommand();
        }

        public SqlConnection establecerConexion()
        {
            return this.con;
        }

        public void AbrirConexion()
        {
            this.con.Open();
        }

        public void CerrarConexion()
        {
            this.con.Close();
        }

        public DataTable EjecutarSentencia(SqlCommand SqlComando)//SELECT
        {
            dt = new DataTable();
            ComandoSQL = SqlComando;
            ComandoSQL.Connection = this.establecerConexion();
            this.AbrirConexion();
            adaptador.SelectCommand = ComandoSQL;
            adaptador.Fill(dt);
            this.CerrarConexion();
            return dt;
        }

        public int EjecutarComando(SqlCommand SqlComando)//INSERT,UPDATE, DELETE
        {
            try
            {
                ComandoSQL = SqlComando;
                ComandoSQL.Connection = this.establecerConexion();
                this.AbrirConexion();
                int id = 0; id = Convert.ToInt32(ComandoSQL.ExecuteScalar());
                this.CerrarConexion();
                return 1;
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                this.CerrarConexion();
            }
        }

        /*public int EjecutarComando(SqlCommand SqlComando)//INSERT,UPDATE, DELETE
        {

            ComandoSQL = SqlComando;
            ComandoSQL.Connection = this.establecerConexion();
            this.AbrirConexion();
            int id = 0; id = Convert.ToInt32(ComandoSQL.ExecuteScalar());
            this.CerrarConexion();
            return id;

        }*/
    }
}