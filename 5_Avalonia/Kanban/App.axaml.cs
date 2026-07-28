using System;
using System.Threading;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Rendering;
using CommunityToolkit.Mvvm.Input;

namespace Kanban;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    public static void MessageBoxJaNein(string msg, ICommand? command = null)
    {
        Window messageBox = new Window
        {
            Title = "Achtung",
            MinWidth = 200,
            MaxWidth = 800,
            MinHeight = 130,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageBox.Content = new StackPanel()
        {
            Margin = new Thickness(20),
            Spacing = 16,
            Children =
            {
                new TextBlock()
                {
                    Text = msg,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 20
                },
                new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Spacing = 8,
                    Children =
                    {
                        new Button()
                        {
                            Content = "Ja",
                            Command = new RelayCommand(() =>
                            {
                                messageBox.Close();
                                command?.Execute(null);
                            }),
                            Width = 50,
                            Height = 50
                        },
                        new Button()
                        {
                            Content = "Nein",
                            Command = new RelayCommand(messageBox.Close),
                            Width = 50,
                            Height = 50
                        }
                    }
                }
            }
        };
        messageBox.SizeToContent = SizeToContent.WidthAndHeight;
        messageBox.Show();
    }

    // ReSharper disable once InconsistentNaming
    public static void MessageBoxOK(string msg)
    {
        Window messageBox = new Window
        {
            Title = "Attention",
            MinWidth = 200,
            MaxWidth = 800,
            MinHeight = 130,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageBox.Content = new StackPanel()
        {
            Margin = new Thickness(20),
            Spacing = 16,
            Children =
            {
                new TextBlock()
                {
                    Text = msg,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 20
                },
                new Button()
                {
                    Content = "OK",
                    Command = new RelayCommand(messageBox.Close),
                    Width = 50,
                    Height = 50
                }
            }
        };
        messageBox.SizeToContent = SizeToContent.WidthAndHeight;
        messageBox.Show();
    }

}