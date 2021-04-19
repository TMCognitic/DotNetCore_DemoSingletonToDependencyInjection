using Microsoft.Extensions.DependencyInjection;
using Microsoft.Models.Entities;
using Microsoft.Models.Interfaces;
using Microsoft.Models.Services;
using System.Data.SqlClient;
using Tools.Connections.Database;
using Tools.Pattern.Locator;

namespace Microsoft
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

        protected override void ConfigureServices(IServiceCollection services)
        {
            //Retourne toujours la même instance
            services.AddSingleton<IConnection, Connection>((sp) => new Connection(SqlClientFactory.Instance, @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;"));
            //Retourne toujours la même instance dans le même scope
            services.AddScoped<IPersonRepository<Person>, PersonRepository>();
            //retourne toujours une nouvelle instance
            services.AddTransient<IRepository<string>, Repository>();
        }

        public IPersonRepository<Person> PersonRepository
        {
            get
            {
                return Container.GetService<IPersonRepository<Person>>();
            }
        }

        public IRepository<string> Repository
        {
            get
            {
                return Container.GetService<IRepository<string>>();
            }
        }        
    }
}
