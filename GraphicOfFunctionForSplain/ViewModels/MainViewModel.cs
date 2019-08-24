using System;
using GraphicOfFunctionForSplain.Extensions;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using GraphicOfFunctionForSplain.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GraphicOfFunctionForSplain;
namespace GraphicOfFunctionForSplain.ViewModels
{
   
    public enum FunctionList { first = 1,
                               second = 2,
                               third = 3,
                               fourth = 4,
                               fifth = 5,
                               sixth = 6,
                               seventh = 7 };
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Private fields

        //
        private ObservableCollection<TabulationFunctionTableItem> _tableItems = new ObservableCollection<TabulationFunctionTableItem>();
        private Func<double, double> _func = (x) => { return Math.Sin(x); };
        private string _leftFunctionLimit;
        private string _rightFunctionLimit;
        private string _expInform ;
        private string _cubicInform;
        private FunctionList _functionListEnum = FunctionList.third;
        private double _maxErrorExp;
        private double _maxErrorCubic;
        private string _maxErrorExpMessage;
        private string _maxErrorCubicMessage;
        private FunctionTabulation _funcTabulation;
        private FunctionTabulation _expTabulation;
        private FunctionTabulation _cubicTabulation;
        private double _step = 0.01;
        private double _precision = 0.000001;
        private double _theoreticalErrorExponential = 2;
        private double _theoreticalErrorCubic = 3;
        private ExponentialHermiteSplain expSplain;
        private CubicHermiteSplain cubSplain;
        private ErrorCounter _errorCounter;
        public event PropertyChangedEventHandler PropertyChanged;


        public double TheoreticalErrorExponential
        {
            get { return _theoreticalErrorExponential; }
            set { _theoreticalErrorExponential = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TheoreticalErrorExponential)));
            }
        }

        public double TheoreticalErrorCubic
        {
            get { return _theoreticalErrorCubic; }
            set { _theoreticalErrorCubic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TheoreticalErrorCubic)));
            }
        }
        public double Step
        {
            get { return _step; }
            set { _step = value; }
        }

        public FunctionTabulation FuncTabulation
        {
            get { return _funcTabulation; }
            set { _funcTabulation = value; }
        }

        public FunctionTabulation ExpTabulation
        {
            get { return _funcTabulation; }
            set { _funcTabulation = value; }
        }

        public FunctionTabulation CubicTabulation
        {
            get { return _funcTabulation; }
            set { _funcTabulation = value; }
        }





        public enum FunctionList { first, second, third, fourth, fifth, sixth, seventh };
        #endregion

        #region Public properties


        public FunctionList FunctionListEnum
        {
            get { return _functionListEnum; }
            set {
                _functionListEnum = value;
                _func = ChangeFunc();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionListEnum)));
                RefreshPlots(_func, _leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());
                
            }
        }



        private Func<double, double> ChangeFunc()
        {
            
            
            switch (FunctionListEnum)
            {
                case FunctionList.first: { return (x) => { return Math.Log(x); }; };
                case FunctionList.second: { return (x) => { return (1 / (1 + x * x)); }; }
                case FunctionList.third: { return  (x) => { return Math.Sin(x); }; };
                case FunctionList.fourth: { return  (x) => { return Math.Tan(x); };  };
                case FunctionList.fifth: { return  (x) => { return Math.Atan(x); };  };
                case FunctionList.sixth: { return  (x) => { return Math.Sinh(x); }; };
                case FunctionList.seventh: { return  (x) => { return (4 * x - 2 + 3 * Math.Exp(-5 * x)); }; };
                default: { break;  }  //TODO handle error
            }
            return null;
          
          
        }

        public ObservableCollection<TabulationFunctionTableItem> TableItems
        {
            get { return _tableItems; }
            set
            {
                _tableItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TableItems)));
            }
        }

        public string ExpInform
        {
            get { return _expInform; }
            set
            {
                _expInform = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExpInform)));
            }
        }

        public string CubicInform
        {
            get { return _cubicInform; }
            set
            {
                _cubicInform = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CubicInform)));
            }
        }

        public string MaxErrorExpMessage
        {
            get { return _maxErrorExpMessage; }
            set
            {
                _maxErrorExpMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxErrorExpMessage)));
            }
        }

        public string MaxErrorCubicMessage
        {
            get { return _maxErrorCubicMessage; }
            set
            {
                _maxErrorCubicMessage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxErrorCubicMessage)));
            }
        }

        public double MaxErrorExp
        {
            get { return _maxErrorExp; }
            set
            {
                _maxErrorExp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxErrorExp)));
            }
        }

        public double MaxErrorCubic
        {
            get { return _maxErrorCubic; }
            set
            {
                _maxErrorCubic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxErrorCubic)));
            }
        }

       public PlotModel MyModel1 { get; set; }

       public PlotModel MyModel2 { get; set; }

       public PlotModel MyModel3 { get; set; }

       public PlotModel MyModel4 { get; set; }



        public Func<double, double> ExpApproximation { get; set; }

        
       
       public Func<double, double> CubicApproximation { get; set; }

 
       public string LeftFunctionLimit
        {
            get { return _leftFunctionLimit; }
            set
            {
                _leftFunctionLimit = value;
                if (_rightFunctionLimit != null)
                {
               
                    RefreshPlots(_func, _leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());

                }

            }
        }

        public ObservableCollection<TabulationFunctionTableItem> RefreshTable(Func<double, double> func, FunctionTabulation expTabulation, FunctionTabulation cubicTabulation)
        {
            ObservableCollection<TabulationFunctionTableItem> items = new ObservableCollection<TabulationFunctionTableItem>();
            for (int i = 0; i < expTabulation.Y.Count; i++)
                items.Add(new TabulationFunctionTableItem(expTabulation.X[i], func(expTabulation.X[i]), expTabulation.Y[i],
                    cubicTabulation.Y[i], Math.Abs(func(expTabulation.X[i]) - expTabulation.Y[i]), Math.Abs(func(cubicTabulation.X[i]) - cubicTabulation.Y[i])));
            return items;
        }

        public string RightFunctionLimit
        {
            get { return _rightFunctionLimit; }
            set
            {
                _rightFunctionLimit = value;
                if (_leftFunctionLimit != null)
                {
               
                    RefreshPlots(_func, _leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());   
                }
               
            }
        }

        #endregion

        public MainViewModel()
        {
            MyModel1 = new PlotModel { Title = "Графік функції і експоненціальної ланки" };
            MyModel2 = new PlotModel { Title = "Графік похибки наближення функції експоненціальною ланкою" };
            MyModel3 = new PlotModel { Title = "Графік функції і кубічної ланки" };
            MyModel4 = new PlotModel { Title = "Графік похибки наближення функції кубічною ланкою" };
           
        }

        private bool CheckLimits(double leftLimit, double rightLimit)
        {
            return leftLimit < rightLimit;
        }

        private List<FunctionSeries> CreateFunctionSeries(List<Func<double, double>> funcs, double leftLimit, 
            double rightLimit, List<string> functionNames)
        {
            List<FunctionSeries> series = new List<FunctionSeries>();
            for(int i = 0; i < funcs.Count; ++i)
            {
                series.Add(new FunctionSeries(funcs[i], leftLimit, rightLimit, 0.1, functionNames[i]));
            }
            return series;
        }

        private void RefreshPlots(Func<double, double> func, double leftLimit, double rightLimit)
        {
            if (CheckLimits(leftLimit, rightLimit))
            {
                RefreshData(func, leftLimit, rightLimit);
                RefreshPlot(MyModel1, new List<Func<double, double>> { func, ExpApproximation }, leftLimit, rightLimit,
                    new List<string> { "Функція", "Експоненціальна ланка" });
                RefreshPlot(MyModel2, new List<Func<double, double>> { (x) => { return Math.Abs(func(x) - ExpApproximation(x)); } }, leftLimit, rightLimit,
                   new List<string> { "Різниця між експоненціальною ланкою і функцією" });
                RefreshPlot(MyModel3, new List<Func<double, double>> { func, CubicApproximation }, leftLimit, rightLimit,
                    new List<string> { "Функція", "Кубічна ланка" });
                RefreshPlot(MyModel4, new List<Func<double, double>> { (x) => { return Math.Abs(func(x) - CubicApproximation(x)); } }, leftLimit, rightLimit,
                   new List<string> { "Різниця між кубічною ланкою і функцією" });
            }
        }

        private void RefreshPlot(PlotModel plotModel, List<Func<double, double>> funcs, double leftLimit,
            double rightLimit, List<string> functionNames)
        {
            List<FunctionSeries> series = CreateFunctionSeries(funcs, leftLimit, rightLimit, functionNames);
            DrawPlot(plotModel, series);
        }

        private void DrawPlot(PlotModel plotModel, List<FunctionSeries> functionSeries)
        {
            plotModel.ResetAllAxes();
            plotModel.Series.Clear();

            foreach (FunctionSeries functionSeria in functionSeries)
            {
                plotModel.Series.Add(functionSeria);
            }
            plotModel.InvalidatePlot(true);
        }

        private void RefreshData(Func<double, double> func, double leftLimit, double rightLimit)
        {
            ExponentialHermiteSplain expApproximation = new ExponentialHermiteSplain(func, leftLimit, rightLimit);
            CubicHermiteSplain cubicApproximation = new CubicHermiteSplain(func, leftLimit, rightLimit);
            ExpApproximation = expApproximation.Polynomial();
            ExpInform = expApproximation.AllData();

            CubicApproximation = cubicApproximation.CubicPolynomial();
            //  FunctionTabulation tabulation = new FunctionTabulation(approximation.Polynomial(), 0.1, leftLimit, rightLimit);
            FunctionTabulation ExpTabulation = new FunctionTabulation(expApproximation.Polynomial(), Step, leftLimit, rightLimit);
            FunctionTabulation CubicTabulation = new FunctionTabulation(cubicApproximation.CubicPolynomial(), Step, leftLimit, rightLimit);
            ExpInform = expApproximation.AllData();
            CubicInform = cubicApproximation.AllData();
            MaxErrorExp = ExpTabulation.FindMaxError(func);
            MaxErrorCubic = CubicTabulation.FindMaxError(func);
            MaxErrorExpMessage = "Похибка :" + MaxErrorExp.ToString();
            MaxErrorCubicMessage = "Похибка :" + MaxErrorCubic.ToString();
            TableItems = RefreshTable(func, ExpTabulation, CubicTabulation);
            //try
            //{
            //    ErrorCounter exp = new ErrorCounter(_leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble(), _func, _precision, 1, 1);
            //    ErrorCounter cubic = new ErrorCounter(_leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble(), _func, _precision, 2, 1); ;
            //    TheoreticalErrorExponential = exp.Error;
            //    TheoreticalErrorCubic = cubic.Error;
            //}
            //catch(Exception)
            //{

            //}
        }
        //old

        //private void DrawPlot(double leftLimit, double rightLimit)
        //{
        //    if (CheckLimits(leftLimit, rightLimit))
        //    {

        //        ExponentialHermiteSplain expApproximation = new ExponentialHermiteSplain(_func, _leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());
        //        CubicHermiteSplain cubicApproximation = new CubicHermiteSplain(_func, _leftFunctionLimit.ParseToDouble(), _rightFunctionLimit.ParseToDouble());

        //        //  FunctionTabulation tabulation = new FunctionTabulation(approximation.Polynomial(), 0.1, leftLimit, rightLimit);
        //        FunctionTabulation ExpTabulation = new FunctionTabulation(expApproximation.Polynomial(), Step, leftLimit, rightLimit);
        //        FunctionTabulation CubicTabulation = new FunctionTabulation(cubicApproximation.CubicPolynomial(), Step, leftLimit, rightLimit);
        //        ExpInform = expApproximation.AllData();
        //        CubicInform = cubicApproximation.AllData();
        //        MaxErrorExp = ExpTabulation.FindMaxError(_func);
        //        MaxErrorCubic = CubicTabulation.FindMaxError(_func);
        //        MaxErrorExpMessage = "Похибка :" + MaxErrorExp.ToString();
        //        MaxErrorCubicMessage = "Похибка :" + MaxErrorCubic.ToString();
        //        TableItems = RefreshTable(_func, ExpTabulation, CubicTabulation);
        //        MyModel1.Series.Clear();
        //        MyModel1.ResetAllAxes();

        //        MyModel1.Series.Add(new FunctionSeries(_func, leftLimit, rightLimit, Step, "функція"));
        //        MyModel1.Series.Add(new FunctionSeries(expApproximation.Polynomial(), leftLimit, rightLimit, Step, "експоненціальна ланка"));
        //        MyModel1.InvalidatePlot(true);

        //        MyModel2.ResetAllAxes();
        //        MyModel2.Series.Clear();

        //        MyModel2.Series.Add(new FunctionSeries((x) => { return Math.Abs((_func(x) - expApproximation.Polynomial()(x))); }, leftLimit, rightLimit, Step, "похибка функції"));
        //        MyModel2.InvalidatePlot(true);

        //        MyModel3.ResetAllAxes();
        //        MyModel3.Series.Clear();

        //        MyModel3.Series.Add(new FunctionSeries(_func, leftLimit, rightLimit, Step, "функція"));
        //        MyModel3.Series.Add(new FunctionSeries(cubicApproximation.CubicPolynomial(), leftLimit, rightLimit, 0.05, "кубічна ланка"));


        //        MyModel3.InvalidatePlot(true);

        //        MyModel4.ResetAllAxes();
        //        MyModel4.Series.Clear();

        //        MyModel4.Series.Add(new FunctionSeries((x) => { return Math.Abs((_func(x) - cubicApproximation.CubicPolynomial()(x))); }, leftLimit, rightLimit, Step, "функція"));

        //        MyModel4.InvalidatePlot(true);
        //  }
        //  }
    }
}
