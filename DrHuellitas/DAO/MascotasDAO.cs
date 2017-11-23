using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class MascotasDAO
    {
        ConexionSQL con = new ConexionSQL();
        FotoBO Foto = new FotoBO();

        public int AgregarMascotas(GestionMascotaBO objBO)
        {
            SqlCommand cmd = new SqlCommand("EXEC GestionMascotas @nombre=@nombre,@CDomitante=@CDominante,@CPDominante=@CPDominante,@CAlternativo=@CAlternativo,@genero=@genero,@fechanacimiento=@fechanacimiento,@idRaza=@idRaza,@idUsuario=@idUsuario");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.mascotas.nombremascota;
            cmd.Parameters.Add("@CDomitante", SqlDbType.VarChar).Value = objBO.mascotas.colorDominate;
            cmd.Parameters.Add("@CPDominante", SqlDbType.VarChar).Value = objBO.mascotas.colorPreDominante;
            cmd.Parameters.Add("@CAlternativo", SqlDbType.VarChar).Value = objBO.mascotas.colorAlternativo;
            cmd.Parameters.Add("@genero", SqlDbType.VarChar).Value = objBO.mascotas.genero;
            cmd.Parameters.Add("@fechanacimiento", SqlDbType.Date).Value = objBO.mascotas.fechaNaci.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("@idRaza", SqlDbType.Int).Value = objBO.mascotas.idRaza;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = objBO.usuarios.id;

            return con.EjecutarComando(cmd);
        }

        public int ActualizarMascotas(GestionMascotaBO objBO)
        {
            SqlCommand cmd = new SqlCommand("EXEC ActualizarGestionMascotas @nombre=@nombre,@CDomitante=@CDominante,@CPDominante=@CPDominante,@CAlternativo=@CAlternativo,@genero=@genero,@fechanacimiento=@fechanacimiento,@foto=@foto,@idRaza=@idRaza,@idUsuario=@idUsuario,@idMascota=@idMascota");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.mascotas.nombremascota;
            cmd.Parameters.Add("@CDomitante", SqlDbType.VarChar).Value = objBO.mascotas.colorDominate;
            cmd.Parameters.Add("@CPDominante", SqlDbType.VarChar).Value = objBO.mascotas.colorPreDominante;
            cmd.Parameters.Add("@CAlternativo", SqlDbType.VarChar).Value = objBO.mascotas.colorAlternativo;
            cmd.Parameters.Add("@genero", SqlDbType.VarChar).Value = objBO.mascotas.genero;
            cmd.Parameters.Add("@fechanacimiento", SqlDbType.Date).Value = objBO.mascotas.fechaNaci.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("@foto", SqlDbType.Image).Value = Foto.ConvertirAFoto(objBO.mascotas.img);
            cmd.Parameters.Add("@idRaza", SqlDbType.Int).Value = objBO.mascotas.idRaza;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = objBO.usuarios.id;
            cmd.Parameters.Add("@idmascota", SqlDbType.Int).Value = objBO.mascotas.id;

            return con.EjecutarComando(cmd);

        }

        public int EliminarMascotas(int idmascota, int idusuario)
        {
            SqlCommand cmd = new SqlCommand("EXEC EliminarMascota @idUsuario = @idusuario, @idMascota = @idmascota");
            cmd.Parameters.Add("@idusuario", SqlDbType.Int).Value = idusuario;
            cmd.Parameters.Add("@idmascota", SqlDbType.Int).Value = idmascota;

            return con.EjecutarComando(cmd);
        }

        public List<GestionMascotaBO> ObtenerListaMascotas()
        {
            var mascotas = new List<GestionMascotaBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM vMascotas");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr= query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.GestionMascotaBO
                    {
                        mascotas = new MascotasBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombremascota = dr["nombre"].ToString(),
                            colorDominate = dr["color"].ToString(),
                            sgenero = dr["genero"].ToString(),
                            fechaNaci = Convert.ToDateTime(dr["fechanacimiento"].ToString()),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"])
                        },
                        especies = new EspeciesBO
                        {
                            nomCientifico = dr["especie"].ToString()
                        },
                        razas = new RazasBO
                        {
                            nombre = dr["raza"].ToString()
                        },
                        usuarios = new UsuarioBO
                        {
                            id = Convert.ToInt32(dr["idusuario"].ToString()),
                            nombre = dr["propietario"].ToString()
                        }
                    };
                    mascotas.Add(p);
                }
                
            }
            con.CerrarConexion();
            return mascotas;
        }


        public List<GestionMascotaBO> ObtenerMascota(int id)
        {
            var mascotas = new List<GestionMascotaBO>();
            SqlCommand cmd = new SqlCommand("SELECT m.id, m.nombre, m.CDominante, m.CPDominante, m.CAlternativo, m.genero, m.fechanacimiento, e.id AS idespecie, (e.nomCientifico + '(' + e.nomComun + ')') AS nombreespecie, r.id AS idraza , r.nombre AS nombreraza, m.foto, um.idusuario, (u.nombre + ' ' + apellidos) AS propietario FROM Mascotas m JOIN Raza r ON r.id=m.idRaza JOIN Especie e ON e.id=r.idEspecie JOIN UsuarioMascota um ON um.idmascota = m.id JOIN Usuario u ON u.id = um.idusuario WHERE m.id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.GestionMascotaBO
                    {
                        mascotas = new BO.MascotasBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombremascota = dr["nombre"].ToString(),
                            colorDominate = dr["CDominante"].ToString(),
                            colorPreDominante = dr["CPDominante"].ToString(),
                            colorAlternativo = dr["CAlternativo"].ToString(),
                            sgenero = dr["genero"].ToString(),
                            fnacimiento = Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy"),
                            idRaza = Convert.ToInt32(dr["idraza"].ToString()),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"])
                        },
                        especies = new BO.EspeciesBO
                        {
                            id = Convert.ToInt32(dr["idespecie"].ToString()),
                            nomCientifico = dr["nombreespecie"].ToString()
                        },
                        razas = new BO.RazasBO
                        {
                            nombre = dr["nombreraza"].ToString()
                        },
                        usuarios = new BO.UsuarioBO
                        {
                            id = Convert.ToInt32(dr["idusuario"].ToString()),
                            nombre = dr["propietario"].ToString()
                            
                        }
                    };
                    mascotas.Add(p);
                }
            }
            con.CerrarConexion();
            return mascotas;
        }


        public List<EspeciesBO> DropDownEspecie()
        {
            var especies = new List<EspeciesBO>();
            SqlCommand cmd = new SqlCommand("SELECT e.id, (e.nomCientifico + ' ('+ e.nomComun+')') AS nombre FROM Especie e");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.EspeciesBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nomCientifico = dr["nombre"].ToString()
                    };
                    especies.Add(p);
                }

            }
            con.CerrarConexion();
            return especies;
        }

        public List<RazasBO> DropDownRaza()
        {
            var razas = new List<RazasBO>();
            SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM Raza");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RazasBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    razas.Add(p);
                }

            }
            con.CerrarConexion();
            return razas;
        }

        public List<UsuarioBO> DropDownUsuario()
        {
            var usuarios = new List<UsuarioBO>();
            SqlCommand cmd = new SqlCommand("SELECT id, (nombre + ' ' + apellidos) AS nombre FROM Usuario");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.UsuarioBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString()
                    };
                    usuarios.Add(p);
                }

            }
            con.CerrarConexion();
            return usuarios;
        }

    }
}