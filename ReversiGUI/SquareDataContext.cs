using Reversi.Cells;
using Reversi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ReversiGUI
{
    public class SquareDataContext
    {
        private readonly ICell<Color> ownerColorName;
        private readonly ICell<bool> isVisible;

        public SquareDataContext(ISquare square)
        {
            ownerColorName = Cell.Derived(square.Owner, square.IsValidMove, ComputeOwnerColorValueFromOwner);
            isVisible = Cell.Derived(square.Owner, square.IsValidMove, ComputeVisibilityFromColor);
            //Console.WriteLine("OwnerColorName: " + OwnerColorName);
        }

        private bool ComputeVisibilityFromColor(Player player, bool isValidMove)
        {
            if (player == null && !isValidMove) return false;
            else return true;
        }

        private Color ComputeOwnerColorValueFromOwner(Player player, bool isValidMove)
        {
            Brush val;
            if (player == Player.ONE) val = Brushes.Black;
            else if (player == Player.TWO) val = Brushes.White;
            else if (isValidMove)
                //val = Brushes.LightGreen;
                //val = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F2F2F2"));
                //val = Brushes.Transparent;
                val = Brushes.LightBlue;
            else
                val = Brushes.Transparent;

            var solidBrush = val as SolidColorBrush;
            //Console.WriteLine(solidBrush);
            return solidBrush.Color;
        }

        public ICell<Color> OwnerColorName { get { return ownerColorName; } }
        public ICell<bool> IsVisible { get { return isVisible; } }
    }
}
