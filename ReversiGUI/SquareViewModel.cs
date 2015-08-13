using Reversi.Cells;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace ReversiGUI
{
    //Shows info about a Square
    public class SquareViewModel
    {
        private readonly ISquare square;
        private readonly SquareDataContext squareData;
        private ICommand click;
       
        public SquareViewModel(ISquare square)
        {
            this.square = square;
            squareData = new SquareDataContext(square);
            click = new ClickCommand(square);
        }

        public ICell<Player> Owner { get { return square.Owner; } }
        //public ICell<Brush> OwnerColorHex { get { return squareData.OwnerColor; } }
        //public ICell<Color> getOwnerColor { get { return squareData.OwnerColorName; } }
        public ICell<Color> OwnerColor { get { return squareData.OwnerColorName; } }
        public ICell<bool> IsVisible { get { return squareData.IsVisible; } }
        public ICommand PlaceStone { get { return click; } }
    }
   public class ClickCommand: ICommand {
       private ISquare square;
       public ClickCommand(ISquare square) {
           this.square = square;
           square.IsValidMove.PropertyChanged += IsValidMove_PropertyChanged;
       }

       void IsValidMove_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
       {
           if (CanExecuteChanged != null)
               CanExecuteChanged(this, new EventArgs());
       }
       public bool CanExecute(object parameter)
       {
           return square.IsValidMove.Value;
       }

       public event EventHandler CanExecuteChanged;

       public void Execute(object parameter)
       {
           square.PlaceStone();
       }
   }
}
