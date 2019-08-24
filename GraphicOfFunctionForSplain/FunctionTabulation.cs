using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicOfFunctionForSplain
{
    delegate double MathFunction(double x);
   public class FunctionTabulation
    {
        private Func<double, double> _function;
        private double _step;
        private double _leftEnd;
        private double _rightEnd;
        private List<double> _x = new List<double>();
        private List<double> _y = new List<double>();

        public List<double> X
        {
            get { return _x; }
            set { _x = value; }
        }
        public List<double> Y
        {
            get { return _y; }
            set { _y = value; }
        }

       
       
        public FunctionTabulation(Func<double, double> function, double step, double leftEnd, double rightEnd)
        {
            _function = function;
            _step = step;
            _leftEnd = leftEnd;
            _rightEnd = rightEnd;
            Y = ValuesOfFunction();

        }

        public List<double> ValuesOfFunction()
        {
            List<double> valuesOfFunction = new List<double>();
            double x = _leftEnd;
            double value;
            while(x <= _rightEnd)
            {
                X.Add(x);
                value = _function(x);
                valuesOfFunction.Add(value);
                
                x += _step;
            }

            return valuesOfFunction;
        }
       
        public double FindMaxError(Func<double, double> func)
        {
            List<double> values = ValuesOfFunction();
            double maxError = 0;
            double error;
            for(double x = _leftEnd; x <= _rightEnd; x = x + _step)
            {
                error = Math.Abs(_function(x) - func(x));
                if (error > maxError)
                {
                    maxError = error;
                }
            }
            return maxError;
        }

    }
}
