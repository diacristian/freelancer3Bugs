using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FL1.Controls
{
    public partial class VolumeSlider : Slider
    {
        public VolumeSlider()
        {
            InitializeComponent();
            UpdateSlider();
        }

        public Brush SliderFill
        {
            get { return (Brush)GetValue(SliderFillProperty); }
            set { SetValue(SliderFillProperty, value); }
        }

        public static readonly DependencyProperty SliderFillProperty =
            DependencyProperty.Register("SliderFill", typeof(Brush), typeof(VolumeSlider), new PropertyMetadata(new LinearGradientBrush(Colors.Green, Colors.Yellow, 0.0)));

        public int SliderWidth
        {
            get { return (int)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }

        public static readonly DependencyProperty SliderWidthProperty =
            DependencyProperty.Register("SliderWidth", typeof(int), typeof(VolumeSlider), new PropertyMetadata(0));

        public double ActiveSoundLevel
        {
            get { return (double)GetValue(ActiveSoundLevelProperty); }
            set { SetValue(ActiveSoundLevelProperty, value); }
        }

        public static readonly DependencyProperty ActiveSoundLevelProperty =
            DependencyProperty.Register("ActiveSoundLevel", typeof(double), typeof(VolumeSlider), new PropertyMetadata(0.0, new PropertyChangedCallback(gradientChanged)));

        public Brush SliderBorder
        {
            get { return (Brush)GetValue(SliderBorderProperty); }
            set { SetValue(SliderBorderProperty, value); }
        }

        public static readonly DependencyProperty SliderBorderProperty =
            DependencyProperty.Register("SliderBorder", typeof(Brush), typeof(VolumeSlider), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 20, 0))));

        public Color GradientStartColor
        {
            get { return (Color)GetValue(GradientStartColorProperty); }
            set { SetValue(GradientStartColorProperty, value); }
        }

        public static readonly DependencyProperty GradientStartColorProperty =
            DependencyProperty.Register("GradientStartColor", typeof(Color), typeof(VolumeSlider), new PropertyMetadata(Colors.Green, new PropertyChangedCallback(gradientChanged)));

        public Color GradientEndColor
        {
            get { return (Color)GetValue(GradientEndColorProperty); }
            set { SetValue(GradientEndColorProperty, value); }
        }

        public static readonly DependencyProperty GradientEndColorProperty =
            DependencyProperty.Register("GradientEndColor", typeof(Color), typeof(VolumeSlider), new PropertyMetadata(Colors.Red, new PropertyChangedCallback(gradientChanged)));

        private static void gradientChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VolumeSlider slider = (VolumeSlider)d;
            slider.UpdateSlider();
        }

        private void UpdateSlider()
        {
            double ratio = ActiveSoundLevel / Maximum;
            ratio = Math.Max(0, Math.Min(1, ratio));
            double pixelSize;
            if (ActualWidth > 0)
                pixelSize = (ActualWidth - 50) * ratio;
            else if (Width > 0)
                pixelSize = (Width - 50) * ratio;
            else
                pixelSize = 0;
            byte red = (byte)((GradientEndColor.R - GradientStartColor.R) * ratio + GradientStartColor.R);
            byte green = (byte)((GradientEndColor.G - GradientStartColor.G) * ratio + GradientStartColor.G);
            byte blue = (byte)((GradientEndColor.B - GradientStartColor.B) * ratio + GradientStartColor.B);
            byte alpha = (byte)((GradientEndColor.A - GradientStartColor.A) * ratio + GradientStartColor.A);
            Color interpolated = Color.FromArgb(alpha, red, green, blue);
            SliderFill = new LinearGradientBrush(GradientStartColor, interpolated, 0);
            SliderWidth = (int)pixelSize;
        }

        private void Slider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSlider();
        }
    }

    public class BarWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Border))
                return 0.0;

            Border sliderBorder = (Border)value;

            DependencyObject searchObject = sliderBorder;
            while (searchObject != null && !(searchObject is VolumeSlider))
                searchObject = VisualTreeHelper.GetParent(searchObject);

            if (!(searchObject is VolumeSlider))
                return 0.0;

            VolumeSlider volumeSlider = (VolumeSlider)searchObject;

            return volumeSlider.ActiveSoundLevel / volumeSlider.Maximum * (sliderBorder.ActualWidth - sliderBorder.Margin.Right + 1);
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}