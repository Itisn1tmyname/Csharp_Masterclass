namespace D_Project;

internal class Question
{
    public string QuestionText { get; set; }
    public string[] Answers { get; set; }
    private int CorrectAnswerIndex { get; set; }

    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswerIndex = correctAnswerIndex;
    }
    
    
    public bool IsCorrectAnswer(int choiceIndex) => CorrectAnswerIndex == choiceIndex;
    
    
    //TODO: Nach Quiz.cs verschieben
    private bool CheckUserChoice(Question question)
    {
        int choice;
        do
        {
            Console.Write("Please enter the ordinal of the correct answer as a whole number.\n   >> ");
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > question.Answers.Length)
                Console.WriteLine("Invalid choice, please enter a number between 1 and " + question.Answers.Length);
            else break;
        } while(true);
        return --choice == question.CorrectAnswerIndex;
    }
    
    //TODO: In Methode im Quiz.cs verschieben
    /*
     * if (CheckUserChoice)
     * {
     *      Console.WriteLine("Correct");
     * }
     * else
     * {
     *      Console.WriteLine("Incorrect");
     * }
     */
}