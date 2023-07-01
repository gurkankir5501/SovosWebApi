using SovosWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.Repositories
{
    public interface IInvoiceHeaderRepository : IGenericRepository<InvoiceHeader>
    {
        IEnumerable<InvoiceLine> GetInvoiceAllDetail(string id);
    }
}
