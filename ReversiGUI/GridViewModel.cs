using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGUI
{
    public class GridViewModel
    {
        public IList<RowViewModel> Grid { get; set; }

        public GridViewModel(IList<RowViewModel> grid)
        {
            Grid = grid;
        }
    }
}
