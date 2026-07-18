namespace B_UniversityStudentsConsole;

class Program
{
    static void Main(string[] args)
    {
        //Collection of query operators: https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/
        
        
        Console.WriteLine("Hello, World!");
    }
}

#region Helper Classes
class University (int id, string name)
{
    public required int Id { get; init; } = id;
    public required string Name { get; init; } = name;

    public void Print()
    {
        Console.WriteLine($"University {Name} with ID {Id}");
    }
}

class Student(int id, string name, string gender, byte age, int universityId)
{
    public required int Id { get; init; } = id;
    public required string Name { get; init; } = name;
    public required string Gender { get; init; } = gender;
    public required byte Age { get; set; } = age;
    
    //Foreign key
    public required int UniversityId { get; init; } = universityId;
    
    public void Print()
    {
        Console.WriteLine($"Student {Name} ({Gender}) with ID {Id} is {Age} years old and goes to the University with ID {UniversityId}");
    }
}

#endregion