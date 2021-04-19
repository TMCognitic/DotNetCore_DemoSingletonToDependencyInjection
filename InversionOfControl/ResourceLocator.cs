using InversionOfControl.Models.Entities;
using InversionOfControl.Models.Interfaces;
using InversionOfControl.Models.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Connections.Database;
using Tools.Pattern.Locator;

namespace InversionOfControl
{
    public class ResourceLocator : LocatorBase
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

        protected override void ConfigureServices()
        {
            Container.Register<IConnection, Connection>(() => new Connection(SqlClientFactory.Instance, @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;"));
            Container.Register<IPersonRepository<Person>, PersonRepository>(() => new PersonRepository(Connection));
            Container.Register<IRepository<string>, Repository>();
        }

        public IPersonRepository<Person> PersonRepository
        {
            get
            {
                return Container.GetResource<IPersonRepository<Person>>();
            }
        }

        public IRepository<string> Repository
        {
            get
            {
                return Container.GetResource<IRepository<string>>();
            }
        }

        private IConnection Connection
        {
            get
            {
                return Container.GetResource<IConnection>();
            }
        }

        
    }
}
