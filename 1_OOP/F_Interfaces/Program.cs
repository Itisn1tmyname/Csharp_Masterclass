namespace F_Interfaces;

public interface IAnimal
{
    /// <summary>
    /// The Animal should be able to make a sound.
    /// </summary>
    void MakeSound();
    
    /// <summary>
    /// The Animal should be able to eat.
    /// </summary>
    /// <param name="food">The food that is being eaten.</param>
    void Eat(string food);
}

public class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Bark.");
    }

    public void Eat(string food)
    {
        Console.WriteLine($"Dog ate {food}.");
    }
}

public class Cat : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Meow.");
    }

    public void Eat(string food)
    {
        Console.WriteLine($"Cat ate {food}.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Dog dog = new Dog();
        dog.MakeSound();
        dog.Eat("treat");
        
        Cat cat = new Cat();
        cat.MakeSound();
        cat.Eat("fish");

        Console.WriteLine("\n####################\n");
        
        IPaymentProcessor creditCardProcessor = new CreditCardProcessor();
        PaymentService paymentService = new PaymentService(creditCardProcessor);
        paymentService.ProcessOrderPayment(123.3m);

        IPaymentProcessor paypalProcessor = new PaypalProcessor();
        paymentService = new PaymentService(paypalProcessor);
        paymentService.ProcessOrderPayment(123.4m);
    }
}