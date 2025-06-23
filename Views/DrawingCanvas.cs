using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;

namespace Sharpie.Views;

public class CustomSkiaPage : Control
{
    private readonly GlyphRun _noSkia;
    public CustomSkiaPage()
    {
        ClipToBounds = true;
        var text = "Current rendering API is not Skia";
        var glyphs = text.Select(ch => Typeface.Default.GlyphTypeface.GetGlyph(ch)).ToArray();
        _noSkia = new GlyphRun(Typeface.Default.GlyphTypeface, 12, text.AsMemory(), glyphs);
    }
    class CustomDrawOp : ICustomDrawOperation
    {
        private readonly IImmutableGlyphRunReference _noSkia;
        private List<SKPoint> _brushPoints;

        public CustomDrawOp(Rect bounds, GlyphRun noSkia, List<SKPoint> brushPoints)
        {
            _brushPoints = brushPoints;
            _noSkia = noSkia.TryCreateImmutableGlyphRunReference();
            Bounds = bounds;
        }
            
        public void Dispose()
        {
            // No-op
        }

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) as ISkiaSharpApiLeaseFeature;
            
            // If null then skia can't be used
            if (leaseFeature == null)
            {
                context.DrawGlyphRun(Brushes.Red, _noSkia);
                
            }
            else
            {
                using var lease = leaseFeature.Lease();

                SKCanvas canvas = lease.SkCanvas;
                canvas.Save();
                Console.WriteLine(canvas.LocalClipBounds);
                
                canvas.Clear(SKColors.AntiqueWhite);
                
                SKPaint paint = new SKPaint();
                // paint.PathEffect = SKPathEffect.CreateDiscrete(10.0f, 10.0f);
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 4;
                paint.Color = SKColors.Crimson;
                paint.IsAntialias = true;
                paint.StrokeCap = SKStrokeCap.Round;
                
                
                SKPath path = new SKPath();

                foreach (var point in _brushPoints)
                {
                    path.LineTo(point);
                }
                
                canvas.DrawPath(path, paint);


                // //Draw a white background
                // canvas.DrawColor(SKColors.Wheat);
                //
                // SKPaint paint = new SKPaint();
                // paint.PathEffect = SKPathEffect.CreateDiscrete(10.0f, 10.0f);
                // paint.Style = SKPaintStyle.Stroke;
                // paint.StrokeWidth = 4;
                // paint.Color = SKColors.Red;
                // paint.IsAntialias = true;
                // paint.StrokeCap = SKStrokeCap.Round;
                //
                // // SKTypeface typeface = SKTypeface.Default;
                //
                // // SKTypeface typeface = SKTypeface.FromFamilyName("Source Code Pro");
                // SKTypeface typeface = SKTypeface.FromFamilyName("Inter");
                //
                //
                // SKFont font1 = new SKFont(typeface, 64.0f, 1.0f, -0.2f);
                //
                //
                // SKTextBlob blob1 = SKTextBlob.Create("Hello World", font1);
                //
                // font1.Edging = SKFontEdging.Antialias;
                //
                // canvas.DrawText(blob1, 100, 100, paint);
                //
                // for (int i = 1; i <= 10; i++)
                // {
                //
                //     var star = CreateStarPath(150 * i, 100 * i);
                //     canvas.DrawPath(star, paint);
                // }
                // //
                // // // SKPath path = new SKPath();
                // // //
                // // // // Starts at 10,10
                // // // path.MoveTo(10, 10);
                // // //
                // // // path.QuadTo(256, 64, 128, 128);
                // // // path.QuadTo(10, 192, 250, 250);
                // //
                // // SKPath path = CreateStarPath();
                // //
                // // canvas.DrawPath(path, paint);

             
              
                

            }
        }

        public Rect Bounds { get; }
        public bool HitTest(Point p) => true;
        public bool Equals(ICustomDrawOperation? other) => false;
        
        
        public SKPath CreateStarPath(int x, int y) {
            const float R = 115.2f, C = 128.0f;
            SKPath path = new SKPath();
            path.MoveTo(C + R + x, C + y);
            for (int i = 1; i < 8; ++i) {
                float a = 2.6927937f * i;
                path.LineTo(C + R * (float)Math.Cos(a) + x, C + R * (float)Math.Sin(a) + y);
            }
            return path;
        }

    }

    public List<SKPoint> brushPoints = new();

    
    protected override void OnPointerMoved(PointerEventArgs e)
    {
        Point p = e.GetPosition(this);
        Console.WriteLine(p);
        brushPoints.Add(new SKPoint((float)p.X, (float)p.Y));
        
        InvalidateVisual();
    }

    public override void Render(DrawingContext context)
    {
        // glue code
        context.Custom(new CustomDrawOp(Bounds, _noSkia, brushPoints));
        Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Render);
    }
}