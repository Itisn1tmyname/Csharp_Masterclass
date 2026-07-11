namespace D_ProjectClasses;

class Program
{
    static void Main(string[] args)
    {
        Question[] questions = { 
            new Question("What is the capital of Germany?",
            new string[] { "Paris", "Berlin", "London", "Madrid" },
            1),
            new Question("What is 2+2?", 
            new string[] { "2", "3", "4", "5" },
            2),
            new Question("Who wrote 'Hamlet'?",
            new string[] { "Goethe", "Austen", "Dickens", "Shakespeare" },
            3),
            new Question("Why are plants green?",
            new string[] { "Chlorophyll", "Cholophile", "Chloroform", "Cholophone" },
            0)
            
        };

        Quiz myQuiz = new Quiz(questions);
        myQuiz.StartQuiz();
    }
}