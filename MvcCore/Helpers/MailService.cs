using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MvcCorePaco.Helpers
{
    public class MailService
    {
        FileUploadService uploadservice;
        IConfiguration configuration;
        public MailService(FileUploadService uploadservice, IConfiguration configuration)
        {
            this.uploadservice = uploadservice;
            this.configuration = configuration;
        }

        public void SendMail(string receptor, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            string usermail = configuration["UserMail"];
            string pass = configuration["PasswordMail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            string smtpserver = configuration["smtp"];
            int port = int.Parse(configuration["port"]);
            bool ssl = bool.Parse(configuration["ssl"]);
            bool defaultcredentials = bool.Parse(configuration["defaultcredentials"]);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaultcredentials;
            NetworkCredential userCredentials = new NetworkCredential(usermail, pass);
            smtpClient.Credentials = userCredentials;
            smtpClient.Send(mail);
        }
        public void SendMail(string receptor, string asunto, string mensaje, string path)
        {
            MailMessage mail = new MailMessage();
            string usermail = this.configuration["UserMail"];
            string pass = this.configuration["PasswordMail"];
            mail.From = new MailAddress(usermail);
            mail.To.Add(new MailAddress(receptor));
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            Attachment attachment = new Attachment(path);
            mail.Attachments.Add(attachment);
            string smtpserver = configuration["smtp"];
            int port = int.Parse(configuration["port"]);
            bool ssl = bool.Parse(configuration["ssl"]);
            bool defaultcredentials = bool.Parse(configuration["defaultcredentials"]);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpserver;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaultcredentials;
            NetworkCredential userCredentials = new NetworkCredential(usermail, pass);
            smtpClient.Credentials = userCredentials;
            smtpClient.Send(mail);


        }

    }
}
