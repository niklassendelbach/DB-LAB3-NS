using LAB3Test.Data;
using LAB3Test.Extras;
using System;
using System.Collections.Generic;

namespace LAB3Test.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Courses = new HashSet<Course>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Position { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Choose what information you want to display");
            Console.WriteLine("1. All employees");
            Console.WriteLine("2. Display by position");
            Console.WriteLine("0. Return to main menu");
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    DisplayEmployee();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 2:
                    DisplayChoice();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 0:
                    MenuClass.Run();
                    break;
            }
        }
        public static void DisplayChoice()
        {
            Console.WriteLine("What position do you want to display?");
            Console.WriteLine("Teacher\nHeadmaster\nJanitor");
            Console.WriteLine("Write your choice:");
            string choice = Console.ReadLine();
            using (var context = new SenHSContext())
            {
                var allEmployees = from c in context.Employees
                                   where c.Position.Equals(choice)
                                   select c;
                Console.WriteLine($"\nList of {choice}:");
                foreach (var employees in allEmployees)
                {
                    
                    Console.WriteLine($"Name: {employees.FirstName} {employees.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void DisplayEmployee()
        {
            using (var context = new SenHSContext())
            {
                var allEmployees = from c in context.Employees
                                  orderby c.LastName
                                  select c;

                foreach (var employees in allEmployees)
                {
                    Console.WriteLine($"Name: {employees.FirstName} {employees.LastName} \nPosition: {employees.Position}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void AddEmployee()
        {
            Console.WriteLine("Add new employee");
            Console.WriteLine("First name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last name:");
            string lastname = Console.ReadLine();
            Console.WriteLine("Position: ");
            string position = Console.ReadLine();

            using SenHSContext context = new SenHSContext();
            Employee employee = new Employee()
            {
                FirstName = firstname,
                LastName = lastname,
                Position = position
            };
            context.Employees.Add(employee);
            context.SaveChanges();
            Console.WriteLine("Database updated");
        }
    }
}
