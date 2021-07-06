using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pdbc.Mailing.RazorEngine.Models;

namespace Pdbc.Mailing.Sample.Models
{
    public class MailOneViewModel : IMailViewModel
    {
        public MailTemplateInfo MailTemplateInfo { get; set; }

        public MailCommunicationInfo MailCommunicationInfo { get; set; }

        public String MailTitle { get; set; }
    }
}
