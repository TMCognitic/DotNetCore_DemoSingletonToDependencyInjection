using Singleton.Models.Entities;
using Singleton.Models.Interfaces;
using Singleton.Models.Services;
using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            IPersonRepository<Person> personRepository = PersonRepository.Instance;
            IPersonRepository<Person> personRepository2 = PersonRepository.Instance;

            Console.WriteLine(ReferenceEquals(personRepository, personRepository2));

            IRepository<string> stringRepository = Repository.Instance;

            foreach (Person item in personRepository.Get())
            {
                Console.WriteLine($"{item.Title} {item.LastName} {item.FirstName}");
            }
        }
    }
}
