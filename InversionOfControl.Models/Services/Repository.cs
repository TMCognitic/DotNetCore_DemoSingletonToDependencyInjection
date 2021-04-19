using InversionOfControl.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControl.Models.Services
{
    public class Repository : IRepository<string>
    {
        public IEnumerable<string> Get()
        {
            yield return "Value 1";
            yield return "Value 2";
            yield return "Value 3";
            yield return "Value 4";
        }
    }
}
