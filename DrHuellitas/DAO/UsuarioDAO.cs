using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;
using System.Data.SqlClient;
using System.Data;
using Encriptacion;

namespace DrHuellitas.DAO
{
    public class UsuarioDAO
    {
        ConexionSQL con;
        EncriptarMD5 MD5 = new EncriptarMD5();
        FotoBO Foto = new FotoBO();
        SqlCommand cmd;

        public UsuarioDAO()
        {
            con = new ConexionSQL();
        }

        public int RegistrarUsuario(RegistrosBO objBO)
        {
            string contraseña = MD5.Encriptar(objBO.usuario.contraseña);
            cmd = new SqlCommand("INSERT INTO Usuario(usuario,idTipo,contraseña,email,status,fecharegistro)values(@usuario,@idTipo,@contraseña,@email,@status,@fecharegistro)");
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = objBO.usuario.usuario;
            cmd.Parameters.Add("@idTipo", SqlDbType.Int).Value = objBO.usuario.idtipo;
            cmd.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = contraseña;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = objBO.usuario.email;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@fecharegistro", SqlDbType.Date).Value = DateTime.Now.ToString("dd-MM-yyyy");

            return con.EjecutarComando(cmd);
        }
        
        public RegistrosBO IniciarSesion(string Usuario, string Contraseña)
        {
            string newpass = MD5.Encriptar(Contraseña);
            SqlCommand cmd = new SqlCommand("SELECT id, nombre, status, foto, idTipo FROM Usuario where usuario=@usuario and contraseña=@contraseña");
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = Usuario;
            cmd.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = newpass;

            cmd.Connection = con.establecerConexion();
            SqlDataReader Reader;
            con.AbrirConexion();
            Reader = cmd.ExecuteReader();
            RegistrosBO objBO = new RegistrosBO();
            if (Reader.Read())
            {
                objBO.usuario = new BO.UsuarioBO
                {
                    id = int.Parse(Reader[0].ToString()),
                    nombre = Reader[1].ToString(),
                    status = int.Parse(Reader[2].ToString()),
                    foto = (Reader[3] != null) ? "data:image/jpeg;base64," + Convert.ToBase64String((byte[])Reader[3]) : "",
                    idtipo = int.Parse(Reader[4].ToString())
                };
            }

            con.CerrarConexion();
            return objBO;
        }

        public int ContinuarRegistro(RegistrosBO registro, int usuario)
        {
            string predominante = (registro.mascota.colorPreDominante != null) ? registro.mascota.colorPreDominante : "-";
            string alternativo = (registro.mascota.colorAlternativo != null) ? registro.mascota.colorAlternativo : "-";
            SqlCommand cmd = new SqlCommand("EXEC ContinuarRegUsuario @nombre=@nombre,@apellidos=@apellidos,@telefono=@telefono,@fechaUsuario=@fechaUsuario,@fotoUsuario=@fotoUsuario,@nomMascota=@nomMascota,@CDominante=@Dominante,@CPreDominante=@PreDominante,@CAlternativo=@Alternativo,@genero=@genero,@fechaMascota=@fechaMascota, @fotoMascota=@fotoMascota, @idUsuario=@idUsuario");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = registro.usuario.nombre;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = registro.usuario.apellidos;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = registro.usuario.telefono;
            cmd.Parameters.Add("@fechaUsuario", SqlDbType.Date).Value = registro.usuario.fechanacimiento.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoUsuario", SqlDbType.Image).Value = Foto.ConvertirAFoto(registro.usuario.img);
            cmd.Parameters.Add("@nomMascota", SqlDbType.VarChar).Value = registro.mascota.nombremascota;
            cmd.Parameters.Add("@Dominante", SqlDbType.VarChar).Value = registro.mascota.colorDominate;
            cmd.Parameters.Add("@PreDominante", SqlDbType.VarChar).Value = predominante;
            cmd.Parameters.Add("@Alternativo", SqlDbType.VarChar).Value = alternativo;
            cmd.Parameters.Add("@genero", SqlDbType.VarChar).Value = registro.mascota.genero;
            cmd.Parameters.Add("@fechaMascota", SqlDbType.Date).Value = registro.mascota.fechaNaci.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoMascota", SqlDbType.Image).Value = Foto.ConvertirAFoto(registro.mascota.img);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario;

            return con.EjecutarComando(cmd);
        }



        //Gestios Usuarios
        public int AgregarUsuario(RegistrosBO objBO)
        {
            string contraseña = MD5.Encriptar(objBO.usuario.contraseña);
            cmd = new SqlCommand("INSERT INTO Usuario(usuario,nombre,apellidos,idTipo,contraseña,email,status,fechanacimiento,fecharegistro,foto)values(@usuario,@nombre,@apellidos,@idTipo,@contraseña,@email,@status,@fechanacimiento,@fecharegistro,@fotoUsuario)");
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = objBO.usuario.usuario;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.usuario.nombre;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = objBO.usuario.apellidos;
            cmd.Parameters.Add("@idTipo", SqlDbType.Int).Value = objBO.usuario.idtipo;
            cmd.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = contraseña;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = objBO.usuario.email;
            cmd.Parameters.Add("@status", SqlDbType.Bit).Value = 0;
            cmd.Parameters.Add("@fechanacimiento", SqlDbType.Date).Value = objBO.usuario.fechanacimiento.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fecharegistro", SqlDbType.Date).Value = DateTime.Now.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoUsuario", SqlDbType.Image).Value = Foto.ConvertirAFoto(objBO.usuario.img);

            return con.EjecutarComando(cmd);
        }

        public int ActualizarUsuario(RegistrosBO objBO)
        {
            string contraseña = MD5.Encriptar(objBO.usuario.contraseña);
            cmd = new SqlCommand("UPDATE Usuario SET usuario=@usuario,nombre=@nombre,apellidos=@apellidos,idTipo=@idTipo,contraseña=@contraseña,email=@email,status=@status,fechanacimiento=@fechanacimiento,fecharegistro=@fecharegistro,foto=@fotoUsuario WHERE id=@id");
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = objBO.usuario.usuario;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = objBO.usuario.nombre;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = objBO.usuario.apellidos;
            cmd.Parameters.Add("@idTipo", SqlDbType.Int).Value = objBO.usuario.idtipo;
            cmd.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = contraseña;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = objBO.usuario.email;
            cmd.Parameters.Add("@status", SqlDbType.Int).Value = 0;
            cmd.Parameters.Add("@fechanacimiento", SqlDbType.Date).Value = objBO.usuario.fechanacimiento.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fecharegistro", SqlDbType.Date).Value = DateTime.Now.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoUsuario", SqlDbType.Image).Value = Foto.ConvertirAFoto(objBO.usuario.img);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objBO.usuario.id;

            return con.EjecutarComando(cmd);
        }

        public int EliminarUsuario(int id)
        {
            
            cmd = new SqlCommand("EXEC EliminarUsuario @idUsuario=@id");
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

            return con.EjecutarComando(cmd);
        }


        public List<RegistrosBO> ObtenerListaUsuarios()
        {
            var usuarios = new List<RegistrosBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM vistaUsuarios");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegistrosBO
                    {
                        usuario = new BO.UsuarioBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            usuario = dr["usuario"].ToString(),
                            tipoUs = dr["tipo"].ToString(),
                            email = dr["email"].ToString(),
                            fnacimiento= Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy"),
                            fregistro = Convert.ToDateTime(dr["fecharegistro"]).ToString("dd/MM/yyyy"),
                            foto = "data:image/jpeg;base64," +Convert.ToBase64String((byte[])dr["foto"])
                        }
                    };
                    usuarios.Add(p);
                }
            }
            return usuarios;
        }

        public List<RegistrosBO> ObtenerUsuario(int idUsuario)
        {
            var usuarios = new List<RegistrosBO>();
            SqlCommand cmd = new SqlCommand("SELECT u.id, u.nombre, u.apellidos, u.usuario,u.contraseña, t.nombre AS tipo,u.idTipo, u.email, u.fechanacimiento, u.fecharegistro, u.foto FROM Usuario u JOIN TipoUsuario t ON t.id = u.idTipo WHERE u.id=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.RegistrosBO
                    {
                        usuario = new BO.UsuarioBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            nombre = dr["nombre"].ToString(),
                            apellidos=dr["apellidos"].ToString(),
                            usuario = dr["usuario"].ToString(),
                            contraseña = MD5.Desencriptar(dr["contraseña"].ToString()),
                            tipoUs = dr["tipo"].ToString(),
                            idtipo = Convert.ToInt32(dr["idTipo"].ToString()),
                            email = dr["email"].ToString(),
                            fnacimiento = Convert.ToDateTime(dr["fechanacimiento"]).ToString("dd/MM/yyyy"),
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"])
                        }
                    };
                    usuarios.Add(p);
                }
            }
            return usuarios;
        }

        public List<TipoUsuarioBO> DropDownTipoUs()
        {
            var usuarios = new List<TipoUsuarioBO>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM TipoUsuario");

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var p = new BO.TipoUsuarioBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        nombre = dr["nombre"].ToString(),
                    };
                    usuarios.Add(p);
                }
            }
            return usuarios;
        }

    }
}