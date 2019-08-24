using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Differentiation;
using MathNet.Numerics.RootFinding;
namespace GraphicOfFunctionForSplain
{
   public class ExponentialHermiteSplain
    {
        private double _A;
        private double _B;
        private double _C;
        private double _V;

        private double _leftEnd;
        private double _rightEnd;
        private double _leftOutcutValue;
        private double _rightOutcutValue;
        private double _leftOutcutDerivationValue;
        private double _rightOutcutDerivationValue;
        private double _h;
        private double _constForEquation; 
        private Func<double, double> _function;
        private Func<double, double> _derivativeFunction;
        private Func<double, double> _functionForFindingV;
        
        public ExponentialHermiteSplain(Func<double, double> function, double leftEnd, double rightEnd)
        {
            _function = function;
         
            _derivativeFunction = FindDerivative(_function);
         
            _leftEnd = leftEnd;
            _rightEnd = rightEnd;
            _leftOutcutValue = _function(_leftEnd);
            _rightOutcutValue = _function(_rightEnd);
            _leftOutcutDerivationValue = _derivativeFunction(_leftEnd);
            _rightOutcutDerivationValue = _derivativeFunction(_rightEnd);
          
            _h = HCount();
            CountPolynomialCoefficients();
        }
        
        public double ConstForEquationCount()
        {
            double constForEquation;
         
            constForEquation = ((_rightOutcutDerivationValue - _leftOutcutDerivationValue) /
                (_rightOutcutValue - _leftOutcutValue - _h * (_rightOutcutDerivationValue + _leftOutcutDerivationValue)));
            Console.WriteLine("constForEquation count: " + constForEquation);
            return constForEquation;
        }

        public Func<double, double> FunctionForEquationCount()
        {
            
            Console.WriteLine("FunctionForEquationCount");
            Func<double, double> chyselnyk = (x1) => { return ((x1 * (Math.Exp(x1 * _rightEnd) - Math.Exp(x1 * _leftEnd)))); };
            Func<double, double> znamennyk = (x2) => { return ((Math.Exp(x2 * _rightEnd) - Math.Exp(x2 * _leftEnd)  -  _h * x2 * (Math.Exp(x2 * _rightEnd) + Math.Exp(x2 * _leftEnd)))); };
            Func<double, double> fraction = (x) => { return chyselnyk(x) / znamennyk(x); };
            Func<double, double> func;
            func = (v) =>
           {
         
               return (fraction(v) - _constForEquation);
           };
         
            return func;

        }
        public double HCount()
        {
            if ((_rightEnd - _leftEnd) > 0) return ((_rightEnd - _leftEnd) / 2);
            else return 0;
        }

        public double VCount()
        {
           
            double root;
            double upperLimit, lowerLimit;
            double eps = 0.00001;
            if (_constForEquation > 0)
            {
                lowerLimit = -45;
                upperLimit = -0.01;
            }
            else
            {
                lowerLimit = 0.01;
                upperLimit = 45;
            }
        
            root = HalfDivideMethod(lowerLimit, upperLimit, _functionForFindingV, eps);
        
            return root;
         
            

              
        }

        public double ACount()
        {
            return ((_rightOutcutValue - _leftOutcutValue - _C * (Math.Exp(_V * _rightEnd) - Math.Exp(_V * _leftEnd))) / (_h*2));
        }

        public double BCount()
        {
            return (_leftOutcutValue - _A * _leftEnd - _C * Math.Exp(_V*_leftEnd));
        }

        public double CCount()
        {
            if (_V == 0)
                return 0;
            else
            return ((_rightOutcutDerivationValue - _leftOutcutDerivationValue)/(_V * (Math.Exp(_V * _rightEnd) - Math.Exp(_V * _leftEnd))));
        }

      static public Func<double, double> FindDerivative(Func<double, double> function)
        {
            NumericalDerivative derivative = new NumericalDerivative();
            Func<double, double> derivativeFunction;
            derivativeFunction = derivative.CreateDerivativeFunctionHandle(function, 1);
            return derivativeFunction;
        }

        public void CountPolynomialCoefficients()
        {
            _constForEquation = ConstForEquationCount();
            _functionForFindingV = FunctionForEquationCount(); 
            _V = VCount();
            _C = CCount();
            _A = ACount();
            _B = BCount();
        }

        public void ShowAllData()
        {
            Console.WriteLine("A = " + _A);
            Console.WriteLine("B = " + _B);
            Console.WriteLine("C = " + _C);
            Console.WriteLine("V = " + _V);
            Console.WriteLine("H = " + _h);
            Console.WriteLine("W = " + _constForEquation);

        }

        public string AllData()
        {
            string data;
            data = "A = " + Math.Round(_A, 4).ToString() + " B = " + Math.Round(_B, 4).ToString() +
            "  C = " + Math.Round(_C, 4).ToString() +
           " V = " + Math.Round(_V, 4).ToString();
         
            return data;
        }

        public Func<double, double>  Polynomial()
        {
            Func<double, double> polynomial ;
            polynomial = (x) => {return _A*x +_B + _C * Math.Exp(_V * x); };
            return polynomial;
        }
        
        public static double HalfDivideMethod(double beginPoint, double endPoint, Func<double, double> function, double Eps)
        {
  
            double beginOffcut = beginPoint;
            double endOffcut = endPoint;


            if (beginPoint > endPoint)
            {
                Console.WriteLine("Invalid input of points. End is more that begin");
                return 0;
            }

       

            if (function(beginPoint) * function(endPoint) > 0)
            {
                Console.WriteLine("There is not root in this outcut. Enter another point");
                return 0;
            }
           
             
            double middleOffcut = 0;
            bool isEnd = false;
            int numberOfIterations = 0;
            double difference = 0;
            while (!isEnd)
            {
                if ((endOffcut - beginOffcut) < Eps)
                    isEnd = true;

                middleOffcut = (endOffcut + beginOffcut) / 2;
            
                if ((function(beginOffcut) * function(middleOffcut)) < 0)
                {
                    endOffcut = middleOffcut;
                }
                else
                    beginOffcut = middleOffcut;

                ++numberOfIterations;
            }

           
            return middleOffcut;

        }

    }
}
