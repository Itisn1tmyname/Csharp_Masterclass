using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Data.SqlClient;

namespace B_CurrencyConverterSQL;

public partial class ConverterWindow : UserControl
{
    private byte _inputValidationCounter = (byte)0;
    private readonly SqlConnection _sqlConnection = MainWindow.SQLconnection;
    
    public ConverterWindow()
    {
        InitializeComponent();
        BindData();
    }
    
    public void BindData()
    {
        try
        {
            SqlDataAdapter adapter = new("select * from Currency_Control", _sqlConnection);

            using (adapter)
            {
                DataTable dataTableCurrency = new  DataTable();
                adapter.Fill(dataTableCurrency);

                DataRow newRow = dataTableCurrency.NewRow();
                newRow["Id"] = 0;
                newRow["Name"] = "    --SELECT--";
                newRow["Value"] = 0;
                
                dataTableCurrency.Rows.InsertAt(newRow, 0);
            
                FromComboBox.ItemsSource = dataTableCurrency.DefaultView;
                FromComboBox.DisplayMemberPath = "Name";
                FromComboBox.SelectedValuePath = "Value";
                FromComboBox.SelectedIndex = 1;
        
                ToComboBox.ItemsSource = dataTableCurrency.DefaultView;
                ToComboBox.DisplayMemberPath = "Name";
                ToComboBox.SelectedValuePath = "Value";
                ToComboBox.SelectedIndex = 2;
            
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error in ConverterWindow.BindData()", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CurrencyTxtBx_OnTextInput(object sender, KeyEventArgs e)
    {
        //Checking input with regular Expressions. Currently accepts spacebar for some godforsaken reason.
        Regex regex = new Regex("[0-9.]+");
        if (e.Key.Equals(Key.Enter))
        {
            ConvertValues();
            CurrencyTxtBx.Clear();
            _inputValidationCounter = 0;
        }
        else if (e.Key.Equals(Key.Space))
        {
            e.Handled = true;
        }
        else if (e.Key.Equals(Key.OemPeriod))
        {
            if (_inputValidationCounter > 0)
                e.Handled = true;
            else
                _inputValidationCounter++;
        }
        else if (e.Key.Equals(Key.Back))
        {
            if (_inputValidationCounter > 2)
                _inputValidationCounter = 1;
            else if (_inputValidationCounter > 0)
                _inputValidationCounter--;
        }
        else if (_inputValidationCounter > 0 && _inputValidationCounter++ > 2)
            e.Handled = true;
        else 
            e.Handled = !regex.IsMatch(e.Key.ToString());
    }

    private void ConvertButton_OnClick(object sender, RoutedEventArgs e)
    {
        ConvertValues();
        CurrencyTxtBx.Clear();
        _inputValidationCounter = 0;
    }

    private void ClearButton_OnClick(object sender, RoutedEventArgs e)
    {
        CurrencyTxtBx.Text = string.Empty;
        if (FromComboBox.Items.Count > 0)
            FromComboBox.SelectedIndex = 0;
        if (ToComboBox.Items.Count > 0)
            ToComboBox.SelectedIndex = 0;
        CurrencyResultLabel.Content = string.Empty;
        CurrencyTxtBx.Focus();
        _inputValidationCounter = 0;
    }

    private void ConvertValues()
    {
        double convertedValue;

        if (string.IsNullOrEmpty(CurrencyTxtBx.Text.Trim()))
        {
            MessageBox.Show("Please enter an amount of money you want to convert.",
                "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            CurrencyTxtBx.Focus();
            return;
        }
        if (FromComboBox.SelectedValue == null || FromComboBox.SelectedIndex == 0)
        {
            MessageBox.Show("Please select a Currency to convert from.",
                "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            CurrencyTxtBx.Focus();
            return;
        }
        if (ToComboBox.SelectedValue == null || ToComboBox.SelectedIndex == 0)
        {
            MessageBox.Show("Please select a Currency to convert to.",
                "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            CurrencyTxtBx.Focus();
            return;
        }

        if (FromComboBox.SelectedIndex == ToComboBox.SelectedIndex)
        {
            convertedValue = double.Parse(CurrencyTxtBx.Text, CultureInfo.InvariantCulture);
            CurrencyResultLabel.Content =
                $"{convertedValue:N2} {FromComboBox.Text} = {convertedValue:N2} {ToComboBox.Text}";
        }
        else
        {
            double inputValue = double.Parse(CurrencyTxtBx.Text, CultureInfo.InvariantCulture);
            convertedValue = double.Parse(ToComboBox.SelectedValue.ToString()!) * inputValue /
                             double.Parse(FromComboBox.SelectedValue.ToString()!);
            CurrencyResultLabel.Content = $"{inputValue:N2} {FromComboBox.Text} = {convertedValue:N2} {ToComboBox.Text}";
        }
    }

    private void ConverterWindow_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        BindData();
    }
}