using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class Answer : ICloneable, IComparable<Answer>
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Answer(int id, string text)
        {
            AnswerId = id;
            AnswerText = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString() => $"{AnswerId}. {AnswerText}";

        public object Clone() => new Answer(AnswerId, AnswerText);

        public int CompareTo(Answer? other)
        {
            if (other == null) return 1;
            return AnswerId.CompareTo(other.AnswerId);
        }
    }

}
