using LAB3Test.Data;
using LAB3Test.Extras;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;

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

        public static void DisplayCourseGrade()
        {
            Console.WriteLine("SQL");
        }
        public static void DisplayGradesCurrentMonth()
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
                                    CourseName = c.CourseName
                                };
                foreach (var grades in allGrades)
                {
                    Console.WriteLine($"Name:{grades.FirstName} {grades.LastName} \nGrade:{grades.Grade} {grades.GradeDate} \nCourse: {grades.CourseName}");
                    Console.WriteLine("-----------------------------------------");
                }
                TextClass.PressEnter();
                MenuClass.Run();
            }
        }
    }
}
