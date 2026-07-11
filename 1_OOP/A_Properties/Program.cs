namespace A_Properties;

class Program
{
    static void Main(string[] args)
    {
        Car audi = new Car("Audi", "A3", false);
        Car bmw = new Car("BMW", "i7", true);

        Console.WriteLine("Please enter the Brand name");
        audi.Brand = Console.ReadLine();

        Console.WriteLine("You entered the Brand " + audi.Brand);
        Console.WriteLine("The other car's brand is  " + bmw.Brand);
        
    }
}