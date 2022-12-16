using LAB3Test.Data;
using LAB3Test.Extras;
using System;
using System.Collections.Generic;

namespace LAB3Test.Models
{
    public partial class Class
    {
        public Class()
        {
            ClassPrograms = new HashSet<ClassProgram>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;

        public virtual ICollection<ClassProgram> ClassPrograms { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("1. Choose class to display");
            Console.WriteLine("0. Return to main menu");
            
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    AllClasses();
                    DisplayClass();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 0:
                    MenuClass.Run();
                    break;
            }
            
            
        }
        private static void AllClasses()
        {
            Console.WriteLine("Current classes in Sendelbach Highschool:");
            using (var context = new SenHSContext())
            {
                var allClasses = from c in context.Classes
                                 select c;

                foreach (var classes in allClasses)
                {
                    Console.WriteLine($"{classes.ClassName}");
                }
            }
        }
        public static void DisplayClass()
        {
            Console.WriteLine("What class do you want to display?");
            string choice = Console.ReadLine();
            using (var context = new SenHSContext())
            {
                var allStudents = from s in context.Students
                                  join e in context.Enrollments on s.StudentId equals e.FkStudentId
                                  join c in context.Classes on e.FkClassId equals c.ClassId
                                  where c.ClassName.Equals(choice)
                                  select new
                                  {
                                      FirstName = s.FirstName,
                                      LastName = s.LastName,
                                  };
                Console.WriteLine($"\nList of {choice}:");
                foreach (var students in allStudents)
                {

                    Console.WriteLine($"Name: {students.FirstName} {students.LastName}");
                }
            }
        }
    }
}
