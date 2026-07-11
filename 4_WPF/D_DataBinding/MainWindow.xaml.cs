using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using D_DataBinding.Data;

namespace D_DataBinding;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    Person person = new Person() { Name = "Jannick", Age = 30 };
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = person;
    }

    private void InfoButton_OnClick(object sender, RoutedEventArgs e)
    {
        string personData = $"{person.Name} is {person.Age} years old.";
        MessageBox.Show(personData);
    }
}