using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GraphicOfFunctionForSplain.Pages
{
    /// <summary>
    /// Interaction logic for ExponentialError.xaml
    /// </summary>
    public partial class TabulationTablePage : UserControl
    {
        
        public TabulationTablePage()
        {
            InitializeComponent();

            this.DataContext = this;
           
        }
        public static readonly DependencyProperty TableItemsProperty = DependencyProperty.Register("TableItems", typeof(ObservableCollection<TabulationFunctionTableItem>), typeof(TabulationTablePage));


        public ObservableCollection<TabulationFunctionTableItem> TableItems
        {
            get { return (ObservableCollection<TabulationFunctionTableItem>)GetValue(TableItemsProperty); }
            set { SetValue(TableItemsProperty, value); }
        }



    }
}
