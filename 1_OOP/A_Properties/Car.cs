namespace A_Properties;

internal class Car
{
    //Member variables / Fields, Backing fields for the properties.
    private string _brand = "";

    //Properties
    //Property without backing field
    public string Model { get;  set;}
    public bool IsLuxury { get; set; }
    
    //Properties used for calculations
    public double Length { get; set; }
    public double  Width { get; set; }
    public double Height { get; set; }
    
    //Calculated Properties
    public double Area {get => Width * Height;}                 // ==> without set!
    public double Volume {get => Length * Width *  Height;}     // Properties without set are READ-ONLY
    
    
    //Property with backing field
    public string Brand
    {
        get
        {
            if (IsLuxury)
            {
                return _brand + " - Luxury Edition";
            }
            else return _brand;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("You entered NOTHING!");
                _brand = "DEFAULT";
            }
            else
                _brand = value;
        }
    }


    //Constructor
    public Car(string brand, string model, bool isLuxury)
    {
        Brand = brand;
        Model = model;
        IsLuxury = isLuxury;
        Console.WriteLine($"A{("aeiouAEIOU".Contains(brand.Last()) ? $"n {brand}": $" {brand}")} of model {model} has been created.");
    }
}