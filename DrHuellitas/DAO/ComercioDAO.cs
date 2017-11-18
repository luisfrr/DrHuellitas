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

        public int ContinuarRegistroComercio(RegistrosBO objBo, int usuario)
        {
            if (objBo.horario.inicioLunes != null && objBo.horario.finalLunes != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioLunes;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalLunes;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Lunes";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                 conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioMarte != null && objBo.horario.finalMartes != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioMarte;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalMartes;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Martes";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioMiercoles != null && objBo.horario.finalMiercoles != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioMiercoles;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalMiercoles;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Miercoles";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioJueves != null && objBo.horario.finalJueves != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioJueves;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalJueves;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Jueves";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                 conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioViernes != null && objBo.horario.finalViernes != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioViernes;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalViernes;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Viernes";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                 conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioSabado != null && objBo.horario.finalSabado != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioSabado;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalSabado;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Sabado";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                 conex.EjecutarComando(comando);
            }
            if (objBo.horario.inicioDomingo != null && objBo.horario.finalDomingo != null)
            {

                SqlCommand comando = new SqlCommand("exec horariosUsu @horaini,@horfin,@dia,@idusuario");
                comando.Parameters.Add("@horaini", SqlDbType.Time).Value = objBo.horario.inicioDomingo;
                comando.Parameters.Add("@horfin", SqlDbType.Time).Value = objBo.horario.finalDomingo;
                comando.Parameters.Add("@dia", SqlDbType.VarChar).Value = objBo.horario.dia = "Domingo";
                comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = usuario;
                comando.CommandType = CommandType.Text;
                 conex.EjecutarComando(comando);
            }
            return 1;
        }

        public int ComercioDireccion(RegistrosBO objbo, int id)
        {
            SqlCommand comando = new SqlCommand("exec comercioDireccion  @nom,@apell,@telefono,@fechanaci,@foto,@id,@comernom,@vete,@estetica,@vender,@nomfis,@rfc,@tele1,@tele2,@email,@idempresa,@calle,@numero,@cruzamineto,@lon,@lati,@colonia,@cp,@idciudad,@idus");
            comando.Parameters.Add("@nom", SqlDbType.VarChar).Value = objbo.usuario.nombre;
            comando.Parameters.Add("@apell", SqlDbType.VarChar).Value = objbo.usuario.apellidos;
            comando.Parameters.Add("@telefono", SqlDbType.Char).Value = objbo.usuario.telefono;
            comando.Parameters.Add("@fechanaci", SqlDbType.Date).Value = objbo.usuario.fechanacimiento.ToString("dd-MM-yyyy");
            comando.Parameters.Add("@foto", SqlDbType.Image).Value = Foto.ConvertirAFoto(objbo.usuario.img);
            comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
            comando.Parameters.Add("@comernom", SqlDbType.VarChar).Value = objbo.comercio.nombreComercial;
            comando.Parameters.Add("@vete", SqlDbType.Bit).Value = objbo.comercio.veterinaria;
            comando.Parameters.Add("@estetica",SqlDbType.Bit).Value=objbo.comercio.estetica;
            comando.Parameters.Add("@vender",SqlDbType.Bit).Value=objbo.comercio.venderproducto;
            comando.Parameters.Add("@nomfis",SqlDbType.VarChar).Value=objbo.comercio.nombreFiscal;
            comando.Parameters.Add("@rfc",SqlDbType.VarChar).Value=objbo.comercio.rfc;
            comando.Parameters.Add("@tele1",SqlDbType.Char).Value=objbo.comercio.telefono1;
            comando.Parameters.Add("@tele2",SqlDbType.Char).Value=objbo.comercio.telefono2;
            comando.Parameters.Add("@email",SqlDbType.VarChar).Value=objbo.comercio.emal;
            comando.Parameters.Add("@idempresa", SqlDbType.Int).Value = id;
            comando.Parameters.Add("@calle", SqlDbType.VarChar).Value = objbo.direccion.calle;
            comando.Parameters.Add("@numero", SqlDbType.VarChar).Value = objbo.direccion.numero;
            comando.Parameters.Add("@cruzamiento", SqlDbType.VarChar).Value = objbo.direccion.cruzamiento;
            comando.Parameters.Add("@lon", SqlDbType.VarChar).Value = objbo.direccion.longitud;
            comando.Parameters.Add("@lati", SqlDbType.VarChar).Value = objbo.direccion.latitud;
            comando.Parameters.Add("@colonia", SqlDbType.VarChar).Value = objbo.direccion.colonia;
            comando.Parameters.Add("@cp", SqlDbType.VarChar).Value = objbo.direccion.CP;
            comando.Parameters.Add("@idciudad", SqlDbType.Int).Value = objbo.direccion.idCiudad = 1;
            comando.Parameters.Add("@idus", SqlDbType.Int).Value = id;

            comando.CommandType = CommandType.Text;
            return conex.EjecutarComando(comando);
        }
    }
}