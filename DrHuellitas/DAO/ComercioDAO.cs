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
        SqlCommand cmd;

        public ComercioDAO()
        {
            conex = new ConexionSQL();
        }

        public int ContinuarRegistroComercio(RegistroComercio objBo, int usuario)
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
    }
}