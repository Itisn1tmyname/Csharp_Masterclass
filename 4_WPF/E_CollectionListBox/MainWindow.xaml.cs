using System.Windows;
using E_CollectionListBox.Data;

namespace E_CollectionListBox;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly List<Person> _people =
    [
        new() { Name = "Yannick", Age = 30 },
        new() { Name = "Emily", Age = 19 },
        new() { Name = "Martin", Age = 29 },
        new() { Name = "Alice", Age = 28 },
        new() { Name = "Peter", Age = 25 },
        new() { Name = "Alissa", Age = 32 },
        new() { Name = "Bert", Age = 20 }
    ];
    
    public MainWindow()
    {
        InitializeComponent();
        ListBoxPeople.ItemsSource = _people;
    }

    private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
    {
        // ListBoxPeople.SelectedItem... => SelectionMode="Single"
        var selectedItems = ListBoxPeople.SelectedItems;
        foreach (var item in selectedItems)
        {
            var person = item as Person;
            MessageBox.Show($"Name: {person!.Name}, Age: {person.Age}");
        }
    }
}