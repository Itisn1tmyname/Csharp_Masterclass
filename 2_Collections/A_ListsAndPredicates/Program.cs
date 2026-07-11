namespace A_Lists;

class Program
{
    static void Main(string[] args)
    {
        List<int> list = new List<int>() {1,15,4,18,34,1,5,6,3,45 };

        Console.Write("The starting List:\n   >> ");

        foreach (int i in list)
        {
            Console.Write($"{i} ");
        }
        
        
        // Predicates define a set of criteria and return whether a specified object
        // meets those criteria. In this case, the criteria is to be an int and greater
        // than 10.
        Predicate<int> isGreaterThanTen = x => x > 10;
        
        // The same thing as a method would look like this:
        static bool IsGreaterThanTen(int x)
        {
            return x > 10;
        }
        
        // A predicate is a type of delegate; And delegates allow to store a method as a
        // variable, to use them as arguments in other methods and to call them later.
        // Therefore, we can assign that method to the predicate:
        
        isGreaterThanTen = IsGreaterThanTen; //IMPORTANT: Method without the Brackets(); !
        
        //FindAll filters the list and returns a new one, taking a predicate as the argument
        List<int> higherThanTen = list.FindAll(isGreaterThanTen);

        Console.Write("\n\nThe filtered List with only numbers greater than ten:\n   >> ");
        
        foreach (int i in higherThanTen)
            Console.Write($"{i} ");
        
        if (higherThanTen.Any(x => x % 17 == 0))
            Console.WriteLine("\n\nThe List contains a number that is divisible by 17.");
        else 
            Console.WriteLine("\n\nThe List doesn't contain a number that is divisible by 17.");


        List<Product> products = new List<Product>
        {
            new Product {Name = "Apple", Price = 0.5},
            new Product {Name = "Orange", Price = 0.8},
            new Product {Name = "Banana", Price = 2.1},
            new Product {Name = "Pear", Price = 3.2}
        };
        
        //Filtering using LINQ
        List<Product> cheapProducts = products.Where(x => x.Price < 1.0).ToList();
        //Filtering without LINQ
        cheapProducts = cheapProducts.FindAll(x => x.Price < 1.0);

        Console.WriteLine("\n\n A list of products that cost less than $1.");
        
        foreach (var cp in cheapProducts)
        {
            Console.WriteLine($"{cp.Name} - {cp.Price}$");
        }
    }
}

public class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
}