namespace E_Inheritance;

class Program
{
    static void Main(string[] args)
    {
        Dog dog = new Dog();
        Animal dawg = new Dog();
        Cat cat = new Cat();
        Animal catty = new Cat();
        
        dog.MakeSound();
        dawg.MakeSound();   //Overridden Method
        cat.MakeSound();
        catty.MakeSound();  //Hidden Method
        dog.Eat();
        
        Console.WriteLine("\n######################\n");
        
        Employee joe = new Employee("Joe", 123, "Sales Rep", 
            36423);
        Manager carl = new Manager("Carl", 32, "Manager", 
            1231234, 7);
        
        joe.DisplayInfo();
        carl.DisplayInfo();
    }
}


//Base class, parent class, Superclass
class Animal
{
    //Virtual Keyword: signifies that this method (or a field, etc.) can be overridden
    public virtual void MakeSound()
    {
        Console.WriteLine("Making a generic sound...");
    }
    
    public void Eat()
    {
        Console.WriteLine("Eating...");
    }
}

//Derived class, child class, Subclass
class Dog : Animal
{
    //Sealed keyword: Can only be used when overriding a method, prevents further overriding
    public sealed override void MakeSound()
    {
        //base.MakeSound(); //Calling the overridden method
        Console.WriteLine("Bark, Bork!");
    }
}

class Cat : Animal
{
    
    //This Method hides the parent method of the same name instead of overriding it.
    //Method hiding does not support polymorphism. See output main method.
    public new void MakeSound()
    {
        Console.WriteLine("Meoooow.");
    }
}