using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicOfFunctionForSplain
{
    class IntegralCounter
    {
        double _leftLimit;
        double _rightLimit;
        double _step;
        double _precision;
        double _stepAmount = 1;
        Func<double, double> _func;
        double _integralValue;


        public double IntegralValue
        {
            get { return _integralValue; }
            set { _integralValue = value; }
        }

     
        public double LeftLimit
        {
            get { return _leftLimit; }
            set { _leftLimit = value; }
        }

        public double RightLimit
        {
            get { return _rightLimit; }
            set { _rightLimit = value; }
        }

        public double Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }

        public Func<double, double> Func
        {
            get { return _func; }
            set { _func = value; }
        }

     

        public IntegralCounter(double leftLimit, double rightLimit, Func<double, double> func, double precision)
        {
            LeftLimit = leftLimit;
            RightLimit = rightLimit;
            Func = func;
            Precision = precision;
            Step = CountStep();
            IntegralValue = CountIntegral(LeftLimit, RightLimit, Func, Precision);
            // Error = ErrorCount(IntegralValue, ParametersAmount, _r);
        }

             
        public double CountStep()
        {
            return (RightLimit - LeftLimit) / _stepAmount;
        }

        public double Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public double MiddleRectanglesFormula(double point, Func<double, double> func)
        {
            return (((func(point) + func(point + Step)) / 2) * Step);
        }

        public double CountSquare(double leftLimit, double rightLimit, Func<double, double> func, double step)
        {
            double point;
            double square = 0;
            for (point = leftLimit; point < rightLimit; point += Step)
            {
                square += MiddleRectanglesFormula(point, func);
            }
            return square;
        }

        public double CountIntegral(double leftLimit, double rightLimit, Func<double, double> func, double precision)
        {
            double firstSquare;
            double secondSquare;
            int i = 0;
            do
            {
                firstSquare = CountSquare(leftLimit, rightLimit, func, Step);
                Step /= 2;
                secondSquare = CountSquare(leftLimit, rightLimit, func, Step);
                ++i;
                if (i > 100)
                {
                    Console.WriteLine("Too much iterations");
                    break;

                }
            }
            while (!RungeCheck(firstSquare, secondSquare));

            return secondSquare;
        }

        public bool RungeCheck(double firstSquare, double secondSquare)
        {
            if ((Math.Abs((secondSquare - firstSquare) / 3)) < Precision)
                return true;
            else
                return false;
        }

       

       
    }
}
