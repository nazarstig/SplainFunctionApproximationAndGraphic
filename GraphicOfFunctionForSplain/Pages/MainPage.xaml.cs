using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using GraphicOfFunctionForSplain.ViewModels;
using OxyPlot;

namespace GraphicOfFunctionForSplain.Pages
{
    public partial class MainPage : Page
    {
        private static readonly Regex _regex = new Regex("[^0-9.,-]+"); //regex that matches disallowed text

        public MainPage()
        {

            InitializeComponent();

            //BitmapImage bi3 = new BitmapImage();
            //bi3.BeginInit();
            //bi3.UriSource = new Uri("Images/14010.jpg", UriKind.Relative);
            //bi3.EndInit();
            //CubicSplainImage.Stretch = System.Windows.Media.Stretch.Fill;

            //CubicSplainImage.Source = bi3;
            //Bitmap bitmap = Properties.Resources.CubicSection;

            //CubicSplainImage.Source = new BitmapImage(
            //new Uri("pack://application:,,,/GraphicOfFunctionForSplain;component/Resources/Images/CubicSection.PNG"));

           // CubicSplainImage.Source = new BitmapImage(new Uri("Images/14010.jpg", UriKind.Relative));
        }


        //public void AnotherFunctionSelected(object sender, EventArgs e)
        //{
        //    _func = (x) => { return Math.Pow(x, 2); };
        //    DrawPlot(_leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());
        //}

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void PreviewTextInputHandler(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void ChangeFunc(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
