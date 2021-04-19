using Locator.Models.Entities;
using Locator.Models.Interfaces;
using Locator.Models.Mappers;
using System.Collections.Generic;
using Tools.Connections.Database;

namespace Locator.Models.Services
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
