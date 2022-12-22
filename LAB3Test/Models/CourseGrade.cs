using LAB3Test.Data;
using LAB3Test.Extras;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LAB3Test.Models
{
    public partial class CourseGrade
    {
        public int CourseGradeId { get; set; }
        public int GradeValue { get; set; } 
        public DateTime GradeDate { get; set; }
        public int FkStudentId { get; set; }
        public int FkCourseId { get; set; }

        public virtual Course FkCourse { get; set; } = null!;
        public virtual Student FkStudent { get; set; } = null!;

        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Choose what information you want to display");
            Console.WriteLine("1. All grades");
            Console.WriteLine("2. Grades from last month");
            Console.WriteLine("3. Average, max and lowest grade per course");
            Console.WriteLine("0. Return to main menu");
            int choice;
            Int32.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    DisplayAllGrades();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 2:
                    DisplayGradesLatestMonth();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 3:
                    Course.DisplayCourse();
                    TextClass.PressEnter();
                    Run();
                    break;
                case 0:
                    MenuClass.Run();
                    break;
            }
        }

        public static void SetNewGrade()
        {
            
            using (var context = new SenHSContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    DateTime today = DateTime.Today;
                    Console.WriteLine("Set a new grade");
                    Console.WriteLine("Grade value:");
                    int gradeValue;
                    Int32.TryParse(Console.ReadLine(), out gradeValue);
                    Console.WriteLine("Student ID: ");
                    int studentId;
                    Int32.TryParse(Console.ReadLine(), out studentId);
                    Console.WriteLine("Course ID: ");
                    int courseId;
                    Int32.TryParse(Console.ReadLine(), out courseId);
                    try
                    {
                        CourseGrade cg = new CourseGrade()
                        {
                            GradeValue = gradeValue,
                            GradeDate = today,
                            FkStudentId = studentId,
                            FkCourseId = courseId
                        };
                        context.CourseGrades.Add(cg);
                        context.SaveChanges();
                        transaction.Commit();
                        Console.WriteLine("New grade is set");
                        Console.WriteLine("Database updated");
                        TextClass.PressEnter();
                        MenuClass.Run();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            
        }
        public static void DisplayAllGrades()
        {
            Console.WriteLine("Grade system:");
            Console.WriteLine("A = 6 | B = 5 | C = 4 | D = 3 | E = 2 | F = 1");
            using (var context = new SenHSContext())
            {
                var allGrades = from s in context.Students
                                join cg in context.CourseGrades on s.StudentId equals cg.FkStudentId
                                join c in context.Courses on cg.FkCourseId equals c.CourseId
                                join e in context.Employees on c.FkEmployeeId equals e.EmployeeId
                                select new
                                {
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                    Grade = cg.GradeValue,
                                    GradeDate = cg.GradeDate,
                                    CourseName = c.CourseName,
                                    TeacherFName = e.FirstName,
                                    TeacherLName = e.LastName
                                };
                foreach (var grades in allGrades)
                {
                    Console.WriteLine($"Name: {grades.FirstName} {grades.LastName} \nGrade: {grades.Grade} {grades.GradeDate} \nGrade by teacher: {grades.TeacherFName} {grades.TeacherLName} \nCourse: {grades.CourseName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
        public static void DisplayGradesLatestMonth()
        {
            Console.WriteLine("Grade system:");
            Console.WriteLine("A = 6 | B = 5 | C = 4 | D = 3 | E = 2 | F = 1");
            using (var context = new SenHSContext())
            {
                DateTime today = DateTime.Today;
                var allGrades = from s in context.Students
                                join cg in context.CourseGrades on s.StudentId equals cg.FkStudentId
                                join c in context.Courses on cg.FkCourseId equals c.CourseId
                                join e in context.Employees on c.FkEmployeeId equals e.EmployeeId
                                where cg.GradeDate.Month == today.Month -1 && cg.GradeDate.Year == today.Year
                                select new
                                {
                                    FirstName = s.FirstName,
                                    LastName = s.LastName,
                                    Grade = cg.GradeValue,
                                    GradeDate = cg.GradeDate,
                                    CourseName = c.CourseName,
                                    TeacherFName = e.FirstName,
                                    TeacherLName = e.LastName
                                };
                foreach (var grades in allGrades)
                {
                    Console.WriteLine($"Name: {grades.FirstName} {grades.LastName} \nGrade: {grades.Grade} {grades.GradeDate} \nGrade by teacher: {grades.TeacherFName} {grades.TeacherLName} \nCourse: {grades.CourseName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
        }
    }
}
