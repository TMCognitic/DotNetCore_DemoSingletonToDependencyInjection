using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Models.Interfaces
{
    public interface IPersonRepository<TPerson>
    {
        IEnumerable<TPerson> Get();
    }
}
