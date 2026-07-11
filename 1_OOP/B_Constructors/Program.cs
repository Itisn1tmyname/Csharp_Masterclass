namespace B_Constructors;

class Program
{
    static void Main(string[] args)
    {
        Customer earl = new  Customer("Earl");
        Console.WriteLine($"Name of the customer ist {earl.Name}");
        Customer frankTheTank = new Customer("Frank The Tank", "Mainstreet 1", "01234 2342534");
        
        frankTheTank.SetDetails(name: "Frank", "Mainstreet 2", "555 123123");
        frankTheTank.Password = "w30984urlns";
    }
}