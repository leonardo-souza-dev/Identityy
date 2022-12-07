using MimeKit;

namespace Identityy.Models;

public class Mensagem
{
    public List<MailboxAddress> Destinatario { get; set; }
    public string Assunto { get; set; }
    public string Conteudo { get; set; }

    public Mensagem(
        IEnumerable<string> destinatario, 
        string assunto, 
        int usuarioId,
        string codigo)
    {
        Destinatario = new List<MailboxAddress>();
        Destinatario.AddRange(destinatario.Select(d => new MailboxAddress(d)));
        Assunto = assunto;
        Conteudo = $"https://localhost:8544/api/ativar-conta?UsuarioId={usuarioId}&CodigoAtivacao={codigo}";
    }
}
