using LAB3Test.Data;
using LAB3Test.Extras;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LAB3Test.Models
{
    public partial class Course
    {
        public Course()
        {
            ClassPrograms = new HashSet<ClassProgram>();
            CourseGrades = new HashSet<CourseGrade>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int FkEmployeeId { get; set; }

        public virtual Employee FkEmployee { get; set; } = null!;
        public virtual ICollection<ClassProgram> ClassPrograms { get; set; }
        public virtual ICollection<CourseGrade> CourseGrades { get; set; }

        public static void DisplayAllCourses()
        {
            using (var context = new SenHSContext())
            {
                var allCourses = from t in context.Employees
                                 join c in context.Courses on t.EmployeeId equals c.FkEmployeeId
                                 orderby t.LastName
                                 select new
                                 {
                                     FirstName = t.FirstName,
                                     LastName = t.LastName,
                                     CourseName = c.CourseName
                                 };

                foreach (var courses in allCourses)
                {
                    Console.WriteLine($"Course: {courses.CourseName} \nTeacher: {courses.FirstName} {courses.LastName}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
            TextClass.PressEnter();
            MenuClass.Run();
        }
        public static void DisplayCourse()
        {
           using (var context = new SenHSContext())
            {
                var gradeCourses = from c in context.Courses
                                 join cg in context.CourseGrades on c.CourseId equals cg.FkCourseId
                                 group cg.GradeValue by c.CourseName into n
                                 select new
                                 {
                                     CourseName = n.Key,
                                     AveGrade = n.Average(),
                                     MaxGrade = n.Max(),
                                     LowGrade = n.Min()
                                 };
                
                foreach (var item in gradeCourses)
                {
                    Console.WriteLine($"Course: {item.CourseName} \nAverage grade:{item.AveGrade} | Max grade:{item.MaxGrade} | Lowest grade:{item.LowGrade}");
                    Console.WriteLine(new string('-', (30)));
                }
            }
            TextClass.PressEnter();
            MenuClass.Run();
        }
    }
}
