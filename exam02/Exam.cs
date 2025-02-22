using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        public int Time { get; set; }
        public int NoOfQuestion { get; set; }
        public List<Question> Questions { get; set; }

        public Exam(int time)
        {
            Time = time;
            Questions = new List<Question>();
        }

        public abstract void ShowExam();

        protected int GetUserAnswer(Question question)
        {
            bool showError = false;
            int userAnswerId;

            do
            {
                Console.WriteLine(question.ToString());

                if (showError)
                {
                    Console.WriteLine("Answer does not exist. Please try again.");
                }
                Console.Write("Enter your answer (number): ");
                string input = Console.ReadLine();

                bool isInteger = int.TryParse(input, out userAnswerId);
                bool answerExists = isInteger && question.AnswerList.Any(a => a.AnswerId == userAnswerId);
                showError = !answerExists;

            } while (showError);

            return userAnswerId;
        }

        public object Clone() => MemberwiseClone();

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;
            return Time.CompareTo(other.Time);
        }
    }

}
