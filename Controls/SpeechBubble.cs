using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FL1.Controls
{
    public class SpeechBubble : ContentControl
    {
        static SpeechBubble()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpeechBubble), new FrameworkPropertyMetadata(typeof(SpeechBubble)));
        }

        public SpeechBubble()
        {
            UpdateDisplay();
            this.SizeChanged += SpeechBubble_SizeChanged;
        }

        private void SpeechBubble_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            int width = 0, height = 0;
            if (this.ActualWidth > 0)
                width = (int)this.ActualWidth;
            else if (this.Width > 0)
                width = (int)this.Width;
            else
                return;
            if (this.ActualHeight > 0)
                height = (int)this.ActualHeight;
            else if (this.Height > 0)
                height = (int)this.Height;
            else
                return;

            PathGeometry bubbleGeometry = new PathGeometry();
            PathFigure bubbleFigure = new PathFigure();
            bubbleFigure.IsClosed = true;
            bubbleFigure.StartPoint = new Point(width, height - 2);
            PathSegment bubbleSegment = new BezierSegment(new Point(width - 7, height - 6), new Point(width - 12, height - 12), new Point(width - 12, height - 20), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new LineSegment(new Point(width - 12, 12), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(width - 12, 5), new Point(width - 17, 0), new Point(width - 24, 0), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new LineSegment(new Point(12, 0), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(5, 0), new Point(0, 5), new Point(0, 12), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new LineSegment(new Point(0, height - 12), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(0, height - 5), new Point(5, height), new Point(12, height), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new LineSegment(new Point(width - 24, height), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(width - 22, height), new Point(width - 21, height - 1), new Point(width - 20, height - 1), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(width - 14, height), new Point(width - 6, height), new Point(width, height - 2), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleGeometry.Figures.Add(bubbleFigure);
            BubbleOutline = bubbleGeometry;

            bubbleGeometry = new PathGeometry();
            bubbleFigure = new PathFigure();
            bubbleFigure.IsClosed = true;
            bubbleFigure.StartPoint = new Point(12, 2);
            bubbleSegment = new LineSegment(new Point(width - 24, 2), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(width - 15, 2), new Point(width - 13, 16), new Point(width - 22, 16), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new LineSegment(new Point(10, 16), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleSegment = new BezierSegment(new Point(1, 16), new Point(3, 2), new Point(12, 2), true);
            bubbleFigure.Segments.Add(bubbleSegment);
            bubbleGeometry.Figures.Add(bubbleFigure);
            HighlightOutline = bubbleGeometry;

            LinearGradientBrush backgroundBrush = new LinearGradientBrush();
            backgroundBrush.EndPoint = new Point(0, 1);
            double ratio = 0.74;
            Color gradientColor = Color.FromRgb((byte)(BaseColor.R * ratio), (byte)(BaseColor.G * ratio), (byte)(BaseColor.B * ratio));
            backgroundBrush.GradientStops.Add(new GradientStop(gradientColor, 0));
            ratio = 0.79;
            gradientColor = Color.FromRgb((byte)(BaseColor.R * ratio), (byte)(BaseColor.G * ratio), (byte)(BaseColor.B * ratio));
            backgroundBrush.GradientStops.Add(new GradientStop(gradientColor, 12 / height));
            backgroundBrush.GradientStops.Add(new GradientStop(BaseColor, 1));
            BubbleFill = backgroundBrush;
        }

        public Geometry BubbleOutline
        {
            get { return (Geometry)GetValue(BubbleOutlineProperty); }
            set { SetValue(BubbleOutlineProperty, value); }
        }

        public static readonly DependencyProperty BubbleOutlineProperty =
            DependencyProperty.Register("BubbleOutline", typeof(Geometry), typeof(SpeechBubble), new PropertyMetadata(new RectangleGeometry(new Rect(0.0, 0.0, 100.0, 100.0))));

        public Color BaseColor
        {
            get { return (Color)GetValue(BaseColorProperty); }
            set { SetValue(BaseColorProperty, value); }
        }

        public static readonly DependencyProperty BaseColorProperty =
            DependencyProperty.Register("BaseColor", typeof(Color), typeof(SpeechBubble), new PropertyMetadata(Colors.Lime));

        public Brush BubbleFill
        {
            get { return (Brush)GetValue(BubbleFillProperty); }
            set { SetValue(BubbleFillProperty, value); }
        }

        public static readonly DependencyProperty BubbleFillProperty =
            DependencyProperty.Register("BubbleFill", typeof(Brush), typeof(SpeechBubble), new PropertyMetadata(Brushes.White));

        public Brush BubbleStroke
        {
            get { return (Brush)GetValue(BubbleStrokeProperty); }
            set { SetValue(BubbleStrokeProperty, value); }
        }

        public static readonly DependencyProperty BubbleStrokeProperty =
            DependencyProperty.Register("BubbleStroke", typeof(Brush), typeof(SpeechBubble), new PropertyMetadata(new LinearGradientBrush(Color.FromRgb(64, 64, 64), Color.FromRgb(128, 128, 128), 90.0)));

        public Geometry HighlightOutline
        {
            get { return (Geometry)GetValue(HighlightOutlineProperty); }
            set { SetValue(HighlightOutlineProperty, value); }
        }

        public static readonly DependencyProperty HighlightOutlineProperty =
            DependencyProperty.Register("HighlightOutline", typeof(Geometry), typeof(SpeechBubble), new PropertyMetadata(new RectangleGeometry(new Rect(0.0, 0.0, 100.0, 100.0))));

        public bool Right
        {
            get { return (bool)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(bool), typeof(SpeechBubble), new PropertyMetadata(false));
    }
}