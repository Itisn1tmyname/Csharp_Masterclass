using System.Data;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;


namespace A_Zoo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly SqlConnection _sqlConnection;
    
    public MainWindow()
    {
        InitializeComponent();
        _sqlConnection = new SqlConnection(@"Data Source=lenovo-3193-11\MASTERCLASSSQL;Database=ZooDB;Persist Security Info=True;Trusted_Connection=True;Encrypt=True;Trust Server Certificate=True");
        ShowZoos();
        ShowAnimals();
    }

    private void ShowZoos()
    {
        try
        {
            string query = "select * from Zoo";
        
            // SqlAdapter can be imagined like an Interface to make Tables usable by C#-Objects.
            SqlDataAdapter adapter = new SqlDataAdapter(query, _sqlConnection);

            using (adapter)
            {
                DataTable zooTable = new DataTable();
            
                adapter.Fill(zooTable);
            
                //listZoos = ListBox element in MainWindow.xaml
            
                //Which Information of the Table should be shown in the ListBox?
                listZoos.DisplayMemberPath = "Location";
                //Which Value should be delivered, when an item from the ListBox is selected?
                listZoos.SelectedValuePath = "Id";
                //Reference to the Data the ListBox should populate
                listZoos.ItemsSource = zooTable.DefaultView;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
    
    private void ShowAnimals()
    {
        try
        {
            string query = "select * from Animal";
        
            SqlDataAdapter adapter = new SqlDataAdapter(query, _sqlConnection);

            using (adapter)
            {
                DataTable table = new DataTable();
            
                adapter.Fill(table);
            
                //listAnimals = ListBox element in MainWindow.xaml
                listAnimals.DisplayMemberPath = "Name";
                listAnimals.SelectedValuePath = "Id";
                listAnimals.ItemsSource = table.DefaultView;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void ShowAssociatedAnimals()
    {
        try
        {
            string query = $"select * from Animal a inner join ZooAnimal za on a.Id = za.AnimalId where za.ZooId = {listZoos.SelectedValue}";
            
            // string query = $"select * from Animal a inner join ZooAnimal za on a.Id = za.AnimalId where za.ZooId = @ZooId";
            // SqlCommand command = new SqlCommand(query, _sqlConnection);
            // SqlDataAdapter adapter = new  SqlDataAdapter(command);
            
            SqlDataAdapter adapter = new SqlDataAdapter(query, _sqlConnection);

            using (adapter)
            {
                // command.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
                
                DataTable table = new DataTable();
            
                adapter.Fill(table);
            
                //listAnimals = ListBox element in MainWindow.xaml
                listZooAnimals.DisplayMemberPath = "Name";
                listZooAnimals.SelectedValuePath = "Id";
                listZooAnimals.ItemsSource = table.DefaultView;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void ListZoos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ShowAssociatedAnimals();
    }
}