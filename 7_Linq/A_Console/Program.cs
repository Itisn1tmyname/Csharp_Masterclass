namespace A_Console;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 10, 3, 7, 2, 9, 5, 8, 4, 6 };
        
        OddNumbers(numbers);
        SortNumbers(numbers);
        
    }

    private static void OddNumbers(int[] numbers)
    {
        Console.WriteLine("Odd Numbers of the array: ");

        IEnumerable<int> oddNumbers = from number in numbers where number % 2 != 0 select number;
        
        Console.WriteLine(oddNumbers);
        
        Console.WriteLine(string.Join(", ", oddNumbers));
    }

    private static void SortNumbers(int[] numbers)
    {
        Console.WriteLine("Reverse-Sorted Numbers of the array: ");
        
        IEnumerable<int> sortedNumbers =
            from i in numbers
            orderby i descending
            select i;
        
        Console.WriteLine(string.Join(", ", sortedNumbers));
        
        var reversed = sortedNumbers.Reverse();

        Console.WriteLine("Sorted Numbers of the array: (Reversing the reverse)");
        Console.WriteLine(string.Join(", ", reversed));
    }
}