using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace B_CurrencyConverterSQL;

public partial class ConverterWindow : UserControl
{
    private byte _inputValidationCounter = (byte)0;
    
    public ConverterWindow()
    {
        InitializeComponent();
        BindData();
    }
    
    private void BindData()
    {
        DataTable dataTableCurrency = new  DataTable();
        dataTableCurrency.Columns.Add("Text", typeof(string));
        dataTableCurrency.Columns.Add("Value", typeof(double));

        dataTableCurrency.Rows.Add("    --SELECT--", 0);
        dataTableCurrency.Rows.Add("EUR (€)", 1);
        dataTableCurrency.Rows.Add("USD ($)", 1.143);
        dataTableCurrency.Rows.Add("JPY (¥)", 185.02);
        dataTableCurrency.Rows.Add("CZK (Kč)", 24.259);
        dataTableCurrency.Rows.Add("GBP (£)", 0.85155);
        dataTableCurrency.Rows.Add("PLN (zł)", 4.3475);
        dataTableCurrency.Rows.Add("CNY (¥uan)", 7.7433);
        
        FromComboBox.ItemsSource = dataTableCurrency.DefaultView;
        FromComboBox.DisplayMemberPath = "Text";
        FromComboBox.SelectedValuePath = "Value";
        FromComboBox.SelectedIndex = 1;
        
        ToComboBox.ItemsSource = dataTableCurrency.DefaultView;
        ToComboBox.DisplayMemberPath = "Text";
        ToComboBox.SelectedValuePath = "Value";
        ToComboBox.SelectedIndex = 2;
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
            Console.WriteLine(_inputValidationCounter);
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
}