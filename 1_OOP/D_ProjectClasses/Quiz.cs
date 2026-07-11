namespace D_ProjectClasses;

internal class Quiz
{
    private Question[] _questions;
    private int _score;


    public Quiz(Question[] questions)
    {
        this._questions = questions;
        _score = 0;
    }

    public void StartQuiz()
    {
        Console.WriteLine("Welcome to Quiz Game!");
        int questionNumer = 1;
        foreach (Question question in _questions)
        {
            Console.WriteLine($"{questionNumer++}:");
            DisplayQuestion(question);
            if (question.GetAndEvaluateUserChoice())
            {
                Console.WriteLine("Correct");
                _score++;
            }
            else
            {
                Console.WriteLine($"Wrong! The correct answer is answer number {question.CorrectAnswerIndex + 1}: " +
                                  $"{question.Answers[question.CorrectAnswerIndex]}");
            }
        }
        DisplayResult();
    }
    
    private void DisplayQuestion(Question question)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("""
                          ╔══════════════════════════════════════════════════════════╗
                          ║                         Question                         ║
                          ╚══════════════════════════════════════════════════════════╝
                          """);
        Console.ResetColor();
        
        Console.WriteLine(question.QuestionText);

        for (int i = 0; i < question.Answers.Length; i++)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"   {i+1}");
            Console.ResetColor();
            Console.WriteLine($". {question.Answers[i]}");
        }
    }

    private void DisplayResult()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("""
                          ╔══════════════════════════════════════════════════════════╗
                          ║                         Results                          ║
                          ╚══════════════════════════════════════════════════════════╝
                          """);
        Console.ResetColor();

        Console.WriteLine($"Quiz finished. Your score is {_score} out of {_questions.Length}.");
        
        double result = (double)_score/_questions.Length;
        if (result > 0.8)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Excellent work!");
        }
        else if (result >= 0.5)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Good Effort!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Keep practicing!");
        }
        Console.ResetColor();
    }
    
    
}