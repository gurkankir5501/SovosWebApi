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
    public class InvoiceLineRepository : GenericRepository<InvoiceLine>, IInvoiceLineRepository
    {
        public InvoiceLineRepository(Context context) : base(context)
        {

        }

        
    }
}
