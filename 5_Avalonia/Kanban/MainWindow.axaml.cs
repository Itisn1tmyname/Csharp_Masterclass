using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.Input;

namespace Kanban;

public partial class MainWindow : Window
{
    private bool _hasProjects;
    
    public MainWindow()
    {
        InitializeComponent();
        StartButton.IsEnabled = false;
        CheckProjects();
        LoadProjects();
    }

    private void CheckProjects()
    {
        try
        {
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
            StartButton.IsEnabled = false;
        else
            StartButton.IsEnabled = true;
    }
    
    #region ButtonClicks
    
    private void StartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MessageBox("Start Button not yet implemented");
    }

    private void NeuesProjektButton_OnClick(object? sender, RoutedEventArgs e)
    {
        App.MessageBox("Button ist noch nicht fertig...");
    }
    
    #endregion

    
    
}