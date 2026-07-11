using System.Globalization;

namespace E_Inheritance;

public class Person
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine("Person constructor called.");
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}");
    }
    
}

public class Employee : Person
{
    public string JobTitle { get; private set; }
    public int EmployeeID { get; private set; }
    
    public Employee(string name, int age, string jobTitle, int employeeId)
        : base(name, age) //Calling the base class constructor
    {
        JobTitle = jobTitle;
        EmployeeID = employeeId;
        Console.WriteLine("Employee (derived class) constructor called.");
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Job Title: {JobTitle}, Employee ID: {EmployeeID}");
    }
}

public sealed class Manager : Employee
{
    public int TeamSize { get; private set; }
    
    public Manager(string name, int age, string jobTitle, int employeeId, int teamSize)
        : base(name, age, jobTitle, employeeId)
    {
        TeamSize = teamSize;
    }
    
    /// <summary>
    /// Displays the info of a Manager, including name, age, job title, employee ID and Team Size.
    /// Prints it to the console.
    /// </summary>
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Team Size: {TeamSize}");
    }
}