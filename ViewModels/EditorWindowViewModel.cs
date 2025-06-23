using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Sharpie.ViewModels;

public class EditorWindowViewModel : ViewModelBase
{
    public ICommand NewCommand { get; }
    public ICommand OpenCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand AboutCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand PasteCommand { get; }
    public ICommand CutCommand { get; }
    public ICommand DeleteCommand { get; }
    
    public ICommand UndoCommand { get; }
    public ICommand RedoCommand { get; }
    
    public ICommand CloseFileCommand { get; }

    // Has a string parameter indicating the tool type
    public ICommand SelectToolCommand { get; }

    private string? _selectedTool = null;
    public string? SelectedTool { 
        get => _selectedTool; 
        set => SetProperty(ref _selectedTool, value); 
    }

    private string _lastAction = string.Empty;

    public string LastAction
    {
        get => _lastAction;
        set => SetProperty(ref _lastAction, value);
    }

    private string _cursorPosition = "0,0";
    
    

    public string CursorPosition
    {
        get => _cursorPosition;
        set => SetProperty(ref _cursorPosition, value);
    }

    public readonly MainWindowViewModel _mainVm;

    public EditorWindowViewModel(MainWindowViewModel mainVm)
    {
        _mainVm = mainVm;
        // Commands
        SelectToolCommand = new RelayCommand<string>(SelectTool);
        CloseFileCommand = new RelayCommand(CloseFile);
    }

    public void SelectTool(string? toolType)
    {
        SelectedTool = toolType;
    }

    public void CloseFile()
    {
        _mainVm.ShowStartView();
    }
}