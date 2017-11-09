using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DrHuellitas.DAO
{
    public class ConexionDAO
    {
        SqlConnection con;
        SqlCommand exec;

        //constructor
        public ConexionDAO()
        {
            con = new SqlConnection("server=DESKTOP-JE5SO9B\\SQLEXPRESS; database=Dr.Huellitas ; integrated security = true;");
            //sirve para establecer las consultas e instrucciones SQL que se ejecutarán en el servidor
            exec = new SqlCommand();

        }

        //método para abrir conexión

        public void abrirConexion()
        {
            this.con.Open();

        }

        //método para cerrar conexión
        public void cerrarConexion()
        {
            this.con.Close();
        }

        public int ejecutarSentencia(String strSql) //insert,update, delete
        {
            try
            {
                //donde se asigna el texto de la instrucción SQL a ser ejecutada en el servidor
                this.exec.CommandText = strSql;
                //donde se asigna el objeto de conexión construido con SqlConnection
                this.exec.Connection = this.con;
                this.abrirConexion();
                // indicar la ejecución de la instrucción SQL definida en CommandText, devuelve un valor entero que indica las filas afectadas
                this.exec.ExecuteNonQuery();
                this.cerrarConexion();
                return 1;

            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                this.cerrarConexion();
            }

        }

        public DataTable EjercutarSentenciaBus(String strSql) //SELECT
        {
            SqlDataAdapter adapter = new SqlDataAdapter(strSql, this.con);
            DataTable tabla = new DataTable();
            //rellenar un objeto DataSet con los resultados del elemento SelectCommand
            adapter.Fill(tabla);
            return tabla;
        }

        /*public List<AlumnoBO> EjecutarSetencialist(String strSql)
        {
            var alumnos = new List<AlumnoBO>();
            this.abrirConexion();
            var query = new SqlCommand(strSql, this.con);
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    // Usuario
                    var usuario = new AlumnoBO
                    {
                        id = Convert.ToInt32(dr["id"]),
                        nombre = dr["nombre"].ToString(),
                        ape_paterno = dr["ape_paterno"].ToString(),
                        ape_materno = dr["ape_materno"].ToString(),
                        matricula = dr["matricula"].ToString(),
                    };

                    // Agregamos el usuario a la lista genreica
                    alumnos.Add(usuario);
                }
            }
            this.cerrarConexion();
            return alumnos;
        }*/
    }
}