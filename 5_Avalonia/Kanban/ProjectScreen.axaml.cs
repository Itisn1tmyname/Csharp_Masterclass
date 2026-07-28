using System;
using System.IO;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Input;

namespace Kanban;

public partial class ProjectScreen : UserControl
{
    private Projekt Projekt { get; }
    private MainWindow _window;
    private (byte, string?) _selectedItem;
    
    public ProjectScreen(MainWindow window, Projekt projekt)
    {
        _window = window;
        Projekt = projekt;
        Write();
        _window.MinWidth = _window.Width = 1350;
        _window.SizeToContent = SizeToContent.Height;
        Binding();
    }

    private void Binding()
    {
        InitializeComponent();
        _window.AdjustWindowPositionMid();
        Width = _window.Width;
        GridLength width = new((_window.Width - 150) / 3);
        ProjectGrid.ColumnDefinitions[0].Width = width;
        ProjectGrid.ColumnDefinitions[2].Width = width;
        ProjectGrid.ColumnDefinitions[4].Width = width;
        ProjektLabel.Content = $"Projekt: {Projekt.Name}";
        ToDoListBox.ItemsSource = Projekt.ToDo;
        DoingListBox.ItemsSource = Projekt.Doing;
        DoneListBox.ItemsSource = Projekt.Done;
    }

    public void Save()
    {
        App.MessageBoxJaNein("Projekt speichern?", new RelayCommand(Write));
    }

    private void Write()
    {
        try
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Projekte");
            File.WriteAllText($"{Projekt.Name}.json", Projekt.ToJson());
        }
        catch (Exception e)
        {
            App.MessageBoxOK($"{e.Message}\n\n{e.StackTrace}");
        }
    }

    #region ButtonClicks
    
    private void DoToDoingButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (ToDoListBox.SelectedValue == null) return;
        Projekt.DoToDoing(ToDoListBox.SelectedValue.ToString()!);
        Binding();
    }
    
    private void DoingToDoButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DoingListBox.SelectedValue == null) return;
        Projekt.DoingToDo(DoingListBox.SelectedValue.ToString()!);
        Binding();
    }
    
    private void DoingToDoneButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DoingListBox.SelectedValue == null) return;
        Projekt.DoingToDone(DoingListBox.SelectedValue.ToString()!);
        Binding();
    }
    
    private void DoneToDoingButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (DoneListBox.SelectedValue == null) return;
        Projekt.DoneToDoing(DoneListBox.SelectedValue.ToString()!);
        Binding();
    }

    private void HinzufuegenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Projekt.AddItem(ItemTextBox.Text!.Trim()); //TODO: disable Button if input invalid
        Binding();
    }

    private void SpeichernButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Save(); //TODO: Save last save and skip asking if window closes
    }
    
    private void ProjektVerlassenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        Save();
        _window.ChangeMainWindowContent();
    }
    
    private void EintragLoeschenButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (_selectedItem.Item2 == null) return;
        string liste = _selectedItem.Item1 == 1 ? "ToDo" : _selectedItem.Item1 == 2 ? "Doing": "Done";
        App.MessageBoxJaNein($"Wollen Sie den ausgewählten Eintrag aus der {liste}-Liste löschen?", new RelayCommand(Loeschen));
        void Loeschen()
        {
            switch (_selectedItem.Item1)
            {
                case 1: 
                    Projekt.ToDo.Remove(_selectedItem.Item2!);
                    break;
                case 2:
                    Projekt.Doing.Remove(_selectedItem.Item2!);
                    break;
                case 3:
                    Projekt.Done.Remove(_selectedItem.Item2!);
                    break;
            }
            Binding();
        }
    }
    
    #endregion

    #region ListBox Selection Changed Selected Value

    private void ToDoListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        object? value = ToDoListBox.SelectedValue;
        _selectedItem = (1, value as string);
    }
    
    private void DoingListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        object? value = DoingListBox.SelectedValue;
        _selectedItem = (2, value as string);
    }
    
    private void DoneListBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        object? value = DoneListBox.SelectedValue;
        _selectedItem = (3 ,value as string);
    }

    #endregion
}