using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace FL1.Controls
{
    public class LightenEffect : ShaderEffect
    {
        private static PixelShader _pixelShader = new PixelShader();

        static LightenEffect()
        {
            _pixelShader.UriSource = new Uri(@"pack://application:,,,/FL1;component/Controls/PixelShaders/lighten.ps");
        }

        public LightenEffect()
        {
            this.PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
        }

        public float Amount
        {
            get { return (float)GetValue(AmountProperty); }
            set { SetValue(AmountProperty, value); }
        }

        public static readonly DependencyProperty AmountProperty = DependencyProperty.Register("Amount", typeof(float), typeof(LightenEffect), new PropertyMetadata(0.1f, PixelShaderConstantCallback(0)));

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(LightenEffect), 0);
    }
}