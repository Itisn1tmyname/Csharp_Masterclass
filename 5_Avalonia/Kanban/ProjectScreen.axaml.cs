using System;
using System.IO;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;

namespace Kanban;

public partial class ProjectScreen : UserControl
{
    private Projekt Projekt { get; }
    private MainWindow _window;
    
    public ProjectScreen(MainWindow window, Projekt projekt)
    {
        _window = window;
        Projekt = projekt;
        Binding();
    }

    private void Binding()
    {
        InitializeComponent();
        _window.SizeToContent = SizeToContent.WidthAndHeight;
        ProjektLabel.Content = $"Projekt: {Projekt.Name}";
        ToDoListBox.ItemsSource = Projekt.ToDo;
        DoingListBox.ItemsSource = Projekt.Doing;
        DoneListBox.ItemsSource = Projekt.Done;
    }

    public void Save()
    {
        App.MessageBoxJaNein("Projekt speichern?", new RelayCommand(() =>
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Projekte");
            File.WriteAllText($"{Projekt.Name}.json", Projekt.ToJson());
        }));
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
    
    #endregion
}