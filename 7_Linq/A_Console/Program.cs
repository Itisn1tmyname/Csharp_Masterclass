namespace A_Console;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        
        OddNumbers(numbers);
        
    }

    private static void OddNumbers(int[] numbers)
    {
        Console.WriteLine("Odd Numbers of the array: ");

        IEnumerable<int> oddNumbers = from number in numbers where number % 2 != 0 select number;
        
        Console.WriteLine(oddNumbers);
        
        Console.WriteLine(string.Join(", ", oddNumbers));
    }
}