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
        ChangeMainWindowContent();
    }

    public void ChangeMainWindowContent(Projekt projekt)
    {
        MainWindowContent.Content = new ProjectScreen(this, projekt);
        AdjustWindowPositionMid();
    }

    public void ChangeMainWindowContent()
    {
        MainWindowContent.Content = new SelectionScreen(this);
        AdjustWindowPositionMid();
    }

    public void AdjustWindowPositionMid()
    {
        int x = (int)(Screens.Primary!.Bounds.Width  - Width)  / 2,
            y = (int)(Screens.Primary!.Bounds.Height - Height) / 2;
        Position = new PixelPoint(x, y);
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