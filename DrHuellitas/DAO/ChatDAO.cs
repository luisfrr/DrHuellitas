using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class ChatDAO
    {
        ConexionSQL con = new ConexionSQL();
        //El Administrador recibe mensajes

        // Este es para el idrecibe (Administrador y Veterinario)
        public List<ChatBO> ObtenerListaChatsRecibe(int idUsuario) 
        {
            var chats = new List<ChatBO>();
            SqlCommand cmd = new SqlCommand("SELECT c.*, u.foto, (u.nombre + ' '+ u.apellidos )AS nombre, (SELECT texto FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS mensaje, (SELECT hora FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS hora, (SELECT fecha FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS fecha, (SELECT COUNT(status) FROM Mensaje WHERE idchat =c.id AND status= 0 AND idUsuario = c.idenvia) AS contador FROM Chat c JOIN Usuario u ON u.id = c.idenvia WHERE c.idrecibe =@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idUsuario;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ChatBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        idenvia = Convert.ToInt32(dr["idenvia"].ToString()),
                        idrecibe = Convert.ToInt32(dr["idrecibe"].ToString()),
                        usuarios = new BO.UsuarioBO
                        {
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            nombre = dr["nombre"].ToString()
                        },
                        mensajes = new BO.MensajesBO
                        {
                            mensaje = dr["mensaje"].ToString(),
                            shora = dr["hora"].ToString(),
                            sfecha = Convert.ToDateTime(dr["fecha"]).ToString("yyyy-MM-dd"),
                            status = Convert.ToInt32(dr["contador"].ToString())
                        }
                    };
                    chats.Add(c);
                }
                
            }
            con.CerrarConexion();
            return chats;
        }

        
        //Para este es para el idenvia (usuario y comercio)

        public List<ChatBO> ObtenerListaChatsEnvia(int idenvia)
        {
            var chats = new List<ChatBO>();
            SqlCommand cmd = new SqlCommand("SELECT c.*, u.foto, (u.nombre + ' '+ u.apellidos )AS nombre, (SELECT texto FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS mensaje, (SELECT hora FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS hora, (SELECT fecha FROM Mensaje WHERE id = (SELECT MAX(m.id) FROM Mensaje m WHERE m.idchat =c.id)) AS fecha FROM Chat c JOIN Usuario u ON u.id = c.idrecibe WHERE c.idenvia=@id");
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = idenvia;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ChatBO
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        idenvia = Convert.ToInt32(dr["idenvia"].ToString()),
                        idrecibe = Convert.ToInt32(dr["idrecibe"].ToString()),
                        usuarios = new BO.UsuarioBO
                        {
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            nombre = dr["nombre"].ToString()
                        },
                        mensajes = new BO.MensajesBO
                        {
                            mensaje = dr["mensaje"].ToString(),
                            shora = dr["hora"].ToString(),
                            sfecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy")
                        }
                    };
                    chats.Add(c);
                }
            }
            con.CerrarConexion();
            return chats;
        }

        // Ver chat del idenvia 
        public List<ChatBO> VerChatEnvia(int idchat)
        {
            var chat = new List<ChatBO>();
            SqlCommand cmd = new SqlCommand("SELECT m.*, u.foto, u.nombre + ' '+ u.apellidos AS nombre FROM Chat c JOIN Mensaje m ON m.idchat = c.id JOIN Usuario u ON u.id = c.idrecibe WHERE c.id =@id");
            cmd.Parameters.Add("id", SqlDbType.Int).Value = idchat;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using(var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ChatBO
                    {
                        id = Convert.ToInt32(dr["idchat"].ToString()),
                        mensajes = new BO.MensajesBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            mensaje = dr["texto"].ToString(),
                            status = Convert.ToInt32(dr["status"].ToString()),
                            sfecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy"),
                            shora = dr["hora"].ToString(),
                        },
                        usuarios = new BO.UsuarioBO
                        {
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            nombre = dr["nombre"].ToString()
                        }
                    };
                    chat.Add(c);
                }
            }

            con.CerrarConexion();
            return chat;
        }

        public int ActulizarStatus(int idChat, int idSession)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Mensaje SET status=1 WHERE idchat =@idchat AND idUsuario !=@idSession AND status = 0");
            cmd.Parameters.Add("@idchat", SqlDbType.Int).Value = idChat;
            cmd.Parameters.Add("@idSession", SqlDbType.Int).Value = idSession;

            return con.EjecutarComando(cmd);
        }

        public int EnviarMensaje(ChatBO chatBO)
        {
            SqlCommand cmd = new SqlCommand("EXEC EnviarMensaje @text,@fecha,@hora,@idenvia,@idrecibe,@idUsuario");
            cmd.Parameters.Add("@text", SqlDbType.VarChar).Value = chatBO.mensajes.mensaje;
            cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = DateTime.Now.ToString("dd/MM/yyyy");
            cmd.Parameters.Add("@hora", SqlDbType.Time).Value = DateTime.Now.ToString("hh:mm:ss");
            cmd.Parameters.Add("@idenvia", SqlDbType.Int).Value = chatBO.idenvia;
            cmd.Parameters.Add("@idrecibe", SqlDbType.Int).Value = chatBO.idrecibe;
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = chatBO.idrecibe;

            return con.EjecutarComando(cmd);
        }

        // Ver chat del idrecibe 
        public List<ChatBO> VerChatRecibe(int idchat)
        {
            var chat = new List<ChatBO>();
            SqlCommand cmd = new SqlCommand("SELECT m.*, u.foto, u.nombre + ' '+ u.apellidos AS nombre FROM Chat c JOIN Mensaje m ON m.idchat = c.id JOIN Usuario u ON u.id = m.idUsuario WHERE c.id=@id");
            cmd.Parameters.Add("id", SqlDbType.Int).Value = idchat;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ChatBO
                    {
                        id = Convert.ToInt32(dr["idchat"].ToString()),
                        mensajes = new BO.MensajesBO
                        {
                            id = Convert.ToInt32(dr["id"].ToString()),
                            mensaje = dr["texto"].ToString(),
                            status = Convert.ToInt32(dr["status"].ToString()),
                            sfecha = Convert.ToDateTime(dr["fecha"]).ToString("dd-MM-yyyy"),
                            shora = dr["hora"].ToString(),
                        },
                        usuarios = new BO.UsuarioBO
                        {
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            nombre = dr["nombre"].ToString()
                        }
                    };
                    chat.Add(c);
                }
            }

            con.CerrarConexion();
            return chat;
        }

        public List<ChatBO> AbrirChat(int idchat)
        {
            var chat = new List<ChatBO>();
            SqlCommand cmd = new SqlCommand("SELECT (SELECT foto FROM Usuario WHERE id=c.idenvia) AS foto, (SELECT (nombre + ' ' + apellidos) FROM Usuario WHERE id=c.idenvia) AS nombre, c.idenvia FROM Chat c WHERE c.id =@id");
            cmd.Parameters.Add("id", SqlDbType.Int).Value = idchat;

            cmd.Connection = con.establecerConexion();
            con.AbrirConexion();
            var query = cmd;
            using (var dr = query.ExecuteReader())
            {
                while (dr.Read())
                {
                    var c = new BO.ChatBO
                    {
                        idenvia = Convert.ToInt32(dr["idenvia"].ToString()),
                        usuarios = new BO.UsuarioBO
                        {
                            foto = "data:image/jpeg;base64," + Convert.ToBase64String((byte[])dr["foto"]),
                            nombre = dr["nombre"].ToString(),
                            id=Convert.ToInt32(dr["idenvia"].ToString())
                        }
                    };
                    chat.Add(c);
                }
            }

            con.CerrarConexion();
            return chat;
        }




    }
}