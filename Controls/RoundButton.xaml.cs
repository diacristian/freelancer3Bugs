using System.Windows;
using System.Windows.Controls;

namespace FL1.Controls
{
    public partial class RoundButton : Button
    {
        public RoundButton()
        {
            InitializeComponent();
        }

        public float BevelWidth
        {
            get { return (float)GetValue(BevelWidthProperty); }
            set { SetValue(BevelWidthProperty, value); }
        }

        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(float), typeof(RoundButton), new PropertyMetadata(10.0f));
    }
}