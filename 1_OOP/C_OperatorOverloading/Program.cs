namespace C_OperatorOverloading;

class Program
{
    static void Main(string[] args)
    {
        Vector a = new  Vector(1, 2);
        Vector b = new  Vector(3, 4);
        
        a.Display();
        b.Display();

        Console.WriteLine("\nAddition:\n");
        
        Vector c = a + b;
        c.Display();
    }

    internal class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Display()
        {
            Console.WriteLine($"Vector: ({X}, {Y})");
        }

        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    }
}