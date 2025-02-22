using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam02
{
    public class FinalExam : Exam
    {
        public FinalExam(int time) : base(time) { }

        public override void ShowExam()
        {
            int totalScore = 0;
            int totalPossible = Questions.Sum(q => q.Mark);

            Console.WriteLine($"Final Exam ({Time} minutes)\n");

            foreach (var question in Questions)
            {
                int userAnswerId = GetUserAnswer(question);

                if (userAnswerId == question.RightAnswer.AnswerId)
                {
                    totalScore += question.Mark;
                }

                Console.WriteLine();
            }

            Console.WriteLine($"Exam completed. Your total score: {totalScore}/{totalPossible}");
            Console.WriteLine("Correct Answers:");
            foreach (var question in Questions)
            {
                Console.WriteLine($"{question.Body} -> {question.RightAnswer}");
            }
        }
    }
    }
