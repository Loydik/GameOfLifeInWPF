using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLife_in_WPF
{
    
    public class CellControl : Shape
    {

        public static readonly DependencyProperty FillProperty;
        public object Content; 

        static CellControl() {
            PropertyMetadata fillPropertyMetaData = new PropertyMetadata(new SolidColorBrush(Colors.White));
            FillProperty = DependencyProperty.Register("FillProperty", typeof(SolidColorBrush), typeof(CellControl), fillPropertyMetaData);
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CellControl), new FrameworkPropertyMetadata(typeof(CellControl)));
        }

        public SolidColorBrush Fill
        {
            get;
            set; 
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return new RectangleGeometry(new Rect(0,0,25,25));
            }
        }

    }
}
