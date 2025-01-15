using FinancasApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Helpers
{
    public class EmailMessageHelper
    {
        //atributos constantes
        private static string _account = "ruan1415oliveira@outlook.com";
        private static string _password = "";
        private static string _smtp = "smtp-mail.outlook.com";
        private static int _port = 587;

        //envio das mensagens
        public static void Send(EmailMessage model)
        {
            //preparar o conteúdo do email que será enviado
            var mailMessage = new MailMessage(_account, model.EmailRecipient);
            mailMessage.Subject = model.Subject;
            mailMessage.Body = model.Body;

            //Enviando o email
            var smtpClient = new SmtpClient(_smtp, _port);
            smtpClient.EnableSsl = true; // Habilita criptografia SSL
            smtpClient.Credentials = new NetworkCredential(_account, _password);
            smtpClient.Send(mailMessage);
        }
    }
}
