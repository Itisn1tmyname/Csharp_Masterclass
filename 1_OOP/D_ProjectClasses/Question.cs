namespace D_ProjectClasses;

internal class Question
{
    public string QuestionText { get; set; }
    public string[] Answers { get; set; }
    public int CorrectAnswerIndex { get; set; }

    public Question(string questionText, string[] answers, int correctAnswerIndex)
    {
        QuestionText = questionText;
        Answers = answers;
        CorrectAnswerIndex = correctAnswerIndex;
    }
    
    public bool GetAndEvaluateUserChoice()
    {
        int choice;
        do
        {
            Console.Write("Please enter the ordinal of the correct answer as a whole number.\n   >> ");
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > Answers.Length)
                Console.WriteLine("Invalid choice, please enter a number between 1 and " + Answers.Length);
            else break;
        } while(true);
        return --choice == CorrectAnswerIndex;
    }
}