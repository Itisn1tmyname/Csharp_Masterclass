namespace F_Interfaces;

public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
}

public class CreditCardProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment of {amount}");
    }
}

public class PaypalProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing paypal payment of {amount}");
    }
}

public class PaymentService
{
    private readonly IPaymentProcessor _paymentProcessor;
    
    public PaymentService(IPaymentProcessor processor)
    {
        _paymentProcessor = processor;
    }
    
    public void ProcessOrderPayment(decimal amount)
    {
        _paymentProcessor.ProcessPayment(amount);
    }
}