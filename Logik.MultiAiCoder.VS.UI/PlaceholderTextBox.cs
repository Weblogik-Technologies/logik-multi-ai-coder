using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Logik.MultiAiCoder.VisualStudio.UI
{
    public class PlaceholderTextBox : TextBox
    {
        static PlaceholderTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceholderTextBox),
                new FrameworkPropertyMetadata(typeof(PlaceholderTextBox)));
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PlaceholderTextBox),
                new PropertyMetadata(string.Empty));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
    }
}
