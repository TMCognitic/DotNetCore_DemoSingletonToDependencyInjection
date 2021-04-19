using System;
using System.Collections.Generic;
using System.Text;

namespace Locator.Models.Interfaces
{
    public interface IPersonRepository<TPerson>
    {
        IEnumerable<TPerson> Get();
    }
}
