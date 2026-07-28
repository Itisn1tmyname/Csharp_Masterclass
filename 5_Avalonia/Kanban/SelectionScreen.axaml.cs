using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Kanban;

public partial class SelectionScreen : UserControl
{
    private bool _hasProjects;
    private readonly MainWindow _window;
    
    public SelectionScreen(MainWindow window)
    {
        _window = window;
        _window.Width = _window.MinWidth = 800;
        InitializeComponent();
        CheckProjects();
        LoadProjects();
    }
    
    private void CheckProjects()
    {
        StartButton.IsEnabled = false;
        ProjektLoeschenButton.IsEnabled = false;
        try
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            Directory.CreateDirectory("Projekte");
            Directory.SetCurrentDirectory("Projekte");

            _hasProjects = 
                Directory
                    .EnumerateFiles(Directory.GetCurrentDirectory())
                    .Any();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _hasProjects = false;
        }
    }
    
    private void LoadProjects()
    {
        if (!_hasProjects) return;

        IEnumerable<string> files =
            from file in Directory.EnumerateFiles(Directory.GetCurrentDirectory())
            select Regex.Replace(new DirectoryInfo(file).Name, @"\..*", "");

        ProjektBox.ItemsSource = files;
    }


    private void ProjektBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (ProjektBox.SelectedValue == null)
        {
            StartButton.IsEnabled = false;
            ProjektLoeschenButton.IsEnabled = false;
        }
        else
        {
            StartButton.IsEnabled = true;
            ProjektLoeschenButton.IsEnabled = true;
        }
    }

    #region ButtonClicks
    
    private void StartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string name = (string)ProjektBox.SelectedValue!;
            string json = File.ReadAllText($"{name}.json");
            Projekt? projekt = JsonSerializer.Deserialize<Projekt>(json);
            _window.ChangeMainWindowContent(projekt!);

        }
        catch (Exception exception)
        {
            App.MessageBoxOK($"{exception.Message}\n\n{exception.StackTrace}");
        }
    }

    private void NeuesProjektButton_OnClick(object? sender, RoutedEventArgs e)
    {
        NeuesProjektWindow win = new(_window);
        win.Show();
    }
    
    #endregion

    private void ProjektLoeschenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string name = (string)ProjektBox.SelectedValue!;
            File.Delete($"{name}.json");
            CheckProjects();
            LoadProjects();
        }
        catch (Exception exception)
        {
            App.MessageBoxOK($"{exception.Message}\n\n{exception.StackTrace}");
        }
    }
}