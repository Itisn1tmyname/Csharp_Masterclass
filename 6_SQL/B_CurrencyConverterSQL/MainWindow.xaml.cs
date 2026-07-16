using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

namespace B_CurrencyConverterSQL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public static SqlConnection SQLconnection { get; } = new(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    
    public MainWindow()
    {
        InitializeComponent();
        ControlContent.Content = new ControlWindow();
        ConverterContent.Content = new ConverterWindow();
    }
    
}