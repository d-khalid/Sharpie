using System.Net;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Sharpie.ViewModels;

public class FileCreationViewModel : ViewModelBase
{
    private decimal? _canvasWidth = 1600;
    private decimal? _canvasHeight = 900;

    // TODO: THERE IS PROBABLY A CLEANER WAY TO DO THIS
    public decimal? CanvasWidth
    {
        get => _canvasWidth;
        set
        {
            SetProperty(ref _canvasWidth, value);
            FileCreationSummary = $"This image has a width of {(int)CanvasWidth} and height of {(int)CanvasHeight}.";
        }
    }
    
    public decimal? CanvasHeight
    {
        get => _canvasHeight;
        set
        {
            SetProperty(ref _canvasHeight, value);
            FileCreationSummary = $"This image has a width of {(int)CanvasWidth} and height of {(int)CanvasHeight}.";
        }
    }

    private string _fileCreationSummary = $"This image has a width of 1600 and height of 900.";
    public string FileCreationSummary
    {
        get => _fileCreationSummary;
        set => SetProperty(ref _fileCreationSummary, value);
    }
    public ICommand CreateFileCommand { get; }
    public ICommand CancelCommand { get; }
    
    
    public FileCreationViewModel()
    {
        // Commands
        CreateFileCommand = new RelayCommand(CreateFile);
        CancelCommand = new RelayCommand(Cancel);
    }

    // TODO: MAKE THESE WORK

    public void CreateFile()
    {
        
    }

    public void Cancel()
    {
    }


}