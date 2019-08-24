using System;
using GraphicOfFunctionForSplain.Extensions;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace GraphicOfFunctionForSplain.ViewModels
{
    class MainViewModelInApp
    {
        private MainViewModel _viewModel;

        public MainViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public MainViewModelInApp()
        {
            _viewModel = new MainViewModel();
        }
    }
}
