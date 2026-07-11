namespace B_Dictionaries;

static class Program
{
    static void Main()
    {
        Dictionary<int, Employee> employees = new Dictionary<int, Employee>
        {
            {1, new Employee("John Does", 32, 100000)},
            {2, new Employee("John Doesnt", 25, 50000)},
            {3, new Employee("John Comes", 47, 200000)},
            {4, new Employee("John Will", 19, 80000)},
        };
        
        employees.Add(101, new Employee("Jane Doe", 22, 12800.589));
        employees[3] = new Employee("Mike Jagged", 27, 48123);

        foreach (KeyValuePair<int, Employee> employee in employees)
        {
            Console.WriteLine($"ID: {employee.Key} - {employee.Value.Name} is {employee.Value.Age}" +
                              $" years old and earns ${employee.Value.Salary:N02}");
        }

        Console.WriteLine("\n\n###############################\n\nNew Dictionary:");

        var codes = new Dictionary<string, string>
        {
            ["NY"] = "New Yok",
            ["CA"] = "California",
            ["TX"] = "Texas"
        };
        
        codes["NY"] = "New York";

        foreach (var (code, name) in codes)
        {
            Console.WriteLine($"   >> {code}: {name}");
        }

        Console.WriteLine("\nTryGetValue with \"NY\":");
        
        if (codes.TryGetValue("NY", out string? state))
            Console.WriteLine("   >> NY: " + state);

    }
}

class Employee (string name, int age, double salary)
{
    //READ-Only Expression
    public string Name => name;
    public double Salary => salary;
    public int Age => age;
}

