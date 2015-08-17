using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SegeoMDL2Helper
{
    public class HexToCharValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = value as string;

            if (!string.IsNullOrEmpty(val))
            {
                val = val.Replace("&x", "");
                val = val.Replace(";", "");
                if (val.Length == 4)
                {
                    val = char.ConvertFromUtf32(int.Parse(val, NumberStyles.HexNumber));
                }
                else
                    throw new NotImplementedException();
            }

            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
