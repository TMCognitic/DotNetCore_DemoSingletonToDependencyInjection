using Singleton.Models.Entities;
using Singleton.Models.Interfaces;
using Singleton.Models.Mappers;
using System.Collections.Generic;
using System.Data.SqlClient;
using Tools.Connections.Database;

namespace Singleton.Models.Services
{
    public class PersonRepository : IPersonRepository<Person>
    {
        #region Singleton Pattern
        private static IPersonRepository<Person> _instance;
        public static IPersonRepository<Person> Instance
        {
            get
            {
                return _instance ?? (_instance = new PersonRepository());
            }
        }

        private PersonRepository()
        {
            _connection = new Connection(SqlClientFactory.Instance, @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;");
        }
        #endregion

        private readonly IConnection _connection;

        public IEnumerable<Person> Get()
        {
            Command command = new Command("Select Top 100 BusinessEntityId, Title, LastName, FirstName From Person.Person;");

            return _connection.ExecuteReader(command, dr => dr.ToPerson());
        }
    }
}
