using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovosWebApi.Core.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> GetAll();
        TEntity Get(object id);
    }
}
