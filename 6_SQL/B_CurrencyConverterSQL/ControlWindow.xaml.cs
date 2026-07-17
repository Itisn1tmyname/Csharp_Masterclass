using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Data.SqlClient;

namespace B_CurrencyConverterSQL;

public partial class ControlWindow
{
    private readonly SqlConnection _sqlConnection = MainWindow.SQLconnection;
    private int _id;
    private bool _hasDecimal;
    
    public ControlWindow()
    {
        InitializeComponent();
        GetData();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        Save();
    }
    
    private void Save()
    {
        if (string.IsNullOrEmpty(TextBoxAmount.Text.Trim()))
        {
            MessageBox.Show("Please enter the current exchange course.", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }
        if(string.IsNullOrEmpty(TextBoxName.Text.Trim()))
        {
            MessageBox.Show("Please enter a name for the currency!", "Information", MessageBoxButton.OK,
                MessageBoxImage.Information);
            return;
        }

        try
        {
            _sqlConnection.Open();
            if (_id > 0)
            {
                if (MessageBox.Show("Are you sure you want to update?", "Attention", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string query =
                        $"update Currency_Control set Value = {TextBoxAmount.Text}, Name = '{TextBoxName.Text}'" +
                        $"where Id = {_id}";
                    SqlCommand command = new SqlCommand(query, _sqlConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data updated successfully!", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to save?", "Attention", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string query = $"insert into Currency_Control(Name, Value) values('{TextBoxName.Text}', {TextBoxAmount.Text})";
                    SqlCommand command = new SqlCommand(query, _sqlConnection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Data saved successfully!", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error in ControlWindow.Save()", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
        finally
        {
            _sqlConnection.Close();
            Clear();
        }
    }

    private void Clear()
    {
        try
        {
            TextBoxName.Clear();
            TextBoxAmount.Clear();
            SaveButton.Content = "Save";
            GetData();
            _id = 0;
            TextBoxAmount.Focus();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error in ControlWindow.Clear()", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GetData()
    {
        try
        {
            _sqlConnection.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("select * from Currency_Control", _sqlConnection);
            sda.Fill(dt);
            DataGridCurrency.ItemsSource = dt.DefaultView;
            DataGridCurrency.SelectedValuePath = "Id";
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error: ControlWindow.GetData()", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            _sqlConnection.Close();
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    private void TextBoxAmount_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
            e.Handled = true;
        else if (e.Key == Key.OemPeriod)
        {
            if (_hasDecimal) e.Handled = true;
            else _hasDecimal = true;
        }
        else if (e.Key == Key.Back)
        {
            Regex regex = new Regex(@"\d+\.");
            if (regex.IsMatch(TextBoxAmount.Text))
                _hasDecimal = false;
        }
        else if (e.Key == Key.Enter)
            TextBoxName.Focus();
    }

    private void TextBoxAmount_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex(@"[\d.]");
        e.Handled = !regex.IsMatch(e.Text) || TextBoxAmount.Text.Length == 15;
    }
    
    private void TextBoxName_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
            e.Handled = true;
        else if (e.Key == Key.Enter)
            Save();
    }

    private void TextBoxName_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex(@"[\w.-_]");
        e.Handled = !regex.IsMatch(e.Text) || TextBoxName.Text.Length == 15;
    }

    private void DataGridCurrency_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        try
        {
            DataGrid grid = (sender as DataGrid)!;
            DataRowView? selectedRow = grid.CurrentItem as DataRowView;
        
            if (selectedRow == null || grid.Items.Count == 0 || grid.SelectedCells.Count == 0)
                return;
            
            
            
            _id = Int32.Parse(selectedRow["Id"].ToString()!);
            if (DataGridCurrency.SelectedCells[0].Column == Edit)
            {
                TextBoxAmount.Text = selectedRow["Value"].ToString()!.Replace(',', '.');
                TextBoxName.Text = selectedRow["Name"].ToString()!;
                SaveButton.Content = "Update";
            }
            else if (DataGridCurrency.SelectedCells[0].Column == Delete)
            {
                if (MessageBox.Show("Are you sure you want to delete this entry?", "Attention", MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _sqlConnection.Open();
                    string query = $"delete from Currency_Control where Id = {_id}";
                    SqlCommand command = new SqlCommand(query, _sqlConnection);
                    command.ExecuteNonQuery();
                    _sqlConnection.Close();
                    GetData();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error in ControlWindow.DataGridCurrency_OnSelectedCellsChanged()",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            _sqlConnection.Close();
        }
    }
}