using DependencyInjection.Models.Entities;
using DependencyInjection.Models.Interfaces;
using DependencyInjection.Models.Mappers;
using System.Collections.Generic;
using Tools.Connections.Database;

namespace DependencyInjection.Models.Services
{
    public class PersonRepository : IPersonRepository<Person>
    {
        private readonly IConnection _connection;

        public PersonRepository(IConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Person> Get()
        {
            Command command = new Command("Select Top 100 BusinessEntityId, Title, LastName, FirstName From Person.Person;");

            return _connection.ExecuteReader(command, dr => dr.ToPerson());
        }
    }
}
