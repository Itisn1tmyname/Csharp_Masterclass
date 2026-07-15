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
        DeleteZooButton.IsEnabled = false;
        RemoveAnimalFromZooButton.IsEnabled = false;
        AddAnimalToZooButton.IsEnabled = false;
        RenameZooLocationButton.IsEnabled = false;
        AddZooButton.IsEnabled = false;
        RenameAnimalButton.IsEnabled = false;
        DeleteAnimalFromListButton.IsEnabled = false;
        addAnimaltoListButton.IsEnabled = false;
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
            MessageBox.Show($"{e.Message} @ ShowZoos");
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
            MessageBox.Show($"{e.Message} @ ShowAnimals");
        }
    }
    
    private void ShowZooAnimals()
    {
        try
        {
            string query = $"select za.Id, a.Name from Animal a inner join ZooAnimal za on a.Id = za.AnimalId where za.ZooId =" +
                           $" {(listZoos.SelectedValue == null ? -1 : listZoos.SelectedValue)}";
            
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
            MessageBox.Show($"{e.Message} @ ShowZooAnimals");
        }
    }

    private void ListZoos_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listZoos.SelectedValue == null)
        {
            DeleteZooButton.IsEnabled = false;
            RemoveAnimalFromZooButton.IsEnabled = false;
            AddAnimalToZooButton.IsEnabled = false;
            RenameZooLocationButton.IsEnabled = false;
            inputTextBox.Clear();
        }
        else
        {
            DeleteZooButton.IsEnabled = true;
            RemoveAnimalFromZooButton.IsEnabled = listZooAnimals.SelectedValue != null;
            AddAnimalToZooButton.IsEnabled = listAnimals.SelectedValue != null;
            RenameZooLocationButton.IsEnabled = true;
            ShowSelectedZooInTextBox();
        }
        ShowZooAnimals();
    }

    private void ListAnimals_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listAnimals.SelectedValue == null)
        {
            DeleteAnimalFromListButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
            AddAnimalToZooButton.IsEnabled = false;
            inputTextBox.Clear();
        }
        else
        {
            DeleteAnimalFromListButton.IsEnabled = true;
            AddAnimalToZooButton.IsEnabled = listZoos.SelectedValue != null;
            RenameAnimalButton.IsEnabled = true;
            ShowSelectedAnimalInTextBox();
        }
    }

    private void ListZooAnimals_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (listZooAnimals.SelectedValue == null)
        {
            RemoveAnimalFromZooButton.IsEnabled = false;
            inputTextBox.Clear();
        }
        else
        {
            RemoveAnimalFromZooButton.IsEnabled = true;
        }
    }
    
    private void OnZooFocus(object sender, RoutedEventArgs e)
    {
        RenameZooLocationButton.IsEnabled = true;
        RenameAnimalButton.IsEnabled = false;
        ShowSelectedZooInTextBox();
    }
    
    private void OnAnimalFocus(object sender, RoutedEventArgs e)
    {
        RenameZooLocationButton.IsEnabled = false;
        RenameAnimalButton.IsEnabled = true;
        ShowSelectedAnimalInTextBox();
    }
    
    private void OnZooAnimalFocus(object sender, RoutedEventArgs e)
    {
        RenameZooLocationButton.IsEnabled = false;
        RenameAnimalButton.IsEnabled = false;
        inputTextBox.Clear();
    }
    
    private void InputTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(inputTextBox.Text.Trim()))
        {
            AddZooButton.IsEnabled = false;
            addAnimaltoListButton.IsEnabled = false;
        }
        else
        {
            AddZooButton.IsEnabled = true;
            addAnimaltoListButton.IsEnabled = true;
        }
    }

    private void AddZoo_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(inputTextBox.Text.Trim())) return;
        try
        {
            string query = $"insert into Zoo values ('{inputTextBox.Text}')";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ AddZoo_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZoos();
            inputTextBox.Clear();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void AddAnimalToZoo_OnClick(object sender, RoutedEventArgs e)
    {
        if (listAnimals.SelectedValue == null || listZoos.SelectedValue == null) return;
        try
        {
            string query = $"insert into ZooAnimal values ({listZoos.SelectedValue}, {listAnimals.SelectedValue})";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ AddAnimalToZoo_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZooAnimals();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }
    
    private void AddAnimalToAnimalList_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(inputTextBox.Text.Trim())) return;
        try
        {
            string query = $"insert into Animal values ('{inputTextBox.Text}')";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ AddAnimalToAnimalList_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowAnimals();
            inputTextBox.Clear();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void DeleteZoo_OnClick(object sender, RoutedEventArgs e)
    {
        if (listZoos.SelectedValue == null) return;
        try
        {
            string query = $"delete from Zoo where id = {listZoos.SelectedValue}";
            // string query = $"delete from Zoo where id = @ZooId";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            // command.Parameters.AddWithValue("@ZooId", listZoos.SelectedValue);
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ DeleteZoo_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZoos();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void RemoveAnimalFromZoo_OnClick(object sender, RoutedEventArgs e)
    {
        if (listZooAnimals.SelectedValue == null) return;
        try
        {
            string query = $"delete from ZooAnimal where id = {listZooAnimals.SelectedValue}";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ RemoveAnimalFromZoo_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZooAnimals();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }
    
    private void DeleteAnimalFromAnimalList_OnClick(object sender, RoutedEventArgs e)
    {
        if (listAnimals.SelectedValue == null) return;
        try
        {
            string query = $"delete from Animal where id = {listAnimals.SelectedValue}";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ DeleteAnimalFromAnimalList_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowAnimals();
            ShowZooAnimals();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void RenameZooLocation_OnClick(object sender, RoutedEventArgs e)
    {
        if (listZoos.SelectedValue == null) return;
        try
        {
            string query = $"update Zoo Set Location = '{inputTextBox.Text.Trim()}' where Id = {listZoos.SelectedValue}";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ RenameZooLocation_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZoos();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void RenameAnimal_OnClick(object sender, RoutedEventArgs e)
    {
        if (listAnimals.SelectedValue == null) return;
        try
        {
            
            string query = $"update Animal Set Name = '{inputTextBox.Text.Trim()}' where Id = {listAnimals.SelectedValue}";
            SqlCommand command = new SqlCommand(query, _sqlConnection);
            _sqlConnection.Open();
            command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            MessageBox.Show($"{exception.Message} @ RenameAnimal_OnClick");
        }
        finally
        {
            _sqlConnection.Close();
            ShowZooAnimals();
            ShowAnimals();
            RenameZooLocationButton.IsEnabled = false;
            RenameAnimalButton.IsEnabled = false;
        }
    }

    private void ShowSelectedZooInTextBox()
    {
        if (listZoos.SelectedValue == null) return;
        try
        {
            string query = $"select Location from Zoo where Id = {listZoos.SelectedValue}";
            
            SqlDataAdapter adapter = new SqlDataAdapter(query, _sqlConnection);

            using (adapter)
            {
                DataTable zooTable = new DataTable();
                adapter.Fill(zooTable);

                inputTextBox.Text = zooTable.Rows[0]["Location"].ToString()!;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"{e.Message} @ ShowSelectedZooInTextBox");
        }
    }

    private void ShowSelectedAnimalInTextBox()
    {
        if (listAnimals.SelectedValue == null) return;
        try
        {
            string query = $"select Name from Animal where Id = {listAnimals.SelectedValue}";
            
            SqlDataAdapter adapter = new SqlDataAdapter(query, _sqlConnection);

            using (adapter)
            {
                DataTable animalTable = new DataTable();
                adapter.Fill(animalTable);

                inputTextBox.Text = animalTable.Rows[0]["Name"].ToString()!;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show($"{e.Message} @ ShowSelectedAnimalInTextBox");
        }
    }
}