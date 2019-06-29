using System.Windows;
using System.Windows.Controls;

namespace FL1.Controls
{
    public class RoundButton2 : Button
    {
        static RoundButton2()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundButton2), new FrameworkPropertyMetadata(typeof(RoundButton2)));
        }

        public float BevelWidth
        {
            get { return (float)GetValue(BevelWidthProperty); }
            set { SetValue(BevelWidthProperty, value); }
        }

        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(float), typeof(RoundButton2), new PropertyMetadata(10.0f));
    }
}