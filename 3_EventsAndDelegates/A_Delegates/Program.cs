using System.Security.Cryptography;

namespace A_Delegates;

class Program
{
    //Declaration
    public delegate int Comparison<T>(T x, T y);

    public class PersonSorter
    {
        public void Sort(Person[] people, Comparison<Person> comparison)
        {
            int num = 0;
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                num++;
                for (int i = 0; i < people.Length - num; i++)
                {
                    if (comparison(people[i], people[i + 1]) > 0)
                    {
                        sorted = false;
                        (people[i], people[i + 1]) = (people[i + 1], people[i]);
                    }
                }
            }
        }
    }
    
    static void Main(string[] args)
    {
        Person[] people =
        [
            new(){Age = 10, FirstName="Bert", LastName="Xena"},
            new(){Age = 12, FirstName="Anja", LastName="Xena"},
            new(){Age = 14, FirstName="Ginny", LastName="Rent"},
            new(){Age = 15, FirstName="Minnie", LastName="Precht"},
            new(){Age = 17, FirstName="Albert", LastName="Ossam"},
            new(){Age = 18, FirstName="Joe", LastName="Lambert"},
            new(){Age = 19, FirstName="Amy", LastName="Kunerdt"},
            new(){Age = 21, FirstName="Bello", LastName="Gent"},
            new(){Age = 22, FirstName="John", LastName="Doe"},
            new(){Age = 22, FirstName="Jane", LastName="Doe"},
            new(){Age = 24, FirstName="Minerva", LastName="Albert"},
            new(){Age = 26, FirstName="Kunz", LastName="Albert"}
        ];

        // Console.WriteLine("\n\"Unsorted\":\n");
        // Person.PrintPeople(people);

        Console.WriteLine("\nUnsorted:\n");
        Person.MixPeople(people);
        Person.PrintPeople(people);

        Console.WriteLine("\n\nSorted by Age:\n");
        PersonSorter sorter = new PersonSorter();
        sorter.Sort(people, CompareByAge);
        
        Person.PrintPeople(people);

        Console.WriteLine("Sorted by Name:\n");
        sorter.Sort(people, CompareByName);
        
        Person.PrintPeople(people);
    }

    public static int CompareByAge(Person x, Person y)
    {
        return x.Age.CompareTo(y.Age);
    }


    public static int CompareByName(Person x, Person y)
    {
        return x.LastName.CompareTo(y.LastName) == 0 ? x.FirstName.CompareTo(y.FirstName)
            : x.LastName.CompareTo(y.LastName);
    }
    
    public class Person
    {
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public static void PrintPeople(Person[] people)
        {
            foreach (Person person in people)
                Console.WriteLine($"{person.LastName}, {person.FirstName} ({person.Age})");
            Console.WriteLine("\n##################\n");
        }

        public static void MixPeople(Person[] people)
        {
            for (int i = 0; i < 100; i++)
            {
                int a = RandomNumberGenerator.GetInt32(0, people.Length);
                int b = RandomNumberGenerator.GetInt32(0, people.Length);
                (people[a], people[b]) = (people[b], people[a]);
            }
        }
    }
    
}