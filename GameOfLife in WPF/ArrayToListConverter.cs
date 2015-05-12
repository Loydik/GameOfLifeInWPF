using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GameOfLife_in_WPF
{
    [ValueConversion(/*sourceType*/ typeof(CellViewModel[,]), /*targetType*/ typeof(ObservableCollection<ObservableCollection<CellViewModel>>))]

    public class ArrayToListConverter : IValueConverter
    {
    
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<ObservableCollection<CellViewModel>> lsts = new ObservableCollection<ObservableCollection<CellViewModel>>();

            CellViewModel[,] cells = (CellViewModel[,])value;

            for (int i = 0; i < 20; i++)
            {
                lsts.Add(new ObservableCollection<CellViewModel>());

                for (int j = 0; j < 20; j++)
                {
                    lsts[i].Add(cells[i,j]);
                }
            }

            return lsts;

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 	        throw new NotImplementedException();
        }
    }
}
