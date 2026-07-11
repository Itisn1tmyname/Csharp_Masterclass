using System.Text.RegularExpressions;

namespace D_Regex;

class Program
{
    static void Main(string[] args)
    {
        string pattern = @"\d";
        Regex regex = new Regex(pattern);

        string text = "Hi there, my number is 12314\nAnd I think your number is 09834.";
        
        MatchCollection matches = regex.Matches(text);
        
        Console.WriteLine($"{matches.Count} matches found in this text:\n{text}\n");

        foreach (Match hit in matches)
        {
            GroupCollection group = hit.Groups;
            Console.WriteLine($"{group[0].Value} found at {group[0].Index}");
        }
    }
}