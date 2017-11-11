﻿using System;
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
        ConexionDAO conex;
        EncriptarMD5 Encriptacion = new EncriptarMD5();

        public UsuarioDAO()
        {
            conex = new ConexionDAO();
        }

        public string MD5Encriptar(string texto)
        {
            string encriptado = Encriptacion.EncriptarMD5_1(texto);
            return encriptado;
        }

        public int agregarUsuario(RegistroBO usuario)
        {
            string contraseña = MD5Encriptar(usuario.contraseña);
           string agregar = string.Format("insert into Usuario(usuario,idTipo,contraseña,nombre,email)values('{0}','{1}','{2}','{3}','{4}')", usuario.usuario,usuario.idtipo,contraseña,usuario.nombre,usuario.email);
            return conex.ejecutarSentencia(agregar);
        }
        public int modificarusuario(RegistroBO usuario)
        {
            string modificar = string.Format("update Usuario set usuario='{1}',contraseña='{2}',nombre='3}',correo='{4}',idtipo='{5}' where id='{0}'", usuario.id, usuario.usuario, usuario.contraseña, usuario.nombre, usuario.email, usuario.idtipo);
            return conex.ejecutarSentencia(modificar);
        }
        public RegistroBO BuscarUsuario(string Usuario, string Contraseña)
        {
            string Sentencia = string.Format("select * from Usuario where usuario ='{0}' and Contraseña='{1}'", Usuario, Contraseña);
            SqlCommand Comando = new SqlCommand(Sentencia);
            Comando.Connection = conex.conectar();
            SqlDataReader Reader;
            conex.abrirConexion();
            Reader = Comando.ExecuteReader();
            RegistroBO UsuarioBO = new RegistroBO();
            if (Reader.Read())
            {
                UsuarioBO.id = int.Parse(Reader[0].ToString());
                UsuarioBO.usuario = Reader[1].ToString();
                UsuarioBO.contraseña = Reader[3].ToString();
                UsuarioBO.nombre = Reader[4].ToString();
                //UsuarioBO.id_tipo =int.Parse( Reader[5].ToString());
            }
            conex.cerrarConexion();
            return UsuarioBO;
        }
    }
}