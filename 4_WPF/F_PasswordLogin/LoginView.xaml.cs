using System.Windows;
using System.Windows.Controls;

namespace F_PasswordLogin;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        LoginButton.IsEnabled = false;
    }

    private void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        string enteredPswrd = PasswordBox.Password;
        string pswrd = "Test123";
        if (enteredPswrd == pswrd)
            MessageBox.Show("Password Correct");
        else
            MessageBox.Show("Login Failed");
        PasswordBox.Password = "";
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        LoginButton.IsEnabled = !string.IsNullOrEmpty(PasswordBox.Password.Trim());
    }
}