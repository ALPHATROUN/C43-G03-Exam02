using System;
using System.Collections.Generic;
using System.Linq;
// الأنشلوت
namespace OOP_exam
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
    // الأنشلوت
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
    //الأنشلوت
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
    //الأنشلوت
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
        //الأنشلوت
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
        // الأنشلوت
        public object Clone() => MemberwiseClone();

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;
            return Time.CompareTo(other.Time);
        }
    }

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
            //الأنشلوت
            Console.WriteLine($"Exam completed. Your total score: {totalScore}/{totalPossible}");
            Console.WriteLine("Correct Answers:");
            foreach (var question in Questions)
            {
                Console.WriteLine($"{question.Body} -> {question.RightAnswer}");
            }
        }
    }
    // الأنشلوت
    public class PracticalExam : Exam
    {
        public PracticalExam(int time) : base(time) { }

        public override void ShowExam()
        {
            int totalScore = 0;
            int totalPossible = Questions.Sum(q => q.Mark);

            Console.WriteLine($"Practical Exam ({Time} minutes)\n");

            foreach (var question in Questions)
            {
                Console.Clear();
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
    // الأنشلوت
    class Program
    {
        static void Main(string[] args)
        {
            // الأنشلوت
            Subject footballSubject = new Subject()
            {
                SubjectId = 1,
                SubjectName = "Real Madrid Football Club",
            };

            FinalExam final = new FinalExam(120);

            final.Questions.Add(new TrueFalseQuestion("Real Madrid was founded in 1902.", 1, true));
            final.Questions.Add(new TrueFalseQuestion("Santiago Bernabéu is the current president of Real Madrid.", 1, false));
            final.Questions.Add(new TrueFalseQuestion("Real Madrid has won more Champions League titles than any other club.", 1, true));
            final.Questions.Add(new TrueFalseQuestion("Cristiano Ronaldo is Real Madrid's all-time top scorer.", 1, true));

            final.Questions.Add(new MCQ("Who is Real Madrid's all-time top scorer?", 2,
                new List<Answer>
                {
                    new Answer(1, "Raúl"),
                    new Answer(2, "Cristiano Ronaldo"),
                    new Answer(3, "Alfredo Di Stéfano"),
                    new Answer(4, "Karim Benzema"),
                },
                new Answer(2, "Cristiano Ronaldo")
            ));

            final.Questions.Add(new MCQ("Which player has the most appearances for Real Madrid?", 2,
                new List<Answer>
                {
                    new Answer(1, "Raúl"),
                    new Answer(2, "Iker Casillas"),
                    new Answer(3, "Sergio Ramos"),
                    new Answer(4, "Karim Benzema"),
                },
                new Answer(3, "Sergio Ramos")
            ));

            final.Questions.Add(new MCQ("What is Real Madrid's nickname?", 2,
                new List<Answer>
                {
                    new Answer(1, "The Whites"),
                    new Answer(2, "The Galácticos"),
                    new Answer(3, "The Royals"),
                    new Answer(4, "The Vikings"),
                },
                new Answer(2, "The Galácticos")
            ));

            footballSubject.CreateExam(final);

            // الأنشلوت
            Subject bayernSubject = new Subject()
            {
                SubjectId = 2,
                SubjectName = "Bayern Munich Football Club",
            };

            PracticalExam practical = new PracticalExam(90);

            practical.Questions.Add(new MCQ("Who is Bayern Munich's all-time top scorer?", 2,
                new List<Answer>
                {
                    new Answer(1, "Gerd Müller"),
                    new Answer(2, "Robert Lewandowski"),
                    new Answer(3, "Thomas Müller"),
                    new Answer(4, "Franck Ribéry"),
                },
                new Answer(1, "Gerd Müller")
            ));

            practical.Questions.Add(new MCQ("Which player has the most appearances for Bayern Munich?", 2,
                new List<Answer>
                {
                    new Answer(1, "Oliver Kahn"),
                    new Answer(2, "Philipp Lahm"),
                    new Answer(3, "Thomas Müller"),
                    new Answer(4, "Bastian Schweinsteiger"),
                },
                new Answer(3, "Thomas Müller")
            ));

            practical.Questions.Add(new MCQ("What is Bayern Munich's nickname?", 2,
                new List<Answer>
                {
                    new Answer(1, "The Bavarians"),
                    new Answer(2, "The Reds"),
                    new Answer(3, "The Lions"),
                    new Answer(4, "The Giants"),
                },
                new Answer(1, "The Bavarians")
            ));

            practical.Questions.Add(new MCQ("How many Bundesliga titles has Bayern Munich won?", 2,
                new List<Answer>
                {
                    new Answer(1, "30"),
                    new Answer(2, "32"),
                    new Answer(3, "33"),
                    new Answer(4, "35"),
                },
                new Answer(3, "33")
            ));

            practical.Questions.Add(new MCQ("Who is Bayern Munich's current manager?", 2,
                new List<Answer>
                {
                    new Answer(1, "Julian Nagelsmann"),
                    new Answer(2, "Thomas Tuchel"),
                    new Answer(3, "Hans-Dieter Flick"),
                    new Answer(4, "Pep Guardiola"),
                },
                new Answer(2, "Thomas Tuchel")
            ));

            bayernSubject.CreateExam(practical);

            //  لو بشمهندس عبدالرحمن اللى هيصحح فا أن شاء الله تطلع مدريردى يا هندسه:)
            // أما لو بشمندس أحمد فى أنى أسف
            Console.WriteLine("Which exam do you want to take?");
            Console.WriteLine("1. Practical Exam (Bayern Munich)");
            Console.WriteLine("2. Final Exam (Real Madrid)");
            Console.Write("Enter your choice (1 or 2): ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                bayernSubject.Exam?.ShowExam(); //  البايرن نادى القضوه والله
            }
            else if (choice == "2")
            {
                footballSubject.Exam?.ShowExam(); // الملكى
            }
            else
            {
                Console.WriteLine("Invalid choice. Exiting the program.");
            }

            // الكود دا ياخد النهاية عشان ريال مدريد بس والله 
            // الأنشلوت
        }
    }
}