using Reversi.Cells;
using Reversi.DataStructures;
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
    public class GameViewModel
    {
        public ICell<GridViewModel> Game { get; set; }
        private Game currentGame { get; set; }
        private ICommand reset;

        public GameViewModel()
        {
            Game = Cell.Create<GridViewModel>( null);
            CreateNew();
            reset = new ResetCommand(this);
        }

        public ICommand ResetGame {
            get {
                return reset;
            }
        }
        public void CreateNew() {
            //Game = new GridViewModel(game);
            //I need to give the GridViewModel a List of RowViewModels
            //each RowViewModel needs a list of SquareViewModels
            //each one of that SquareViewModel has to have one of the game.Board Squares

            this.currentGame = Reversi.Domain.Game.CreateNew();

            //init rowviewmodels
            var rowViewModels = new List<RowViewModel>();
            for (int x = 0; x < this.currentGame.Board.Width; x++)
            {
                //init squareviewmodels
                var squareViewModels = new List<SquareViewModel>();
                for (int y = 0; y < this.currentGame.Board.Height; y++)
                {
                    //Console.WriteLine("Creating squareviewmodel for square at " + x + "," + y);
                    var square = this.currentGame.Board[new Vector2D(x, y)];

                    //Console.WriteLine("Trying to instantiate a new squareviewmodel with square at " + x + "," + y);
                    var svmTmp = new SquareViewModel(square);

                    //Console.WriteLine("Trying to add this squareviewmodel to a rowviewmodel");
                    squareViewModels.Add(svmTmp);
                }
                //RowViewModel for this row
                var rowViewModel = new RowViewModel(squareViewModels);

                //Adding current rows rowviewmodel to the rowviewmodels
                rowViewModels.Add(rowViewModel);
            }
            //Creating and finalizing
            var gvm = new GridViewModel(rowViewModels);
            
            this.Game.Value = gvm;
        }

        private Color ComputeOwnerColorValueFromOwner(Player player)
        {
            Brush val;
            if (player == Player.ONE) val = Brushes.Black;
            else val = Brushes.White;

            var solidBrush = val as SolidColorBrush;
            //Console.WriteLine(solidBrush);
            return solidBrush.Color;
        }

        private SolidColorBrush ComputeInvertedOwnerColor(Player player)
        {
            Brush val;
            if (player == Player.ONE) val = Brushes.White;
            else val = Brushes.Black;

            var solidBrush = val as SolidColorBrush;
            //Console.WriteLine(solidBrush);
            return solidBrush;
        }

        
        public ICell<Player> CurrentPlayer { get { return currentGame.CurrentPlayer; } }
        public ICell<int> Player1Score { get { return currentGame.StoneCount(Player.ONE); } }
        public ICell<int> Player2Score { get { return currentGame.StoneCount(Player.TWO); } }
        public ICell<Color> OwnerColor { get { return Cell.Derived(currentGame.CurrentPlayer, ComputeOwnerColorValueFromOwner); } }
        public ICell<SolidColorBrush> OwnerColorInverted { get { return Cell.Derived(currentGame.CurrentPlayer, ComputeInvertedOwnerColor); } }
    }

    public class ResetCommand : ICommand
    {
        private GameViewModel game;

        public ResetCommand(GameViewModel game)
        {
            this.game = game;
            //square.IsValidMove.PropertyChanged += IsValidMove_PropertyChanged;
            
        }

        void IsValidMove_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
        public bool CanExecute(object parameter)
        {
            //return square.IsValidMove.Value;

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //square.PlaceStone();
            game.CreateNew();
        }
    }
}
