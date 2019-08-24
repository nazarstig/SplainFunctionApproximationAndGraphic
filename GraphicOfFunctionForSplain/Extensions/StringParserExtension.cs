using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicOfFunctionForSplain.Extensions
{
    public static class StringParserExtension
    {
        public static double ParseToDouble(this string targetString)
        {
            if(!String.IsNullOrEmpty(targetString) &&
                !String.IsNullOrWhiteSpace(targetString))
            {
                double resultValue;
                Double.TryParse(targetString, out resultValue);
                return resultValue;
            }

            return 0;
        }
    }
}
