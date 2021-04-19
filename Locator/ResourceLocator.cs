using Locator.Models.Entities;
using Locator.Models.Interfaces;
using Locator.Models.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Database;

namespace Locator
{
    public class ResourceLocator
    {
        #region Singleton Pattern
        private static ResourceLocator _instance;

        public static ResourceLocator Instance
        {
            get
            {
                return _instance ?? (_instance = new ResourceLocator());
            }
        }       

        private ResourceLocator()
        {

        }
        #endregion

        private IConnection _connection;
        private IPersonRepository<Person> _personRepository;
        private IRepository<string> _repository;

        public IPersonRepository<Person> PersonRepository
        {
            get
            {
                return _personRepository ?? (_personRepository = new PersonRepository(Connection));
            }
        }

        public IRepository<string> Repository
        {
            get
            {
                return _repository ?? (_repository = new Repository());
            }
        }

        private IConnection Connection
        {
            get
            {
                return _connection ?? (_connection = new Connection(SqlClientFactory.Instance, @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;"));
            }
        }
    }
}
