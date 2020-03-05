using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceMetricsAPI.Repositories
{
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<T> : IDisposable where T : DbContext, new()
    {
        internal DbContext _context;
        protected T Context => (T) this._context;
        protected RepositoryBase()
        {
            this._context = new T();
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
                this._context = null;
            }
        }
    }
}
