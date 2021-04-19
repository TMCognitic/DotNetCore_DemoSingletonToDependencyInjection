using InversionOfControl.Models.Entities;
using InversionOfControl.Models.Interfaces;
using System;

namespace InversionOfControl
{
    class Program
    {
        static void Main(string[] args)
        {
            IPersonRepository<Person> personRepository = ResourceLocator.Instance.PersonRepository;
            IPersonRepository<Person> personRepository2 = ResourceLocator.Instance.PersonRepository;

            Console.WriteLine(ReferenceEquals(personRepository, personRepository2));

            //IRepository<string> stringRepository = ResourceLocator.Instance.Repository;
            //IRepository<string> stringRepository2 = ResourceLocator.Instance.Repository;

            //Console.WriteLine(ReferenceEquals(stringRepository, stringRepository2));

            foreach (Person item in personRepository.Get())
            {
                Console.WriteLine($"{item.Title} {item.LastName} {item.FirstName}");
            }
        }
    }
}
