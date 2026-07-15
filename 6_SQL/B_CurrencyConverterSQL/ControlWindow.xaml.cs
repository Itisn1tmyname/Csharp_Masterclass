using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace B_CurrencyConverterSQL;

public partial class ControlWindow : UserControl
{
    public ControlWindow()
    {
        InitializeComponent();
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        Save();
    }
    
    private void Save()
    {
        
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        // throw new NotImplementedException();
    }

    private void TextBoxAmount_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Space)
            e.Handled = true;
        else if (e.Key == Key.Enter)
            textBoxName.Focus();
    }

    private void TextBoxAmount_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        MessageBox.Show("Not Implemented");
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
        MessageBox.Show("Not Implemented");
    }

    private void DataGridCurrency_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        MessageBox.Show("Not Implemented");
    }
}