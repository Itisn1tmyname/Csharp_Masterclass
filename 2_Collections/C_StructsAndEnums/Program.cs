namespace C_StructsAndEnums;

public enum Month{Jan = 1, Feb, Mar, Apr, May, Jun = 12, Jul, Aug, Sep, Oct, Nov, Dec};
public struct Point
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X},{Y})";
    }

    public double Distance(Point other)
    {
        return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }
}

public class ClassPoint
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public ClassPoint(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public override string ToString()
    {
        return $"({X},{Y})";
    }
}


class Program
{
    static void Main(string[] args)
    {
        Point A = new Point(1, 2);
        Point B = A;

        Console.WriteLine($"A{A} and B{B} as structs");
        
        B.X += 12;
        B.Y -= 1;
        
        Console.WriteLine($"A{A} and B{B} after changing B");
        Console.WriteLine($"Distance between A{A} and B{B}: {A.Distance(B):F2}");

        Console.WriteLine("\n################################################\n");
        Console.WriteLine("Now with classes:\n");
        
        ClassPoint C = new ClassPoint(1, 2);
        ClassPoint D = C;
        
        Console.WriteLine($"C{C} and D{D} as classes");
        
        D.X += 12;
        D.Y -= 1;
        
        Console.WriteLine($"C{C} and D{D} after changing D");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("CLASSES ARE REFERENCE TYPES AND STRUCTS ARE VALUE TYPES!");
        Console.ResetColor();


        Console.WriteLine("\n#################################################\n");
        Console.WriteLine("Testing Enums.");

        Console.WriteLine($"{Month.Feb} is month number {(int)Month.Feb}.\n" +
                          $"After assigning 12 to Jun, {Month.Jul} is month number {(int)Month.Jul}.");
        
        
        DateTime now = DateTime.Now;
        DateTime birth = new DateTime(1997, 4, 26);

        Console.WriteLine(birth.CompareTo(now));
        
        TimeSpan age = now.Subtract(birth);
        Console.WriteLine(age.Days);
    }
}