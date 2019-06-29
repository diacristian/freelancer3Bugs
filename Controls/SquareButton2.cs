using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FL1.Controls
{
    public class SquareButton2 : Button
    {
        static SquareButton2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SquareButton2), new FrameworkPropertyMetadata(typeof(SquareButton2)));
        }

        public SquareButton2()
        {
            this.SizeChanged += Button_SizeChanged;
            GenerateGradients();
        }

        public double BevelWidth
        {
            get { return (double)GetValue(BevelWidthProperty); }
            set { SetValue(BevelWidthProperty, value); }
        }

        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(double), typeof(SquareButton2), new PropertyMetadata(10.0));

        public double BevelHeight
        {
            get { return (double)GetValue(BevelHeightProperty); }
            set { SetValue(BevelHeightProperty, value); }
        }

        public static readonly DependencyProperty BevelHeightProperty =
            DependencyProperty.Register("BevelHeight", typeof(double), typeof(SquareButton2), new PropertyMetadata(10.0));

        public Color BaseColor
        {
            get { return (Color)GetValue(BaseColorProperty); }
            set { SetValue(BaseColorProperty, value); }
        }

        public static readonly DependencyProperty BaseColorProperty =
            DependencyProperty.Register("BaseColor", typeof(Color), typeof(SquareButton2), new PropertyMetadata(Colors.PaleTurquoise, new PropertyChangedCallback(colorChanged)));

        public static void colorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SquareButton2 button = (SquareButton2)d;
            button.GenerateGradients();
        }

        #region Internal Gradient Helpers

        private static Color Darken(Color original, double multiple)
        {
            double r, g, b;
            r = Math.Max(0, Math.Min(255, original.R * multiple));
            g = Math.Max(0, Math.Min(255, original.G * multiple));
            b = Math.Max(0, Math.Min(255, original.B * multiple));
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

        private static Color Lighten(Color original, double multiple)
        {
            double r, g, b;
            r = Math.Max(0, Math.Min(255, 255 - ((255 - original.R) * multiple)));
            g = Math.Max(0, Math.Min(255, 255 - ((255 - original.G) * multiple)));
            b = Math.Max(0, Math.Min(255, 255 - ((255 - original.B) * multiple)));
            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }

        public LinearGradientBrush TopGradient
        {
            get { return (LinearGradientBrush)GetValue(TopGradientProperty.DependencyProperty); }
            protected set { SetValue(TopGradientProperty, value); }
        }

        public LinearGradientBrush LeftGradient
        {
            get { return (LinearGradientBrush)GetValue(LeftGradientProperty.DependencyProperty); }
            protected set { SetValue(LeftGradientProperty, value); }
        }

        public LinearGradientBrush BottomGradient
        {
            get { return (LinearGradientBrush)GetValue(BottomGradientProperty.DependencyProperty); }
            protected set { SetValue(BottomGradientProperty, value); }
        }

        public LinearGradientBrush RightGradient
        {
            get { return (LinearGradientBrush)GetValue(RightGradientProperty.DependencyProperty); }
            protected set { SetValue(RightGradientProperty, value); }
        }

        public RadialGradientBrush CenterGradient
        {
            get { return (RadialGradientBrush)GetValue(CenterGradientProperty.DependencyProperty); }
            protected set { SetValue(CenterGradientProperty, value); }
        }

        internal static readonly DependencyPropertyKey TopGradientProperty = DependencyProperty.RegisterReadOnly("TopGradient", typeof(LinearGradientBrush), typeof(SquareButton2), new PropertyMetadata(new LinearGradientBrush()));
        internal static readonly DependencyPropertyKey LeftGradientProperty = DependencyProperty.RegisterReadOnly("LeftGradient", typeof(LinearGradientBrush), typeof(SquareButton2), new PropertyMetadata(new LinearGradientBrush()));
        internal static readonly DependencyPropertyKey BottomGradientProperty = DependencyProperty.RegisterReadOnly("BottomGradient", typeof(LinearGradientBrush), typeof(SquareButton2), new PropertyMetadata(new LinearGradientBrush()));
        internal static readonly DependencyPropertyKey RightGradientProperty = DependencyProperty.RegisterReadOnly("RightGradient", typeof(LinearGradientBrush), typeof(SquareButton2), new PropertyMetadata(new LinearGradientBrush()));
        internal static readonly DependencyPropertyKey CenterGradientProperty = DependencyProperty.RegisterReadOnly("CenterGradient", typeof(RadialGradientBrush), typeof(SquareButton2), new PropertyMetadata(new RadialGradientBrush()));

        public bool isLandscape
        {
            get { return (bool)GetValue(isLandscapeProperty.DependencyProperty); }
            protected set { SetValue(isLandscapeProperty, value); }
        }

        internal static readonly DependencyPropertyKey isLandscapeProperty = DependencyProperty.RegisterReadOnly("isLandscape", typeof(bool), typeof(SquareButton2), new PropertyMetadata(true));

        private void GenerateGradients()
        {
            TopGradient = InternalTopGradient;
            LeftGradient = InternalLeftGradient;
            BottomGradient = InternalBottomGradient;
            RightGradient = InternalRightGradient;
            CenterGradient = InternalCenterGradient;
            isLandscape = InternalisLandscape;
        }

        private LinearGradientBrush InternalTopGradientBase
        {
            get
            {
                LinearGradientBrush gradient = new LinearGradientBrush();
                gradient.MappingMode = BrushMappingMode.Absolute;
                gradient.StartPoint = new Point(0, 0);
                gradient.EndPoint = new Point(0, BevelWidth);
                return gradient;
            }
        }

        private LinearGradientBrush InternalLeftGradientBase
        {
            get
            {
                LinearGradientBrush gradient = new LinearGradientBrush();
                gradient.MappingMode = BrushMappingMode.Absolute;
                gradient.StartPoint = new Point(0, 0);
                gradient.EndPoint = new Point(BevelWidth, 0);
                return gradient;
            }
        }

        private LinearGradientBrush InternalBottomGradientBase
        {
            get
            {
                LinearGradientBrush gradient = new LinearGradientBrush();
                gradient.MappingMode = BrushMappingMode.Absolute;
                double bottom;
                if (ActualHeight != 0 && ActualWidth != 0)
                {
                    if (ActualWidth <= ActualHeight)
                        bottom = ActualWidth / 2;
                    else
                        bottom = ActualHeight - BevelWidth - 1;
                }
                else
                {
                    if (Width <= Height)
                        bottom = Width / 2;
                    else
                        bottom = Height - BevelWidth - 1;
                }
                gradient.StartPoint = new Point(0, bottom);
                gradient.EndPoint = new Point(0, bottom - BevelWidth);
                return gradient;
            }
        }

        private LinearGradientBrush InternalRightGradientBase
        {
            get
            {
                LinearGradientBrush gradient = new LinearGradientBrush();
                gradient.MappingMode = BrushMappingMode.Absolute;
                double right;
                if (ActualHeight != 0 && ActualWidth != 0)
                {
                    if (ActualWidth > ActualHeight)
                        right = ActualHeight / 2;
                    else
                        right = ActualWidth - BevelWidth - 1;
                }
                else
                {
                    if (Width > Height)
                        right = Height / 2;
                    else
                        right = Width - BevelWidth - 1;
                }
                gradient.StartPoint = new Point(right, 0);
                gradient.EndPoint = new Point(right - BevelWidth, 0);
                return gradient;
            }
        }

        private LinearGradientBrush InternalTopGradient
        {
            get
            {
                LinearGradientBrush gradient = InternalTopGradientBase;
                gradient.GradientStops.Add(new GradientStop(Lighten(BaseColor, (BevelWidth - BevelHeight) / BevelWidth), 0));
                gradient.GradientStops.Add(new GradientStop(BaseColor, 1));
                return gradient;
            }
        }

        private LinearGradientBrush InternalLeftGradient
        {
            get
            {
                LinearGradientBrush gradient = InternalLeftGradientBase;
                gradient.GradientStops.Add(new GradientStop(Lighten(BaseColor, (BevelWidth - BevelHeight) / BevelWidth), 0));
                gradient.GradientStops.Add(new GradientStop(BaseColor, 1));
                return gradient;
            }
        }

        private LinearGradientBrush InternalBottomGradient
        {
            get
            {
                LinearGradientBrush gradient = InternalBottomGradientBase;
                gradient.GradientStops.Add(new GradientStop(Darken(BaseColor, (BevelWidth - BevelHeight) / BevelWidth), 0));
                gradient.GradientStops.Add(new GradientStop(BaseColor, 1));
                return gradient;
            }
        }

        private LinearGradientBrush InternalRightGradient
        {
            get
            {
                LinearGradientBrush gradient = InternalRightGradientBase;
                gradient.GradientStops.Add(new GradientStop(Darken(BaseColor, (BevelWidth - BevelHeight) / BevelWidth), 0));
                gradient.GradientStops.Add(new GradientStop(BaseColor, 1));
                return gradient;
            }
        }

        private RadialGradientBrush InternalCenterGradient
        {
            get
            {
                RadialGradientBrush gradient = new RadialGradientBrush();
                gradient.GradientStops.Add(new GradientStop(Lighten(BaseColor, 0.9), 0));
                gradient.GradientStops.Add(new GradientStop(BaseColor, 1));
                return gradient;
            }
        }

        private bool InternalisLandscape
        {
            get
            {
                if (ActualWidth != 0 && ActualHeight != 0)
                    return ActualWidth > ActualHeight;
                return Width > Height;
            }
        }

        public Thickness TopMargin { get { return new Thickness(0, 0, 0, BevelWidth + 1); } }
        public Thickness LeftMargin { get { return new Thickness(0, 0, BevelWidth + 1, 0); } }
        public Thickness BottomMargin { get { return new Thickness(0, BevelWidth + 1, 0, 0); } }
        public Thickness RightMargin { get { return new Thickness(BevelWidth + 1, 0, 0, 0); } }
        public Thickness CenterMargin { get { return new Thickness(BevelWidth + 1); } }

        private void Button_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GenerateGradients();
        }

        #endregion Internal Gradient Helpers
    }
}