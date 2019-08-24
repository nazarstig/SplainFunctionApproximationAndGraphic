using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.Differentiation;
namespace GraphicOfFunctionForSplain
{
    class ErrorCounter : IntegralCounterForCore
    {
      

        static int _parametersAmount = 4;
        int _r = 1;
        double _error;


        

       


        public static int ParametersAmount
        {
            get { return _parametersAmount; }
            set { _parametersAmount = value; }
        }

        public int R
        {
            get { return _r; }
            set { _r = value; }
        }

        public double Error
        {
            get { return _error; }
            set { _error = value; }
        }

        public ErrorCounter(double leftLimit, double rightLimit, Func<double, double> func, double precision, int kindOfCore, int r) :
             base(leftLimit, rightLimit, func, precision, kindOfCore)
        {

            Console.WriteLine("IntegralValue = " + IntegralValue);
            R = r;
            Error = ErrorCount(IntegralValue, ParametersAmount, R);
        }

      

        public double Factorial(double number)
        {
            double res = 1;
            double n = number;
            while (n != 1)
            {
                res *= n;
                n--;
            }
            return res;
        }

        public double ErrorCount(double integralValue, int parametersAmount, double r)
        {
            double error = 0;

            
            error = ((Math.Pow(integralValue, parametersAmount)) / (Math.Pow(2, parametersAmount) * Factorial(parametersAmount) * Math.Pow(r, parametersAmount)));
            return error;
        }
    }
}
