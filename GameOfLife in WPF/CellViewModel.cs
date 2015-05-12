using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife_in_WPF
{
    public class CellViewModel 
    {

        private ObservableCell _cell;
        private int _x, _y;

        public CellViewModel(int x, int y) {
            this.Cell = new ObservableCell(false);
            this.X = x;
            this.Y = y;
        }

        public ObservableCell Cell{ get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        
        
        
        

    }
}
