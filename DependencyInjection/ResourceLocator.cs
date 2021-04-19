using DependencyInjection.Models.Entities;
using DependencyInjection.Models.Interfaces;
using DependencyInjection.Models.Services;
using System.Data.SqlClient;
using Tools.Connections.Database;
using Tools.Pattern.Locator;

namespace DependencyInjection
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
            Container.Register<IPersonRepository<Person>, PersonRepository>();
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
    }
}
