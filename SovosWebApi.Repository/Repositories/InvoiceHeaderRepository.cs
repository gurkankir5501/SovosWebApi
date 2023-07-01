using SovosWebApi.Core.Models;
using SovosWebApi.Core.Repositories;
using SovosWebApi.Repository.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Repository.Repositories
{
    public class InvoiceHeaderRepository : GenericRepository<InvoiceHeader>, IInvoiceHeaderRepository
    {
        public InvoiceHeaderRepository(Context context) : base(context)
        {
        }

        public IEnumerable<InvoiceLine> GetInvoiceAllDetail(string id)
        {
            return _context.Set<InvoiceLine>().Where(s => s.InvoiceId == id).ToList();
        }

        
    }
}
