using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using HST.Core.Utils;

namespace HST.Converters
{
    public class WeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var weight = double.Parse(value.ToString());
            var type = parameter.ToString();

            return HST.Core.Utils.WeightConverter.Convert(weight, type == "kg" ? WeightEnum.KG : WeightEnum.LBS) + " per side";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
