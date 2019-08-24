using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GraphicOfFunctionForSplain
{
   public class Helper
    {
        private BitmapImage _cubicSplainImagePath;

        public BitmapImage CubicSplainImagePath
        {
            get { return _cubicSplainImagePath; }
            set { _cubicSplainImagePath = value; }
        }

        private BitmapImage _exponentialSplainImagePath;

        public BitmapImage ExponentialSplainImagePath
        {
            get { return _exponentialSplainImagePath; }
            set { _exponentialSplainImagePath = value; }
        }

        public Helper()
        {
            CubicSplainImagePath = new BitmapImage(new Uri("pack://application:,,,/GraphicOfFunctionForSplain;component/Resources/Images/CubicSection.PNG"));
            ExponentialSplainImagePath = new BitmapImage(new Uri("pack://application:,,,/GraphicOfFunctionForSplain;component/Resources/Images/ExponentialSection.PNG"));
        }

    }
}
