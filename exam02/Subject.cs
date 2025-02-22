using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class Subject : ICloneable, IComparable<Subject>
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public Exam? Exam { get; set; }

        public void CreateExam(Exam exam) => Exam = exam ?? throw new ArgumentNullException(nameof(exam));

        public object Clone() => MemberwiseClone();

        public int CompareTo(Subject? other)
        {
            if (other == null) return 1;
            return SubjectId.CompareTo(other.SubjectId);
        }
    }


}
