using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using OxyPlot;
using GraphicOfFunctionForSplain.ViewModels;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GraphicOfFunctionForSplain.Pages
{
    /// <summary>
    /// Interaction logic for CubicHermiteSplainPage.xaml
    /// </summary>
    public partial class GraphicPage : UserControl
    {
        public GraphicPage()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public static readonly DependencyProperty GraphicProperty = DependencyProperty.Register("Graphic", typeof(PlotModel), typeof(GraphicPage));

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(GraphicPage));

        public static readonly DependencyProperty ImagePathProperty = DependencyProperty.Register("ImagePath", typeof(BitmapImage), typeof(GraphicPage));


        public PlotModel Graphic
        {
            get { return (PlotModel)GetValue(GraphicProperty); }
            set { SetValue(GraphicProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }


        public BitmapImage ImagePath
        {
            get { return (BitmapImage)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

    }
}
