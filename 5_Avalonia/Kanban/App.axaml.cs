using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
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
    
    public static void MessageBox(string msg)
    {
        Window messageBox = new Window();
        messageBox.Title = "Attention";
        messageBox.Width = 300;
        messageBox.Height = 150;
        messageBox.SizeToContent = SizeToContent.WidthAndHeight;
        messageBox.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        messageBox.Content = new StackPanel()
        {
            Margin = new Thickness(20),
            Spacing = 16,
            Children =
            {
                new TextBlock()
                {
                    Text = msg,
                    FontSize = 20
                },
                new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Spacing = 8,
                    Children =
                    {
                        new Button()
                        {
                            Content = "Yes",
                            Command = new RelayCommand(messageBox.Close),
                            Width = 50,
                            Height = 50
                        },
                        new Button()
                        {
                            Content = "No",
                            Command = new RelayCommand(messageBox.Close),
                            Width = 50,
                            Height = 50
                        }
                    }
                }
            }
        };
        
        messageBox.Show();
    }

}