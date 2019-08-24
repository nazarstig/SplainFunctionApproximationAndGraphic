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
           // Console.WriteLine("7: " + _function(7));
            _derivativeFunction = FindDerivative(_function);
          //  Console.WriteLine("der7: " + _derivativeFunction(7));
            _leftEnd = leftEnd;
            _rightEnd = rightEnd;
            _leftOutcutValue = _function(_leftEnd);
            _rightOutcutValue = _function(_rightEnd);
            _leftOutcutDerivationValue = _derivativeFunction(_leftEnd);
            _rightOutcutDerivationValue = _derivativeFunction(_rightEnd);
           // ConstForEquationCount();
            _h = HCount();
            CountPolynomialCoefficients();
        }
        
        public double ConstForEquationCount()
        {
            double constForEquation;
           // constForEquation = ((-_rightOutcutDerivationValue + _leftOutcutDerivationValue) /
           //     (_rightOutcutValue - _leftOutcutValue - _h * (_rightOutcutDerivationValue + _leftOutcutDerivationValue)));
            constForEquation = ((_rightOutcutDerivationValue - _leftOutcutDerivationValue) /
                (_rightOutcutValue - _leftOutcutValue - _h * (_rightOutcutDerivationValue + _leftOutcutDerivationValue)));
            Console.WriteLine("constForEquation count: " + constForEquation);
            return constForEquation;
        }

        public Func<double, double> FunctionForEquationCount()
        {
            // double v1 = -0.5585;
            Console.WriteLine("FunctionForEquationCount");
            Func<double, double> chyselnyk = (x1) => { return ((x1 * (Math.Exp(x1 * _rightEnd) - Math.Exp(x1 * _leftEnd)))); };
            Func<double, double> znamennyk = (x2) => { return ((Math.Exp(x2 * _rightEnd) - Math.Exp(x2 * _leftEnd)  -  _h * x2 * (Math.Exp(x2 * _rightEnd) + Math.Exp(x2 * _leftEnd)))); };
            Func<double, double> fraction = (x) => { return chyselnyk(x) / znamennyk(x); };
            Func<double, double> func;
            func = (v) =>
           {
             //  Console.WriteLine("mudak");
              
               
               //  Console.WriteLine("chyselnyk_deleg: " + chyselnyk(arg));
               //  Console.WriteLine("znamennyk_deleg: " + znamennyk(arg));
               //  Console.WriteLine("readyValues: " + chyselnyk(arg) / znamennyk(arg));
              
           //    Console.WriteLine("fraction : " + fraction(-0.5585));
               return (fraction(v) - _constForEquation);
           };
          //  Console.WriteLine("func: " + func(-0.5585));
            //Console.WriteLine("arguments: ");
            //Console.WriteLine("leftEnd :" + _leftEnd);
            //Console.WriteLine("rightEnd :" + _rightEnd);
            //Console.WriteLine("exp(leftend) :" + Math.Exp(_leftEnd));
            //Console.WriteLine("exp(rightend) :" + Math.Exp(_rightEnd));
            //Console.WriteLine("h :" + _h);
            //Console.WriteLine("ready values: " + func(-0.5585));
            //double v1 = -0.5585;
            //Console.WriteLine("chyselnyk: " + ((v1 * (Math.Exp(v1 * _rightEnd) - Math.Exp(v1 * _leftEnd)))));
            //Console.WriteLine("znamennyk: " + ((Math.Exp(v1 * _rightEnd) - Math.Exp(v1 * _leftEnd) - _h * v1 * (Math.Exp(v1 * _rightEnd) + Math.Exp(v1 * _leftEnd)))));
            //// func = (v) =>
            // {
            //     return ((v * (Math.Exp(v * _rightEnd) - Math.Exp(v * _leftEnd))) /
            //(Math.Exp(v * _rightEnd) - Math.Exp(v * _leftEnd) - v * _h * Math.Exp(v * _rightEnd)) - _constForEquation);
            // };
            return func;

        }
        public double HCount()
        {
            if ((_rightEnd - _leftEnd) > 0) return ((_rightEnd - _leftEnd) / 2);
            else return 0;
        }

        public double VCount()
        {
            //double root = Bisection.FindRoot(_functionForFindingV, -100, 100);
            //Console.WriteLine("root" + root);
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
            // root = Bisection.FindRoot(_functionForFindingV, -100, 101);
          //  lowerLimit = 0.5;
          //  upperLimit = 1000;
            root = HalfDivideMethod(lowerLimit, upperLimit, _functionForFindingV, eps);
           // root = Bisection.FindRoot(_functionForFindingV, lowerLimit, upperLimit);
           // root = NewtonRaphson.FindRoot((x)=> { return 2 * x; }, (x) => { return 2; }, -1000, 1000, 0.1, 10000);
            return root;
          //  Console.WriteLine("function for finding V: " + _functionForFindingV(-0.5585));
            

              
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
          //  " H = " + _h.ToString( )+
          //  " W = " + _constForEquation.ToString();
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
            //Console.WriteLine("-0.2: " + function(-0.2));
            //Console.WriteLine("-0.8: " + function(-0.8));
            //Console.WriteLine("halfDivideMethod: beginning");
            double beginOffcut = beginPoint;
            double endOffcut = endPoint;
            //Console.WriteLine("beginPoint : " + beginOffcut);
            //Console.WriteLine("endPoint: " + endOffcut);
            //Console.WriteLine("f(beginPoint) : " + function(beginOffcut));
            //Console.WriteLine("f(endPoint): " + function(endOffcut));

            if (beginPoint > endPoint)
            {
                Console.WriteLine("Invalid input of points. End is more that begin");
                return 0;
            }

          //  Console.WriteLine("f(beg) * f(end): " + function(beginPoint) + " * " + function(endPoint));

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
                //Console.WriteLine("beginPoint : " + beginOffcut);
                //Console.WriteLine("endPoint: " + endOffcut);
                //Console.WriteLine("middlePoint:  " + middleOffcut);

                //Console.WriteLine("f(beginPoint) : " + function(beginOffcut));
                //Console.WriteLine("f(endPoint): " + function(endOffcut));
                //Console.WriteLine("f(middlePoint):  " + function(middleOffcut));

                
                //Console.WriteLine("middle: " + middleOffcut);
                //Console.WriteLine("begin: " + beginOffcut);
                //Console.WriteLine("end: " + endOffcut);
                if ((function(beginOffcut) * function(middleOffcut)) < 0)
                {
                    endOffcut = middleOffcut;
                }
                else
                    beginOffcut = middleOffcut;

                ++numberOfIterations;
            }

            //Console.WriteLine("Number of iterations: " + numberOfIterations);
            return middleOffcut;

        }

    }
}
