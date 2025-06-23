using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using Sharpie.Views;

namespace Sharpie.ViewModels;

public class StartWindowViewModel : ViewModelBase
{
    public ICommand OpenFileCommand { get; }
    
    public ICommand NewFileCommand { get; }

    // Keep the parent's reference for changing the view
    private readonly MainWindowViewModel _mainVm;
    
    public StartWindowViewModel(MainWindowViewModel mainVm)
    {
        _mainVm = mainVm;
        OpenFileCommand = new RelayCommand(OpenFile);
        NewFileCommand = new RelayCommand(NewFile);
    }

    public void OpenFile()
    {
        Console.WriteLine("Button pressed!");

        
        _mainVm.ShowEditor();

    }

    // Creates a child window that prompts the user to enter info to make a file
    // Like width, height, etc
    public void NewFile()
    {
        Console.WriteLine("Button pressed!");
        
        var fcWindow = new FileCreationWindow();

        // Center it relative to main window
        fcWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        // Get reference to main window
        var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)
            ?.MainWindow;

        fcWindow.ShowDialog(mainWindow);
        
        // When the dialog is closed, go to the editor
        // TODO: THIS IS TEMPORARY
        fcWindow.Closed += (s, e) => _mainVm.ShowEditor();
        
    }
}
