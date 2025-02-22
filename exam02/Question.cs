using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public abstract class Question : ICloneable, IComparable<Question>
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public List<Answer> AnswerList { get; set; }
        public Answer RightAnswer { get; set; }

        protected Question(string header, string body, int mark, List<Answer> answers, Answer rightAnswer)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Mark = mark;
            AnswerList = answers ?? throw new ArgumentNullException(nameof(answers));
            RightAnswer = rightAnswer ?? throw new ArgumentNullException(nameof(rightAnswer));
        }

        public override string ToString() =>
            $"{Header}\n{Body}\nMarks: {Mark}\nAnswers:\n{string.Join("\n", AnswerList)}";

        public object Clone() => MemberwiseClone();

        public int CompareTo(Question? other)
        {
            if (other == null) return 1;
            return Mark.CompareTo(other.Mark);
        }
    }

}
