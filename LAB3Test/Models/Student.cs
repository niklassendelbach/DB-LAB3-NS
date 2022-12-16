using LAB3Test.Data;
using LAB3Test.Extras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.ExceptionServices;

namespace LAB3Test.Models
{
    public partial class Student
    {
        public Student()
        {
            CourseGrades = new HashSet<CourseGrade>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int StudentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonNumber { get; set; } = null!;

        public virtual ICollection<CourseGrade> CourseGrades { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Display all students and choose how you want to sort the list:");
            Console.WriteLine("1. First name ascending");
            Console.WriteLine("2. First name descending");
            Console.WriteLine("3. Last name ascending");
            Console.WriteLine("4. Last name descending");
            Console.WriteLine("0. Return to main menu");
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    DisplayStudentFnAsc();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 2:
                    DisplayStudentFnDesc();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 3:
                    DisplayStudentLnAsc();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 4:
                    DisplayStudentLnDesc();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 0:
                    MenuClass.Run();
                    TextClass.PressEnter();
                    break;
            }
        }
        public static void DisplayStudentFnAsc()
        {
            Console.WriteLine("Sorted by first name ascending:\n");
            using(var context = new SenHSContext())
            {
                var allStudents = from c in context.Students
                               orderby c.FirstName 
                               select c;
                               
                foreach (var students in allStudents)
                {
                    Console.WriteLine($"Name: {students.FirstName} {students.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void DisplayStudentFnDesc()
        {
            Console.WriteLine("Sorted by first name descending\n");
            using (var context = new SenHSContext())
            {
                var allStudents = from c in context.Students
                                  orderby c.FirstName descending
                                  select c;

                foreach (var students in allStudents)
                {
                    Console.WriteLine($"Name: {students.FirstName} {students.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void DisplayStudentLnAsc()
        {
            Console.WriteLine("Sorted by last name ascending:\n");
            using (var context = new SenHSContext())
            {
                var allStudents = from c in context.Students
                                  orderby c.LastName
                                  select c;

                foreach (var students in allStudents)
                {
                    Console.WriteLine($"Name: {students.FirstName} {students.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void DisplayStudentLnDesc()
        {
            Console.WriteLine("Sorted by last name descending:\n ");
            using (var context = new SenHSContext())
            {
                var allStudents = from c in context.Students
                                  orderby c.LastName descending
                                  select c;

                foreach (var students in allStudents)
                {
                    Console.WriteLine($"Name: {students.FirstName} {students.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void AddStudent()
        {
            Console.WriteLine("Add new student");
            Console.WriteLine("First name:");
            string firstname = Console.ReadLine();
            Console.WriteLine("Last name:");
            string lastname = Console.ReadLine();
            Console.WriteLine("Personal identity number: ");
            string personnumber = Console.ReadLine();

            using SenHSContext context = new SenHSContext();
            Student student = new Student()
            {
                FirstName = firstname,
                LastName = lastname,
                PersonNumber = personnumber
            };
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine("Database updated");
        }
    }
}
