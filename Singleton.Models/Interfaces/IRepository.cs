using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton.Models.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
    }
}
