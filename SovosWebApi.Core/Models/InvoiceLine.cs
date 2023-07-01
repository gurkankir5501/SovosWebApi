using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.Models
{
    [Table(nameof(InvoiceLine))]
    public class InvoiceLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string UnitCode { get; set; }
        public decimal UnitPrice { get; set; }

        [ForeignKey(nameof(InvoiceHeader))]
        public string InvoiceId { get; set; }

        public InvoiceHeader? InvoiceHeader { get; set; }
    }
}
