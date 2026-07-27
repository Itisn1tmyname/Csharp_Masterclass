using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace Kanban;

public partial class MainWindow : Window
{
    
    
    public MainWindow()
    {
        InitializeComponent();
        MainWindowContent.Content = new SelectionScreen(this);
        Width = 800;
    }

    public void ChangeMainWindowContent(Projekt projekt)
    {
        MainWindowContent.Content = new ProjectScreen(this, projekt);
        Width = 1200;
    }
    
    protected override void OnClosing(WindowClosingEventArgs e)
    {
        base.OnClosing(e);
        if (MainWindowContent.Content!.GetType() == typeof(ProjectScreen))
            Save();
    }

    private void Save()
    {
        ProjectScreen screen = (MainWindowContent.Content as ProjectScreen)!;
        screen.Save();
    }
}