using System;
using System.Collections.Generic;

namespace LAB3Test.Models
{
    public partial class ClassProgram
    {
        public int ClassProgramId { get; set; }
        public int FkCourseId { get; set; }
        public int FkClassId { get; set; }

        public virtual Class FkClass { get; set; } = null!;
        public virtual Course FkCourse { get; set; } = null!;
    }
}
