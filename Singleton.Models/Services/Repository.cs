using Singleton.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton.Models.Services
{
    public class Repository : IRepository<string>
    {
        #region Singleton Pattern
        private static IRepository<string> _instance;
        public static IRepository<string> Instance
        {
            get
            {
                return _instance ?? (_instance = new Repository());
            }
        }

        private Repository()
        {
        }
        #endregion

        public IEnumerable<string> Get()
        {
            yield return "Value 1";
            yield return "Value 2";
            yield return "Value 3";
            yield return "Value 4";
        }
    }
}
