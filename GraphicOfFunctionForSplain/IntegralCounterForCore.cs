using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.Differentiation;
namespace GraphicOfFunctionForSplain
{
    class IntegralCounterForCore : IntegralCounter
    {
        Func<double, double> _coreFunc;
        Func<double, double> _underIntegralFunction;
        
        int _kindOfCore;

      //  static int _parametersAmount = 4;
        //int _r = 1;
        //double _error;

        public int KindOfCore
        {
            get { return _kindOfCore; }
            set { _kindOfCore = value; }
        }

        public Func<double, double> CoreFunc
        {
            get { return _coreFunc; }
            set { _coreFunc = value; }
        }

        public Func<double, double> UnderIntegralFunction
        {
            get { return _underIntegralFunction; }
            set { _underIntegralFunction = value; }
        }

        //public static int ParametersAmount
        //{
        //    get { return _parametersAmount; }
        //    set { _parametersAmount = value; }
        //}

        //public int R
        //{
        //    get { return _r; }
        //    set { _r = value; }
        //}

        //public double Error
        //{
        //    get { return _error; }
        //    set { _error = value; }
        //}

        public IntegralCounterForCore(double leftLimit, double rightLimit, Func<double, double> func, double precision, int kindOfCore) : 
            base(leftLimit, rightLimit, func, precision)
        {
            KindOfCore = kindOfCore;
            //Func<double, double>  CoreFunc = CountCoreFunc(Func, KindOfCore);
            // Func<double, double>   UnderIntegralFunction = (x) => { return Math.Abs(CoreFunc(x)); };//Math.Pow(Math.Abs(CoreFunc(x)), 0.25); };
            IntegralValue = CountIntegralForCore(KindOfCore);//CountIntegral(leftLimit, rightLimit, UnderIntegralFunction, Precision);
           // R = r;
           // Error = ErrorCount(IntegralValue, ParametersAmount, R);
        }

        public Func<double, double> CountCoreFunc(Func<double, double> func, int kindOfCore)
        {
            NumericalDerivative derivative = new NumericalDerivative(6, 3);
            Func<double, double> fourthDerivative = derivative.CreateDerivativeFunctionHandle(func, 4); //CountDerivative(func, 4);
            Func<double, double> thirdDerivative = derivative.CreateDerivativeFunctionHandle(func, 3);//CountDerivative(func, 3);
            Func<double, double> secondDerivative = derivative.CreateDerivativeFunctionHandle(func, 2);//CountDerivative(func, 2);

            switch (kindOfCore)
            {
                case 1: { return (x) => { return (fourthDerivative(x) - Math.Pow(thirdDerivative(x), 2) / (secondDerivative(x))); }; }
                case 2: { return (x) => { return fourthDerivative(x); }; };
                default: break;
            }
            return null;

        }

        public double CountIntegralForCore(int kindOfCore)
        {
            Func<double, double> core;
            NumericalDerivative derivative = new NumericalDerivative(10, 5);
            Func<double, double> fourthDerivative = derivative.CreateDerivativeFunctionHandle(Func, 4); //CountDerivative(func, 4);
            Func<double, double> thirdDerivative = derivative.CreateDerivativeFunctionHandle(Func, 3);//CountDerivative(func, 3);
            Func<double, double> secondDerivative = derivative.CreateDerivativeFunctionHandle(Func, 2);//CountDerivative(func, 2);
            //Console.WriteLine("fourthDerivative = " + fourthDerivative(1.57));
            //Console.WriteLine("thirdDerivative = " + thirdDerivative(1.57));
            //Console.WriteLine("secondDerivative = " + secondDerivative(1.57));
            if(kindOfCore == 1)
             core = (x) => { return (fourthDerivative(x) - ( (Math.Pow(thirdDerivative(x), 2)) / (secondDerivative(x + 0.00001)))); };
            else core = (x) => { return (fourthDerivative(x)) ; };

           // Console.WriteLine("core = " + core(1));
            Func<double, double> underIntegralFunc = (x)=> { return Math.Pow(Math.Abs((core(x) )), 0.25); };
            double integralValue = CountIntegral(LeftLimit, RightLimit, underIntegralFunc, Precision);
            return integralValue;
        }

        //public double Factorial(double number)
        //{
        //    double res = 1;
        //    double n = number;
        //    while (n != 1)
        //    {
        //        res *= n;
        //        n--;
        //    }
        //    return res;
        //}

        //public double ErrorCount(double integralValue, int parametersAmount, double r)
        //{
        //    double error = 0;

        //    // double integralValue = CountIntegral();
        //    error = ((Math.Pow(integralValue, parametersAmount)) / (Math.Pow(2, parametersAmount) * Factorial(parametersAmount) * Math.Pow(r, parametersAmount)));
        //    return error;
        //}



    }
}
