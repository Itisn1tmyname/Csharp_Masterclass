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

namespace A_Test;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Button myButton = new Button();
        myButton.Content = "B";
        
        Grid.SetRow(myButton, 3);
        Grid.SetColumn(myButton, 4);
        
        myGrid.Children.Add(myButton); 
        //myGrid exists already because it's the Grid of the XAML doc and I named it this way.
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Hello World!");
    }
}