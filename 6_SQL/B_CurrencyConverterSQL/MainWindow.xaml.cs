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
   
    public SqlConnection SQLconnection { get; }
    
    public MainWindow()
    {
        InitializeComponent();
        SQLconnection =
            new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Programme\Csharp_Masterclass\6_SQL\B_CurrencyConverterSQL\Data\CurrencyConverterDB.mdf;Integrated Security=True");
        ControlContent.Content = new ControlWindow();
        ConverterContent.Content = new ConverterWindow();
    }
}