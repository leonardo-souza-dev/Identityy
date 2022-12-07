using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;

namespace Identityy.Models;

public class EmailService
{
    private IConfiguration _configuration;
    private UserManager<IdentityUser<int>> _userManager;

	public EmailService(IConfiguration configuration, 
        UserManager<IdentityUser<int>> userManager)
	{
        _configuration = configuration;
        _userManager = userManager;
    }

    public void EnviarEmail(
        string?[] destinatario, 
        string assunto, 
        int usuarioId, string code)
    {
        var mensagem = new Mensagem(destinatario, assunto, usuarioId, code);
        var mensagemDeEmail = CriarCorpoEmail(mensagem);
        EnviarEmail(mensagemDeEmail);

    }

    private MimeMessage CriarCorpoEmail(Mensagem mensagem)
    {
        var mensagemDeEmail = new MimeMessage();
        var from = _configuration.GetValue<string>("EmailSettings:From");
        mensagemDeEmail.From.Add(new MailboxAddress(from));
        mensagemDeEmail.To.AddRange(mensagem.Destinatario);
        mensagemDeEmail.Subject = mensagem.Assunto;
        mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
        {
            Text = mensagem.Conteudo
        };

        return mensagemDeEmail;
    }

    private void EnviarEmail(MimeMessage mensagemDeEmail)
    {
        using(var client = new MailKit.Net.Smtp.SmtpClient())
        {
            try
            {
                var smtpConfig = _configuration.GetValue<string>("EmailSettings:SmtpServer");
                var portConfig = _configuration.GetValue<int>("EmailSettings:Port");
                var from = _configuration.GetValue<string>("EmailSettings:From");
                var password = _configuration.GetValue<string>("EmailSettings:Password");

                client.Connect(smtpConfig, portConfig, MailKit.Security.SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(from, password);

                client.Send(mensagemDeEmail);
            }
            catch 
            {
                throw;
            }
            finally
            { 
                client.Disconnect(true); 
            }
        }
    }
}
