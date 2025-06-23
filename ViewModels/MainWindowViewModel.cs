using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Sharpie.Views;
using Sharpie.ViewModels;

namespace Sharpie.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set => this.SetProperty(ref _currentView, value);
    }

 
    public MainWindowViewModel()
    {
        // Start the app by showing the starting screen
        // ShowStartView();

        // var skiaPage = new CustomSkiaPage();
        // CurrentView = skiaPage;
        
        ShowEditor();
    }

    // FOR TESTING ONLY
    public void ShowEditor()
    {
        var editorView = new EditorWindowView();
        editorView.DataContext = new EditorWindowViewModel(this);
        
        CurrentView = editorView;
    }

    public void ShowStartView()
    {
        var startView = new StartWindowView();
        startView.DataContext = new StartWindowViewModel(this);
        
        CurrentView = startView;
    }
}
