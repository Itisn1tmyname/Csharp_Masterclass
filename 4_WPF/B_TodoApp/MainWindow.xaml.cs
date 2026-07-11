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

namespace B_TodoApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddTodoButton_Click(object sender, RoutedEventArgs e)
    {
        string todoText = TodoInput.Text;
        if (string.IsNullOrEmpty(todoText.Trim()))
            return;
        
        TextBlock todoItem = new TextBlock()
        {
            Text = todoText.Trim(),
            Margin = new Thickness(1),
            Foreground = new SolidColorBrush(Colors.DarkBlue),
            Background = new SolidColorBrush(Colors.LightBlue),
            FontSize = 14,
            FontWeight = FontWeights.Bold,
            FontFamily = new FontFamily("Consolas"),
            FontStretch = FontStretches.Normal,
            FontStyle = FontStyles.Normal,
            Padding = new Thickness(9),
            TextAlignment = TextAlignment.Left,
            TextWrapping = TextWrapping.Wrap
        };
        
        TodoList.Children.Add(todoItem);
        TodoInput.Clear();
        CreateTodoButton.IsEnabled = false;
    }

    private void TodoInput_OnKeyUp(object sender, KeyEventArgs e)
    {
        if (string.IsNullOrEmpty(TodoInput.Text.Trim()))
            CreateTodoButton.IsEnabled = false;
        else if (e.Key == Key.Enter)
            AddTodoButton_Click(sender, e);
        else
            CreateTodoButton.IsEnabled = true;
    }
}