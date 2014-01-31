using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication31
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new PersonsDbContext())
            {
                var person = new Person
                    {
                        FirstName = "Alex",
                        LastName = "Friedman",
                        Age = 31
                    };

                var order = new Order
                    {
                        ProductName = "Prod 1",
                        Quantity = 88
                    };
                person.Orders.Add(order);
                dbContext.Persons.Add(person);
                dbContext.SaveChanges();
            }

            Console.WriteLine("Done");
            Console.ReadKey(true);
        }
    }

    [Table("Persons")]
    public class Person
    {
        public Person()
        {
            this.Orders = new List<Order>();
        }
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        [ForeignKey("PersonId")]
        public virtual ICollection<Order> Orders { get; set; } 

        public override string ToString()
        {
            return String.Format("Id: {0}, First Name: {1}, Last Name: {2}, Age: {3}",
                                 Id, FirstName, LastName, Age);
        }
    }

    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }

    public class PersonsDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    [Table("Cars")]
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
    }
}
