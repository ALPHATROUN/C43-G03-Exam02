using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string body, int mark, bool isTrue) :
            base("T/F", body, mark, GenerateAnswers(), GetRightAnswer(isTrue))
        { }

        private static List<Answer> GenerateAnswers() =>
            new List<Answer> { new Answer(0, "False"), new Answer(1, "True") };

        private static Answer GetRightAnswer(bool isTrue) =>
            isTrue ? new Answer(1, "True") : new Answer(0, "False");
    }

    public class MCQ : Question
    {
        public MCQ(string body, int mark, List<Answer> answers, Answer rightAnswer) :
            base("Multiple Choice Question:", body, mark, answers, rightAnswer)
        { }
    }


}
