namespace Pdbc.Mailing.RazorEngine.Models
{
    public interface IMailViewModel
    {
        MailTemplateInfo MailTemplateInfo { get; set; }

        MailCommunicationInfo MailCommunicationInfo { get; set; }

    }
}
