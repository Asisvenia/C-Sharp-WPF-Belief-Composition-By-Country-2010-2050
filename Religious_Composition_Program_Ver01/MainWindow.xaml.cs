using Religious_Composition_Program_Ver01.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Religious_Composition_Program_Ver01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            allCountriesBtn_Click(this, null);
        }

        private void allCountriesBtn_Click(object sender, RoutedEventArgs e)
        {
            allCountriesBtn.Background = (GradientBrush) Application.Current.FindResource("LinearGradient_Purple");
            allCountriesBtn.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#6A136B"));
            byCountryBtn.Background = (GradientBrush) Application.Current.FindResource("TabbtnLinear");
        }

        private void byCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            byCountryBtn.Background = (GradientBrush) Application.Current.FindResource("LinearGradient_Purple");
            allCountriesBtn.Background = (GradientBrush) Application.Current.FindResource("TabbtnLinear");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1,
                            (Duration)TimeSpan.FromSeconds(1));
            this.BeginAnimation(UIElement.OpacityProperty, animation);
        }
    }
}
