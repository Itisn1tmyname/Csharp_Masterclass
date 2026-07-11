using System.Windows;
using System.Windows.Controls;

namespace C_ContentControl;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window window = Window.GetWindow(this)!;
        window.Content = new InvoiceView();
    }
}