using System.Windows;

namespace Wpf.TonUINOManager.Themes.Controls
{
    public class TabControl : System.Windows.Controls.TabControl
    {
        public static DependencyProperty TabPaddingProperty = DependencyProperty.Register("TabPadding", typeof(Thickness), typeof(TabControl), new PropertyMetadata(new Thickness(0)));

        public Thickness TabPadding
        {
            get => (Thickness)GetValue(TabPaddingProperty);
            set => SetValue(TabPaddingProperty, value);
        }

        static TabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
        }
    }
}
