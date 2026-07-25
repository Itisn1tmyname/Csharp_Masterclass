using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Kanban;

public partial class MainWindow : Window
{
    private readonly bool _hasProjects;
    private readonly string _path;
    
    public MainWindow()
    {
        InitializeComponent();
        StartButton.IsEnabled = false;
        CheckProjects();
        LoadProjects();
    }

    private void CheckProjects()
    {
        
    }
    
    private void LoadProjects()
    {
        
    }
    
    
    private void ProjektBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ProjektBox.SelectedValue == null)
            StartButton.IsEnabled = false;
        else
            StartButton.IsEnabled = true;
    }
    
    #region ButtonClicks
    
    private void StartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void NeuesProjektButton_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
    
    #endregion

}