using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton.Models.Interfaces
{
    public interface IPersonRepository<TPerson>
    {
        IEnumerable<TPerson> Get();
    }
}
