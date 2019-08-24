using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicOfFunctionForSplain
{
  public  class TabulationFunctionTableItem
    {
        private double _x;
        private double _y;
        private double _exp;
        private double _cubic;
        private double _difYAndExp;
        private double _difYAndCubic;

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public double Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        public double Cubic
        {
            get { return _cubic; }
            set { _cubic = value; }
        }
        public double DifYAndExp
        {
            get { return _difYAndExp; }
            set { _difYAndExp = value; }
        }
        public double DifYAndCubic
        {
            get { return _difYAndCubic; }
            set { _difYAndCubic = value; }
        }



        public TabulationFunctionTableItem(double x, double y, double exp, double cubic, double difYAndExp, double difYAndCubic)
        {
            X = Math.Round(x, 5);
            Y = Math.Round(y, 5);
            Exp = Math.Round(exp, 5);
            Cubic = Math.Round(cubic, 5);
            DifYAndExp = Math.Round(difYAndExp, 5);
            DifYAndCubic = Math.Round(difYAndCubic, 5);
        }
    }
}
