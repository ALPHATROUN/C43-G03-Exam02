namespace exam02
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            footballSubject.Exam?.ShowExam();

            // الكود دا ياخد النهاية عشان ريال مدريد بس والله 
            // الأنشلوت
        }

    }
}

