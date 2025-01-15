using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Models
{
    /// <summary>
    /// Modelo de dados para o serviço de envio de email
    /// </summary>
    public class EmailMessage
    {
        public string? EmailRecipient { get; set; } //email destinatário
        public string? Body { get; set; }
        public string? Subject { get; set; } //assunto
    }
}
