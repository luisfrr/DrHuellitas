using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;
using System.Data;
using System.Data.SqlClient;

namespace DrHuellitas.DAO
{
    public class AgendaDAO
    {
        ConexionSQL con = new ConexionSQL();


        public int AgregarCita(CitasBO citas, int idUsuario)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Citas (idComercio, idMascota, idUsuario, titulo, descripcion,fechaInicio, fechaFin, status) VALUES(@idComercio, @idMascota, @idUsuario, @titulo, @descripcion, @fechaInicio, @fechaFin, @status)");
            cmd.Parameters.Add("@idComercio", SqlDbType.Int).Value = citas.comercio.id;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = citas.sInicio;
            cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = citas.sFin;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = citas.descripcion;
            cmd.Parameters.Add("@idMascota", SqlDbType.Int).Value = citas.mascotas.id;
            cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = citas.titulo;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = 0;
            

            return con.EjecutarComando(cmd);
        }

        public int ActualizarCita(CitasBO citas, int idUsuario)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Citas SET idComercio=@idComercio, idUsuario=@idUsuario, fechaInicio=@fechaInicio, fechaFin=@fechaFin, descripcion=@descripcion, idMascota=@idMascota, titulo=@titulo, status=0 WHERE id=@id");
            cmd.Parameters.Add("@idComercio", SqlDbType.Int).Value = citas.comercio.id;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = citas.sInicio;
            cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = citas.sFin;
            cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = citas.descripcion;
            cmd.Parameters.Add("@idMascota", SqlDbType.Int).Value = citas.mascotas.id;
            cmd.Parameters.Add("@titulo", SqlDbType.VarChar).Value = citas.titulo;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = citas.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarCita(int idCita)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Citas WHERE id = @id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idCita;

            return con.EjecutarComando(cmd);
        }

        public List<MascotasBO> ObtenerMisMascotas(int id)
        {
            var mascotas = new List<MascotasBO>();
            SqlCommand cmd = new SqlCommand("SELECT um.idMascota, m.nombre FROM UsuarioMascota um JOIN Mascotas m ON m.id = um.idmascota WHERE idUsuario =@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.MascotasBO
                    {
                        id = Convert.ToInt32(dr["idMascota"].ToString()),
                        nombremascota = dr["nombre"].ToString(),
                    };
                    mascotas.Add(c);
                }
            }
            con.CerrarConexion();
            return mascotas;
        }

        public List<ComercioBO> ObtenerComercios()
        {
            var mascotas = new List<ComercioBO>();
            SqlCommand cmd = new SqlCommand("SELECT uc.idempresa, c.nombreComercial FROM UsuarioComercio uc JOIN Comercio c ON c.id = uc.idsucursal WHERE c.veterinaria = 1 OR c.estetica =1");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ComercioBO
                    {
                        id = Convert.ToInt32(dr["idempresa"].ToString()),
                        nombreComercial = dr["nombreComercial"].ToString(),
                    };
                    mascotas.Add(c);
                }
            }
            con.CerrarConexion();
            return mascotas;
        }


        public List<CitasBO> GetEventsUser(int id)
        {
            var citas = new List<CitasBO>();
            SqlCommand cmd = new SqlCommand("SELECT c.id,c.titulo, c.descripcion,c.color,c.fechaInicio, c.fechaFin, (SELECT nombreComercial FROM Comercio WHERE id = (SELECT idsucursal FROM UsuarioComercio WHERE idempresa = c.idComercio)) AS nombrecomercio, c.idComercio, (SELECT nombre FROM Mascotas WHERE id = c.idMascota) AS nombremascota,c.idMascota FROM Citas c WHERE c.idUsuario=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.CitasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        titulo = dr["titulo"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        inicio = Convert.ToDateTime(dr["fechaInicio"]),
                        fin = Convert.ToDateTime(dr["fechaFin"]),
                        color = dr["color"].ToString(),
                        comercio = new BO.ComercioBO
                        {
                            id = Convert.ToInt32(dr["idComercio"].ToString()),
                            nombreComercial = dr["nombrecomercio"].ToString()
                        },
                        mascotas = new BO.MascotasBO
                        {
                            id = Convert.ToInt32(dr["idMascota"].ToString()),
                            nombremascota = dr["nombremascota"].ToString()
                        }
                    };
                    citas.Add(c);
                }
            }
            con.CerrarConexion();
            return citas;
        }
    }
}