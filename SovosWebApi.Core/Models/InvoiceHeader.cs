using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.Models
{
    [Table(nameof(InvoiceHeader))]
    public class InvoiceHeader
    {
        public InvoiceHeader()
        {
            Invoices = new List<InvoiceLine>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string InvoiceId { get; set; }
        public string SenderTitle { get; set; }
        public string ReceiverTitle { get; set; }
        public DateTime Date { get; set; }

        public ICollection<InvoiceLine>? Invoices { get; set; }
    }
}
