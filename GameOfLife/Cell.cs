using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    [Serializable]
    public class Cell
    {
        #region Properties

        protected Boolean _alive;

        public virtual Boolean Alive
        {
            get { return _alive; }
            set
            {
                _alive = value;
            }
        }

        #endregion Properties

        #region Construction

        public Cell(Boolean alive)
        {
            this.Alive = alive;
        }

        #endregion Construction

        #region Methods

        public void setState(Boolean alive) {
            this.Alive = alive;
        }

        public static Boolean calculateNewState(int neighbors, Cell cell)
        {
            if (neighbors < 2 || neighbors > 3){
                return false;
            }
            else if ((cell.Alive == false && neighbors == 3) || (neighbors >= 2 && neighbors <= 3 && cell.Alive == true))
            {
                return true;
            }
            else {
                return false;
            }
        }

        #endregion Methods
    }
}
