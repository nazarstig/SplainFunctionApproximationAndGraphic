using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Differentiation;
namespace GraphicOfFunctionForSplain
{
    delegate double Function(double x);
    //public  class CubicHermiteSplain
    // {


    //     private double _leftEnd;
    //     private double _rightEnd;
    //     private double _leftOutcutValue;
    //     private double _rightOutcutValue;
    //     private double _leftOutcutDerivationValue;
    //     private double _rightOutcutDerivationValue;
    //     private double _h;
    //     //private double _resultVector;
    //     private int _degree = 3;
    //     private int _systemOrder = 4;
    //     private Matrix<double> _A;
    //     private Vector<double> _B;
    //     private Vector<double> _resultVector;
    //     private Func<double, double> _function;
    //     private Func<double, double> _derivativeFunction;


    //     public double H
    //     {
    //         get { return _h; }
    //         set { _h = value; }
    //     }

    //     public Matrix<double> A
    //     {
    //         get { return _A; }
    //         set { _A = value; }
    //     }

    //     public Vector<double> B
    //     {
    //         get { return _B; }
    //         set { _B = value; }
    //     }

    //     public Vector<double> ResultVector
    //     {
    //         get { return _resultVector; }
    //         set { _resultVector = value; }
    //     }

    //     public CubicHermiteSplain(Func<double, double> function, double leftEnd, double rightEnd)
    //     {
    //         _function = function;

    //         // Console.WriteLine("7: " + _function(7));
    //         _derivativeFunction = ExponentialHermiteSplain.FindDerivative(_function);
    //         //  Console.WriteLine("der7: " + _derivativeFunction(7));
    //         _leftEnd = leftEnd;
    //         _rightEnd = rightEnd;
    //         _leftOutcutValue = _function(_leftEnd);
    //         _rightOutcutValue = _function(_rightEnd);
    //         _leftOutcutDerivationValue = _derivativeFunction(_leftEnd);
    //         _rightOutcutDerivationValue = _derivativeFunction(_rightEnd);
    //         // ConstForEquationCount();
    //         _h = HCount();
    //         _A = FormMatrixA();
    //         _B = FormVectorB();
    //         _resultVector = FormResultVector(_A, _B);
    //     }


    //     private Matrix<double> FormMatrixA()
    //     {
    //         Matrix<double> A = Matrix<double>.Build.DenseIdentity(_systemOrder);
    //         A.SetRow(0, FormCubicPolynomialMatrixRow(_degree, _leftEnd));
    //         A.SetRow(1, FormCubicPolynomialDerivationMatrixRow(_degree, _leftEnd));
    //         A.SetRow(2, FormCubicPolynomialMatrixRow(_degree, _rightEnd));
    //         A.SetRow(3, FormCubicPolynomialDerivationMatrixRow(_degree, _rightEnd));
    //         return A;

    //     }

    //     public double HCount()
    //     {
    //         if (_rightEnd > _leftEnd)
    //             return _rightEnd - _leftEnd;
    //         else return 0;
    //     }

    //     private Vector<double> FormVectorB()
    //     {
    //         Vector<double> B = Vector<double>.Build.Dense(_systemOrder);
    //         B[0] = (_leftOutcutValue);
    //         B[1] = (_leftOutcutDerivationValue);
    //         B[2] = (_rightOutcutValue);
    //         B[3] = (_rightOutcutDerivationValue);

    //         return B;
    //     }

    //     private Vector<double> FormResultVector(Matrix<double> A, Vector<double> B)
    //     {
    //         Vector<double> ResultVector = A.Inverse() * B;
    //         return ResultVector;
    //     }

    //     public Vector<double> FormCubicPolynomialMatrixRow(int degree, double point)
    //     {
    //         List<double> matrixRow = new List<double>();
    //         for(int i = 0; i < degree; ++i)
    //         {
    //             matrixRow.Add(Math.Pow(point, degree - i));
    //         }
    //         matrixRow.Add(1);
    //         Vector<double> vector = CreateVector.DenseOfEnumerable(matrixRow);

    //         return vector;
    //     }

    //     public Vector<double> FormCubicPolynomialDerivationMatrixRow(int degree, double point)
    //     {
    //         List<double> matrixRow = new List<double>();
    //         for (int i = 0; i < degree; ++i)
    //         {
    //             matrixRow.Add((degree - i) * (Math.Pow(point, degree - i - 1)));
    //         }
    //         matrixRow.Add(0);
    //         Vector<double> vector = CreateVector.DenseOfEnumerable(matrixRow);

    //         return vector;
    //     }

    //     public Func<double, double> CubicPolynomial()
    //     {
    //         Func<double, double> polynomial;
    //         polynomial = (x) => {
    //             return ResultVector[0] * Math.Pow(x, 3) + ResultVector[1] * Math.Pow(x, 2) +
    //                    ResultVector[2] * Math.Pow(x, 1) + ResultVector[3] * Math.Pow(x, 0);
    //         };
    //         return polynomial;
    //     }

    //     public string AllData()
    //     {
    //         string data;
    //         data = "A = " + Math.Round(ResultVector[0], 4).ToString() + 
    //             " B = " + Math.Round(ResultVector[1], 4).ToString() +
    //         "  C = " + Math.Round(ResultVector[2], 4).ToString() +
    //        " D = " + Math.Round(ResultVector[3], 4).ToString();
    //     //    " H = " + H.ToString();

    //         return data;
    //     }

    // }

   public class CubicHermiteSplain
    {


        private double _leftEnd;
        private double _rightEnd;
        private double _leftOutcutValue;
        private double _rightOutcutValue;
        private double _leftOutcutDerivationValue;
        private double _rightOutcutDerivationValue;
        private double _h;
        //private double _resultVector;
        private int _degree = 3;
        private int _systemOrder = 4;
        private double _a;
        private double _b;
        private double _c;
        private double _d;

        private Func<double, double> _function;
        private Func<double, double> _derivativeFunction;




        public double A
        {
            get { return _a; }
            set { _a = value; }
        }
        public double B
        {
            get { return _b; }
            set { _b = value; }
        }
        public double C
        {
            get { return _c; }
            set { _c = value; }
        }
        public double D
        {
            get { return _d; }
            set { _d = value; }
        }

        public double CountA()
        {
            return (((_rightOutcutValue - _leftOutcutValue - _leftOutcutDerivationValue * (_rightEnd - _leftEnd)) * 2 * (_leftEnd - _rightEnd) - (_leftOutcutDerivationValue - _rightOutcutDerivationValue) * (Math.Pow(_rightEnd, 2) - Math.Pow(_leftEnd, 2) - 2 * _leftEnd * (_rightEnd - _leftEnd))) /
                ((Math.Pow(_rightEnd, 3) - Math.Pow(_leftEnd, 3) - 3 * Math.Pow(_leftEnd, 2) * (_rightEnd - _leftEnd)) * 2 * (_leftEnd - _rightEnd) - 3 * (Math.Pow(_leftEnd, 2) - Math.Pow(_rightEnd, 2)) * (Math.Pow(_rightEnd, 2) - Math.Pow(_leftEnd, 2) - 2 * _leftEnd * (_rightEnd - _leftEnd))));
        }

        public double CountB()
        {
            return ((_leftOutcutDerivationValue - _rightOutcutDerivationValue - 3 * A * (Math.Pow(_leftEnd, 2) - Math.Pow(_rightEnd, 2))) / (2 * (_leftEnd - _rightEnd)));
        }

        public double CountC()
        {
            return (_leftOutcutDerivationValue - 3 * A * Math.Pow(_leftEnd, 2) - 2 * B * _leftEnd);
        }

        public double CountD()
        {
            return (_leftOutcutValue - A * Math.Pow(_leftEnd, 3) - B * Math.Pow(_leftEnd, 2) - C * _leftEnd);
        }

        public void CountCoefficients()
        {
            A = CountA();
            B = CountB();
            C = CountC();
            D = CountD();
        }



        public CubicHermiteSplain(Func<double, double> function, double leftEnd, double rightEnd)
        {
            _function = function;
            _derivativeFunction = ExponentialHermiteSplain.FindDerivative(_function);
            _leftEnd = leftEnd;
            _rightEnd = rightEnd;
            _leftOutcutValue = _function(_leftEnd);
            _rightOutcutValue = _function(_rightEnd);
            _leftOutcutDerivationValue = _derivativeFunction(_leftEnd);
            _rightOutcutDerivationValue = _derivativeFunction(_rightEnd);
            CountCoefficients();
        }

        public Func<double, double> CubicPolynomial()
        {
            Func<double, double> polynomial;
            polynomial = (x) => {
                return A * Math.Pow(x, 3) + B * Math.Pow(x, 2) +
                       C * Math.Pow(x, 1) + D * Math.Pow(x, 0);
            };
            return polynomial;
        }


        public string AllData()
        {
            string data;
            data = "A = " + Math.Round(A, 4).ToString() +
                " B = " + Math.Round(B, 4).ToString() +
            "  C = " + Math.Round(C, 4).ToString() +
           " D = " + Math.Round(D, 4).ToString();

            return data;
        }
    }

}
