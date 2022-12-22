using LAB3Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB3Test
{
    public class MenuClass
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Welcome!");
            Console.WriteLine("You get to choose on the different options below:");
            Console.WriteLine("1. Employee information");
            Console.WriteLine("2. Student information");
            Console.WriteLine("3. Class information");
            Console.WriteLine("4. Display all grades from last month");
            Console.WriteLine("5. Display all courses and average grade");
            Console.WriteLine("6. Add new students");
            Console.WriteLine("7. Add new employees");
            Console.WriteLine("8. Set new grade");
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    Employee.Run();
                    break;
                case 2:
                    Student.Run();
                    break;
                case 3:
                    Class.Run();
                    break;
                case 4:
                    CourseGrade.DisplayGradesCurrentMonth();
                    break;
                case 5:
                    Course.DisplayCourse();
                    break;
                case 6:
                    Student.AddStudent();
                    break;
                case 7:
                    Employee.AddEmployee();
                    break;
                case 8:
                    CourseGrade.SetNewGrade();
                    break;
                default:
                    Console.WriteLine("Please make your choice in the menu");
                    break;
            }
        }
    }
}
