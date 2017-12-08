using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DrHuellitas.BO;
using Encriptacion;
namespace DrHuellitas.DAO
{
    public class AgregarVeterinarioDAO
    {
        ConexionSQL conex = new ConexionSQL();
        EncriptarMD5 encriptar = new EncriptarMD5();
        FotoBO convert = new FotoBO();

        public int agregarVeterinario(RegistrosBO obj,int idempresa)
        {
            SqlCommand cmd = new SqlCommand("exec insertarveterinario @usuario,@idtipo,@contraseña,@fecharegistro,@foto,@idempresa");
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = obj.usuario.usuario;
            cmd.Parameters.Add("@idtipo", SqlDbType.Int).Value = 4;
            cmd.Parameters.Add("@contraseña", SqlDbType.VarChar).Value = encriptar.Encriptar(obj.usuario.contraseña);
            cmd.Parameters.Add("@fecharegistro", SqlDbType.Date).Value = DateTime.Now.ToShortDateString();
            cmd.Parameters.Add("@foto", SqlDbType.Image).Value = convert.ConvertirAFoto(obj.usuario.img);
            cmd.Parameters.Add("@idempresa", SqlDbType.Int).Value = idempresa;

            return conex.EjecutarComando(cmd);
        }

        public int actualizardatos(RegistrosBO obj,int id)
        {
            SqlCommand cmd = new SqlCommand("update Usuario set email=@email,nombre=@nombre,apellidos=@apell,telefono=@telefono,fechanacimiento=@fechanacimiento,foto=@foto, status=1 where id='"+id+"'");
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = obj.usuario.email;
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.usuario.nombre;
            cmd.Parameters.Add("@apell", SqlDbType.VarChar).Value = obj.usuario.apellidos;
            cmd.Parameters.Add("@telefono", SqlDbType.Char).Value = obj.usuario.telefono;
            cmd.Parameters.Add("@fechanacimiento", SqlDbType.Date).Value = obj.usuario.fechanacimiento.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@foto", SqlDbType.Image).Value = convert.ConvertirAFoto(obj.usuario.img);
            return conex.EjecutarComando(cmd);

        }
    }
}
