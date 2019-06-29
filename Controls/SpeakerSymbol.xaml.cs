using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FL1.Controls
{
    public enum AnimationType
    {
        None,
        GreenGradient,
        Opacity
    }

    public class ClickEventArgs : RoutedEventArgs
    {
        private readonly MouseButton button;

        public MouseButton Button
        {
            get { return button; }
        }

        public ClickEventArgs(RoutedEvent routedEvent, MouseButton button)
        {
            this.RoutedEvent = routedEvent;
            this.button = button;
        }
    }

    public partial class SpeakerSymbol : UserControl
    {
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(SpeakerSymbol));

        public event RoutedEventHandler Click
        {
            add
            {
                AddHandler(ClickEvent, value);
            }
            remove
            {
                RemoveHandler(ClickEvent, value);
            }
        }

        public AnimationType AnimationType
        {
            get { return (AnimationType)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }

        public static readonly DependencyProperty AnimationTypeProperty =
            DependencyProperty.Register("AnimationType", typeof(AnimationType), typeof(SpeakerSymbol), new PropertyMetadata(AnimationType.None, new PropertyChangedCallback(animationChanged)));

        public double AnimationProgress
        {
            get { return (double)GetValue(AnimationProgressProperty); }
            set { SetValue(AnimationProgressProperty, value); }
        }

        public static readonly DependencyProperty AnimationProgressProperty =
            DependencyProperty.Register("AnimationProgress", typeof(double), typeof(SpeakerSymbol), new PropertyMetadata(0.0, new PropertyChangedCallback(animationChanged)));

        public static void animationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SpeakerSymbol speaker = (SpeakerSymbol)d;
            double totalProgress = speaker.AnimationProgress * 3.0 / 100.0;
            double barProgress = totalProgress - Math.Floor(totalProgress);
            if (speaker.AnimationProgress >= 100)
                barProgress = 1;
            if (speaker.AnimationProgress <= 0)
                barProgress = 0;
            switch (speaker.AnimationType)
            {
                case AnimationType.GreenGradient:
                    LinearGradientBrush greenGradient = new LinearGradientBrush();
                    greenGradient.EndPoint = new Point(0.0, 1.0);
                    greenGradient.GradientStops.Add(new GradientStop(Colors.Black, Math.Max(barProgress * 0.6 + 0.5, 0.5002)));
                    greenGradient.GradientStops.Add(new GradientStop(Colors.Green, Math.Max(barProgress * 0.5 + 0.5, 0.5001)));
                    greenGradient.GradientStops.Add(new GradientStop(Colors.Green, Math.Min(barProgress * -0.5 + 0.5, 0.4999)));
                    greenGradient.GradientStops.Add(new GradientStop(Colors.Black, Math.Min(barProgress * -0.6 + 0.5, 0.4998)));

                    if (speaker.AnimationProgress < 100.0 / 3.0)
                    {
                        speaker.SpeakerBarColor1 = greenGradient;
                        speaker.SpeakerBarColor2 = Brushes.Black;
                        speaker.SpeakerBarColor3 = Brushes.Black;
                    }
                    else if (speaker.AnimationProgress < 200.0 / 3.0)
                    {
                        speaker.SpeakerBarColor1 = Brushes.Green;
                        speaker.SpeakerBarColor2 = greenGradient;
                        speaker.SpeakerBarColor3 = Brushes.Black;
                    }
                    else
                    {
                        speaker.SpeakerBarColor1 = Brushes.Green;
                        speaker.SpeakerBarColor2 = Brushes.Green;
                        speaker.SpeakerBarColor3 = greenGradient;
                    }
                    break;

                case AnimationType.Opacity:

                    if (speaker.AnimationProgress < 100.0 / 3.0)
                    {
                        speaker.SpeakerBarColor1 = ChangeBrushOpacity(speaker.SpeakerBarColor1, barProgress);
                        speaker.SpeakerBarColor2 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 0);
                        speaker.SpeakerBarColor3 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 0);
                    }
                    else if (speaker.AnimationProgress < 200.0 / 3.0)
                    {
                        speaker.SpeakerBarColor1 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 1);
                        speaker.SpeakerBarColor2 = ChangeBrushOpacity(speaker.SpeakerBarColor1, barProgress);
                        speaker.SpeakerBarColor3 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 0);
                    }
                    else
                    {
                        speaker.SpeakerBarColor1 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 1);
                        speaker.SpeakerBarColor2 = ChangeBrushOpacity(speaker.SpeakerBarColor1, 1);
                        speaker.SpeakerBarColor3 = ChangeBrushOpacity(speaker.SpeakerBarColor1, barProgress);
                    }
                    break;
            }
        }

        public static Brush ChangeBrushOpacity(Brush input, double opacity)
        {
            Brush output = input.Clone();
            output.Opacity = opacity;
            return output;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SpeakerSymbol), new PropertyMetadata("Master Volume"));

        public Brush SpeakerColor
        {
            get { return (Brush)GetValue(SpeakerColorProperty); }
            set { SetValue(SpeakerColorProperty, value); }
        }

        public static readonly DependencyProperty SpeakerColorProperty =
            DependencyProperty.Register("SpeakerColor", typeof(Brush), typeof(SpeakerSymbol), new PropertyMetadata(Brushes.Black));

        public Brush SpeakerBarColor1
        {
            get { return (Brush)GetValue(SpeakerBarColor1Property); }
            set { SetValue(SpeakerBarColor1Property, value); }
        }

        public static readonly DependencyProperty SpeakerBarColor1Property =
            DependencyProperty.Register("SpeakerBarColor1", typeof(Brush), typeof(SpeakerSymbol), new PropertyMetadata(Brushes.Black));

        public Brush SpeakerBarColor2
        {
            get { return (Brush)GetValue(SpeakerBarColor2Property); }
            set { SetValue(SpeakerBarColor2Property, value); }
        }

        public static readonly DependencyProperty SpeakerBarColor2Property =
            DependencyProperty.Register("SpeakerBarColor2", typeof(Brush), typeof(SpeakerSymbol), new PropertyMetadata(Brushes.Black));

        public Brush SpeakerBarColor3
        {
            get { return (Brush)GetValue(SpeakerBarColor3Property); }
            set { SetValue(SpeakerBarColor3Property, value); }
        }

        public static readonly DependencyProperty SpeakerBarColor3Property =
            DependencyProperty.Register("SpeakerBarColor3", typeof(Brush), typeof(SpeakerSymbol), new PropertyMetadata(Brushes.Black));

        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(SpeakerSymbol), new PropertyMetadata(Brushes.White));

        public SpeakerSymbol()
        {
            InitializeComponent();
        }

        private bool[] buttonDownOnControl = new bool[] { false, false, false, false, false };

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonDownOnControl[(int)e.ChangedButton] = true;
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (buttonDownOnControl[(int)e.ChangedButton])
            {
                ClickEventArgs clickEvent = new ClickEventArgs(ClickEvent, e.ChangedButton);
                RaiseEvent(clickEvent);
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            bool[] buttonState = new bool[] {e.LeftButton == MouseButtonState.Pressed,
                                              e.MiddleButton == MouseButtonState.Pressed,
                                              e.RightButton == MouseButtonState.Pressed,
                                              e.XButton1 == MouseButtonState.Pressed,
                                              e.XButton2 == MouseButtonState.Pressed};
            // If the mouse moves over the control and the mouse button was released outside the control, cancel the click.
            for (int i = 0; i < 5; i++)
            {
                if (!buttonState[i] && buttonDownOnControl[i])
                    buttonDownOnControl[i] = false;
            }
        }
    }
}