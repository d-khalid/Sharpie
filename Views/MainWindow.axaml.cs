using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace Sharpie.Views;

public partial class MainWindow : Window
{
    
    // Testing,

    private Color selectedColor = Colors.White;

    private Polyline? currentLine = null;
    
    public MainWindow()
    {
        InitializeComponent();

        // // Just a test
        // Rectangle r = new Rectangle();
        // r.Width = 20;
        // r.Height = 20;
        // r.Fill = Brushes.Blue;
        // Canvas.SetLeft(r, 10);
        // Canvas.SetTop(r, 10);
        
        // ColorPallete.Children.Add(r);

        // ColorPicker cp = new();
        // cp.Background = new SolidColorBrush(Colors.White);
        // cp.MinHeight = 300;
        // cp.Palette = new FlatColorPalette();
        //
        //
        
        ColorView cp = new();
        cp.Background = new SolidColorBrush(Colors.White);
        cp.MinHeight = 300;
        cp.Palette = new FlatColorPalette();


        cp.IsColorPaletteVisible = true;
        cp.IsColorModelVisible = false;

        
        cp.IsAccentColorsVisible = false;
        cp.IsColorComponentsVisible = false;
        cp.IsColorSpectrumVisible = false;
        cp.IsColorSpectrumSliderVisible = false;
        cp.ColorChanged += (s, e) =>
        {
            selectedColor = e.NewColor;
        };


        SideBar.Children.Add(cp);
        
        // Making a simple brush, hardcoding style
        MainCanvas.PointerPressed += (s, e) =>
        {
            currentLine = new Polyline();
            currentLine.StrokeThickness = 10;
            currentLine.Stroke = new SolidColorBrush(selectedColor);
            
            currentLine.Points.Add(e.GetPosition(MainCanvas));
            
            MainCanvas.Children.Add(currentLine);
        };
        
        MainCanvas.PointerMoved += (s, e) =>
        {
            if(currentLine == null) return;
            
            currentLine.Points.Add(e.GetPosition(MainCanvas));

        };
        
        MainCanvas.PointerReleased += (s, e) =>
        {
            currentLine.Points.Add(e.GetPosition(MainCanvas));
            currentLine = null;

        };

    }

   


}
