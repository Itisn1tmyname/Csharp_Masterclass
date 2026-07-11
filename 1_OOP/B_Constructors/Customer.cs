namespace B_Constructors;

public class Customer
{
    //Static field
    private static int _nextId = 0;
    // Read-only field can only be initialized once, inside a constructor
    private readonly int _id;
    //Backing field for write-only property
    private string _password;
    
    //Properties
    public string Name { get; set; }
    public string Address { get; set; }
    public string ContactNumber { get; set; }
    
    // Write-Only Property
    public string Password{set => _password = value;}
    
    
    //Custom Constructor
    public Customer(string name, string address = "Unknown", string contactNumber = "None")
    {
        _id = _nextId++;
        Name = name;
        Address = address;
        ContactNumber = contactNumber;
    }

    //Default Constructor
    public Customer()
    {
        _id = _nextId++;
        Name = "New Customer";
        Address = "Unknown";
        ContactNumber = "None";
    }

       
    public void SetDetails(string name, string address, string contactNumber)
    {
        Name = name;
        Address = address;
        ContactNumber = contactNumber;
    }
}