using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace DrHuellitas.BO
{
    public class GmailBO
    {
        public void EnviarEmail(string emailEmisor,string contraseña,string emailRemitente,string asunto,string mensaje)
        {
            MailMessage msj = new MailMessage();
            SmtpClient cli = new SmtpClient();
            //drhuellitas@gmail.com
            //drhuellitas2017.
            msj.From = new MailAddress(emailEmisor);
            msj.To.Add(new MailAddress(emailRemitente));
            msj.Subject = asunto;
            msj.Body = mensaje;
            msj.IsBodyHtml = false;

            cli.Host = "smtp.gmail.com";
            cli.Port = 587;
            cli.Credentials = new NetworkCredential(emailEmisor, contraseña);
            cli.EnableSsl = true;
            cli.Send(msj);
        }
    }
}