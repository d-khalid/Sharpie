using Avalonia.Controls;
using Sharpie.ViewModels;

namespace Sharpie.Views;

public partial class FileCreationWindow : Window
{
    public FileCreationWindow()
    {
        InitializeComponent();
        DataContext = new FileCreationViewModel();
    }
}