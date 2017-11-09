using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DrHuellitas.BO;

namespace DrHuellitas.DAO
{
    public class UsuarioDAO
    {
        ConexionDAO conex;

        public UsuarioDAO()
        {
            conex = new ConexionDAO();
        }

        public int agregarUsuario(RegistroBO usuario)
        {
           string agregar = string.Format("insert into Usuario(usuario,contraseña,nombre,correo,idtipo)values('{0}','{1}','{2}','{3}','{4}')",usuario.usuario,usuario.contraseña,usuario.nombre,usuario.email,usuario.id_tipo);
            return conex.ejecutarSentencia(agregar);
        }
        public int modificarusuario(RegistroBO usuario)
        {
            string modificar = string.Format("update Usuario set usuario='{1}',contraseña='{2}',nombre='3}',correo='{4}',idtipo='{5}' where id='{0}'", usuario.id, usuario.usuario, usuario.contraseña, usuario.nombre, usuario.email, usuario.id_tipo);
            return conex.ejecutarSentencia(modificar);
        }
    }
}