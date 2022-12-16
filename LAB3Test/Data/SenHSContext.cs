using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LAB3Test.Models;

namespace LAB3Test.Data
{
    public partial class SenHSContext : DbContext
    {
        public SenHSContext()
        {
        }

        public SenHSContext(DbContextOptions<SenHSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<ClassProgram> ClassPrograms { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseGrade> CourseGrades { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-RHB9JCL; Initial Catalog=SendelbachHighSchool; Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClassProgram>(entity =>
            {
                entity.ToTable("ClassProgram");

                entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.ClassPrograms)
                    .HasForeignKey(d => d.FkClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassProgram_Class");

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.ClassPrograms)
                    .HasForeignKey(d => d.FkCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassProgram_Course");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FkEmployeeId).HasColumnName("FK_EmployeeId");

                entity.HasOne(d => d.FkEmployee)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FkEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Employee");
            });

            modelBuilder.Entity<CourseGrade>(entity =>
            {
                entity.ToTable("CourseGrade");

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.GradeDate).HasColumnType("date");

                entity.Property(e => e.GradeValue)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.HasOne(d => d.FkCourse)
                    .WithMany(p => p.CourseGrades)
                    .HasForeignKey(d => d.FkCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseGrade_Course");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.CourseGrades)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseGrade_Student");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.Position)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("Enrollment");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FkClassId).HasColumnName("FK_ClassId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.FkClass)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.FkClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Class");

                entity.HasOne(d => d.FkStudent)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.FkStudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FirstName).HasMaxLength(30);

                entity.Property(e => e.LastName).HasMaxLength(30);

                entity.Property(e => e.PersonNumber)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
