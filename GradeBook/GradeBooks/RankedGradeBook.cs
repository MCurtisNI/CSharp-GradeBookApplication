using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) :base(name, isWeighted)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
          if (Students.Count<5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            double StudentsPerGrade = Convert.ToDouble(Students.Count) / 5;
            var Allgrades =
                from student in Students
                where student.AverageGrade > averageGrade
                orderby student.AverageGrade
                select student.AverageGrade;
            var rank = (Convert.ToDouble(Allgrades.Count()) / StudentsPerGrade);
            if (rank < 1)
                return 'A';
            else if (rank < 2)
                return 'B';
            else if (rank < 3)
                return 'C';
            else if (rank < 4)
                return 'D';
            else return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count() < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStudentStatistics(name);
        }
    }
}
