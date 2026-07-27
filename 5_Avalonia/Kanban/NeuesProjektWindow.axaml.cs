using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Kanban;

public partial class NeuesProjektWindow : Window
{
    private readonly MainWindow _window;
    public NeuesProjektWindow(MainWindow window)
    {
        _window = window;
        InitializeComponent();
        ProjektErstellenButton.IsEnabled = false;
    }

    private void ProjektErstellenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Projekt projekt = new(ProjektNameTextBox.Text!.Trim());
        App.MessageBoxOK($"Projekt \"{projekt.Name}\" erfolgreich erstellt!");
        //TODO: Check if chosen name already exists among Projects, exclude invalid keystrokes in input
        _window.ChangeMainWindowContent(projekt);
        Close();
    }

    private void ProjektNameTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ProjektErstellenButton.IsEnabled = !string.IsNullOrEmpty(ProjektNameTextBox.Text!.Trim());
    }
}