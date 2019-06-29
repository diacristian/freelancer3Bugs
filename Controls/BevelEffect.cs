using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace FL1.Controls
{
    public class BevelEffect : ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader();

        static BevelEffect()
        {
            _pixelShader.UriSource = new Uri(@"pack://application:,,,/FL1;component/Controls/PixelShaders/bevelShader.ps");
        }

        public BevelEffect()
        {
            this.PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(BevelRatioProperty);
        }

        public float BevelRatio
        {
            get { return (float)GetValue(BevelRatioProperty); }
            set { SetValue(BevelRatioProperty, value); }
        }

        public static readonly DependencyProperty BevelRatioProperty = DependencyProperty.Register("BevelRatio", typeof(float), typeof(BevelEffect), new PropertyMetadata(0.1f, PixelShaderConstantCallback(0)));

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(BevelEffect), 0);
    }

    /// <summary>
    /// Convert bevel pixel width and control pixel width and get the ratio to give to the filter
    /// </summary>
    public class BevelRadiusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length < 2 || !(values[0] is float || values[0] is double) || !(values[1] is float || values[1] is double))
                return 0.1;
            // converter is passed in objects representing floats, so we have to use System.Convert instead of simple casting
            float bevelRatio = System.Convert.ToSingle(values[0]) / System.Convert.ToSingle(values[1]);
            return bevelRatio;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}