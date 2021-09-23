using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HandyControl.Data;
using HandyControl.Interactivity;
using Microsoft.Win32;

namespace HandyControl.Controls
{
    public class ImageSelector : Control
    {
        public ImageSelector() => CommandBindings.Add(new CommandBinding(ControlCommands.Switch, SwitchImage));

        private void SwitchImage(object sender, ExecutedRoutedEventArgs e)
        {
            if (!HasValue)
            {
                var dialog = new OpenFileDialog
                {
                    RestoreDirectory = true,
                    Filter = Filter,
                    DefaultExt = DefaultExt,
                    Multiselect = true
                };

                if (dialog.ShowDialog() == true)
                {
                    List<Uri> uriList = new List<Uri>();
                    foreach (var file in dialog.FileNames)
                    {
                        uriList.Add(new Uri(file, UriKind.RelativeOrAbsolute));
                    }

                    SetValue(UrisProperty, uriList);
                    SetValue(PreviewBrushProperty,
                        new ImageBrush(BitmapFrame.Create(Uris[0], BitmapCreateOptions.IgnoreImageCache,
                            BitmapCacheOption.None))
                        {
                            Stretch = Stretch
                        });
                    SetValue(HasValueProperty, ValueBoxes.TrueBox);
                    SetCurrentValue(ToolTipProperty, dialog.FileName);
                }
            }
            else
            {
                SetValue(UrisProperty, default(Uri));
                SetValue(PreviewBrushProperty, default(Brush));
                SetValue(HasValueProperty, ValueBoxes.FalseBox);
                SetCurrentValue(ToolTipProperty, default);
            }
        }

        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(
            "Stretch", typeof(Stretch), typeof(ImageSelector), new PropertyMetadata(default(Stretch)));

        public Stretch Stretch
        {
            get => (Stretch) GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        public static DependencyProperty UrisProperty = DependencyProperty.Register(
            "Uris", typeof(List<Uri>), typeof(ImageSelector), new PropertyMetadata(default(List<Uri>)));


        public List<Uri> Uris
        {
            get => (List<Uri>) GetValue(UrisProperty);
            set => SetValue(UrisProperty, value);
        }

        public static readonly DependencyProperty PreviewBrushProperty = DependencyProperty.Register(
            "PreviewBrush", typeof(Brush), typeof(ImageSelector), new PropertyMetadata(default(Brush)));


        public Brush PreviewBrush
        {
            get => (Brush) GetValue(PreviewBrushProperty);
            set => SetValue(PreviewBrushProperty, value);
        }

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness", typeof(double), typeof(ImageSelector),
            new FrameworkPropertyMetadata(ValueBoxes.Double1Box, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThickness
        {
            get => (double) GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly DependencyProperty StrokeDashArrayProperty = DependencyProperty.Register(
            "StrokeDashArray", typeof(DoubleCollection), typeof(ImageSelector),
            new FrameworkPropertyMetadata(default(DoubleCollection), FrameworkPropertyMetadataOptions.AffectsRender));

        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection) GetValue(StrokeDashArrayProperty);
            set => SetValue(StrokeDashArrayProperty, value);
        }

        public static readonly DependencyProperty DefaultExtProperty = DependencyProperty.Register(
            "DefaultExt", typeof(string), typeof(ImageSelector), new PropertyMetadata(".jpg"));

        public string DefaultExt
        {
            get => (string) GetValue(DefaultExtProperty);
            set => SetValue(DefaultExtProperty, value);
        }

        public static readonly DependencyProperty FilterProperty = DependencyProperty.Register(
            "Filter", typeof(string), typeof(ImageSelector), new PropertyMetadata("(.jpg)|*.jpg|(.png)|*.png"));

        public string Filter
        {
            get => (string) GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }

        public static readonly DependencyProperty HasValueProperty = DependencyProperty.Register(
            "HasValue", typeof(bool), typeof(ImageSelector), new PropertyMetadata(ValueBoxes.FalseBox));


        public bool HasValue
        {
            get => (bool) GetValue(HasValueProperty);
            set => SetValue(HasValueProperty, ValueBoxes.BooleanBox(value));
        }
    }
}
