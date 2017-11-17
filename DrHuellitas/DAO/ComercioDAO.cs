using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Encriptacion;
using DrHuellitas.BO;
using System.Data.SqlClient;
using System.Data;

namespace DrHuellitas.DAO
{
    public class ComercioDAO
    {
        ConexionSQL conex;
        EncriptarMD5 MD5 = new EncriptarMD5();
        FotoBO Foto = new FotoBO();
        

        public ComercioDAO()
        {
            conex = new ConexionSQL();
        }

        public int ContinuarRegistroComercio(RegistroBO registro, int usuario)
        {
            string predominante = (registro.colorPreDominante != null) ? registro.colorPreDominante : "-";
            string alternativo = (registro.colorAlternativo != null) ? registro.colorAlternativo : "-";
            SqlCommand cmd = new SqlCommand("EXEC ContinuarRegUsuario @nombre=@nombre,@apellidos=@apellidos,@telefono=@telefono,@fechaUsuario=@fechaUsuario,@fotoUsuario=@fotoUsuario,@nomMascota=@nomMascota,@CDominante=@Dominante,@CPreDominante=@PreDominante,@CAlternativo=@Alternativo,@genero=@genero,@fechaMascota=@fechaMascota, @fotoMascota=@fotoMascota, @idUsuario=@idUsuario");
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = registro.nombre;
            cmd.Parameters.Add("@apellidos", SqlDbType.VarChar).Value = registro.ape;
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = registro.telefono;
            cmd.Parameters.Add("@fechaUsuario", SqlDbType.Date).Value = registro.fechanacimiento.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoUsuario", SqlDbType.Image).Value = Foto.ConvertirAFoto(registro.img);
            cmd.Parameters.Add("@nomMascota", SqlDbType.VarChar).Value = registro.nombremascota;
            cmd.Parameters.Add("@Dominante", SqlDbType.VarChar).Value = registro.colorDominate;
            cmd.Parameters.Add("@PreDominante", SqlDbType.VarChar).Value = predominante;
            cmd.Parameters.Add("@Alternativo", SqlDbType.VarChar).Value = alternativo;
            cmd.Parameters.Add("@genero", SqlDbType.VarChar).Value = registro.genero;
            cmd.Parameters.Add("@fechaMascota", SqlDbType.Date).Value = registro.fechaNaci.ToString("dd-MM-yyyy");
            cmd.Parameters.Add("@fotoMascota", SqlDbType.Image).Value = Foto.ConvertirAFoto(registro.imgmas);
            cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario;

            return conex.EjecutarComando(cmd);
        }
    }
}