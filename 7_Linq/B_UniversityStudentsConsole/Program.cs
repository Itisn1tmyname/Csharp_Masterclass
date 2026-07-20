using System.Xml.Serialization;

namespace B_UniversityStudentsConsole;

class UniversityManager
{
    private readonly List<Student> _students =
    [
        new (1, "Carla", "female", 17, 1),
        new (2, "Toni", "male", 21, 1),
        new (3, "Thomas", "male", 23, 1),
        new(4, "Leyla", "female", 19, 2),
        new (5, "James", "divers", 25, 2),
        new (6, "Linda", "female", 22, 2)
    ];
    private readonly List<University> _universities =
    [
        new() { Id = 1, Name = "Yale" },
        new () { Id = 2, Name = "Beijing Tech" }
    ];

    public void MaleStudents()
    {
        IEnumerable<Student> maleStudents =
            from student in _students
            where student.Gender == "male"
            select student;
        Console.WriteLine("Male students:");
        foreach (Student student in maleStudents)
            Console.WriteLine(student);
    }

    public void FemaleStudents()
    {
        IEnumerable<Student> femaleStudents =
            from student in _students
            where student.Gender == "female"
            select student;
        Console.WriteLine("Female students:");
        foreach (Student student in femaleStudents)
            Console.WriteLine(student);
    }

    public void SortStudentsByAge()
    {
        IEnumerable<Student> sortedStudents =
            from student in _students
            orderby student.Age
            select student;
        Console.WriteLine("Students sorted by age:");
        foreach (Student student in sortedStudents)
            Console.WriteLine(student);
    }

    public void StudentsFromBeijingTech()
    {
        IEnumerable<Student> bjtStudents =
            from student in _students
            join university in _universities
            on student.UniversityId equals university.Id
            where university.Name.Contains("Beijing")
            select student;
        Console.WriteLine("Beijing tech students:");
        foreach (Student student in bjtStudents)
            Console.WriteLine(student);
    }

    public void StudentAndUniversityNameCollection()
    {
        var newCollection =
            from student in _students
            join university in _universities
            on student.UniversityId equals university.Id
            orderby student.Name
            // select (student.Name, university.Name);
            select new { StudentName = student.Name, UniversityName = university.Name };

        Console.WriteLine("Students and their Universities: ");
        
        foreach(var item in newCollection)
            Console.WriteLine($"{item.StudentName} goes to the university {item.UniversityName}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Collection of query operators: https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/
        UniversityManager um = new();
        um.MaleStudents();
        Console.WriteLine();
        um.FemaleStudents();
        Console.WriteLine();
        um.SortStudentsByAge();
        Console.WriteLine();
        um.StudentsFromBeijingTech();
        Console.WriteLine();
        um.StudentAndUniversityNameCollection();
    }
}

#region Helper Classes
class University
{
    public required int Id { get; init; }
    public required string Name { get; init; }

    public override string ToString()
    {
        return $"University {Name} with ID {Id}";
    }
}

class Student (int id, string name, string gender, byte age, int universityId)
{
    public int Id { get; } = id;
    public string Name { get;} = name;
    public string Gender { get; } = gender;
    public byte Age { get; } = age;
    
    
    
    //Foreign key
    public int UniversityId { get; } = universityId;

    public override string ToString()
    {
        return $"Student {Name} ({Gender}) with ID {Id} is {Age} years old and goes to the University with ID {UniversityId}";
    }
}

#endregion