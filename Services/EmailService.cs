using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using FixedModules.Models;

namespace Api.Services
{
    public class EmailServices
    {
        public interface IMailer
        {
            Task SendEmailAsync(string email, string subject, string body);
        }

        public class Mailer : IMailer
        {
            private readonly EmailSmtpConfig smtpConfig;
            private readonly IWebHostEnvironment env;

            public Mailer(IOptions<EmailSmtpConfig> smtpConfig, IWebHostEnvironment env)
            {
                this.smtpConfig = smtpConfig.Value;
                this.env = env;
            }

            public async Task SendEmailAsync(string email, string subject, string body)
            {
                try
                {
                    smtpConfig.SmtpServer = "mail.emerico.biz";
                    smtpConfig.SmtpSender = "kelvin@emerico.biz";
                    smtpConfig.SmtpUsername = "kelvin@emerico.biz";
                    smtpConfig.SmtpPassword = "abcd11223344556677889910";
                    smtpConfig.SmtpPort = 587;

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("no-reply", smtpConfig.SmtpSender));
                    message.To.Add(new MailboxAddress(email));
                    message.Subject = subject;
                    message.Body = new TextPart("html")
                    {
                        Text = body
                    };

                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        if (env.IsDevelopment())
                        {
                            await client.ConnectAsync(smtpConfig.SmtpServer, smtpConfig.SmtpPort, false);
                        }

                        else
                        {
                            await client.ConnectAsync(smtpConfig.SmtpServer);
                        }

                        await client.AuthenticateAsync(smtpConfig.SmtpUsername, smtpConfig.SmtpPassword);
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                }
                catch (Exception e)
                {

                    throw new InvalidOperationException(e.Message);
                }
                await Task.CompletedTask;
            }
        }
    }
}
