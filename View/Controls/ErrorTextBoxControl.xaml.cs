using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PilkarzeMVVM.View
{
    public partial class ErrorTextBoxControl : UserControl
    {
        public ErrorTextBoxControl()
        {
            InitializeComponent();
        }


        public bool ShowToolTip
        {
            get
            {
                return (bool)GetValue(ShowToolTipProperty);
            }
            set
            {
                SetValue(ShowToolTipProperty, value);
            }
        }

        public static readonly DependencyProperty ShowToolTipProperty = DependencyProperty.Register("ShowToolTip", typeof(bool), typeof(ErrorTextBoxControl), new PropertyMetadata(null));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ErrorTextBoxControl), new PropertyMetadata(null));

    }
}
