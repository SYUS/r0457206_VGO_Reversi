using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiGUI
{
    //Keeps a List of SquareViewModels that each show the info of one square in a row
    public class RowViewModel
    {
        public IList<SquareViewModel> Row { get; set; }

        public RowViewModel(IList<SquareViewModel> row)
        {
            Row = row;
        }
    }
}
