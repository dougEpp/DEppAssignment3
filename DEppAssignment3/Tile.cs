using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
    /// <summary>
    /// A class to model a tile in the 15-puzzle
    /// </summary>
    public class Tile : Button
    {
        private FifteenPuzzle game;
        private int row;

        public int Row
        {
            get { return row; }
            set { row = value; }
        }
        private int col;
        public int Col
        {
            get { return col; }
            set { col = value; }
        }
        /// <summary>
        /// Constructor for tiles not using a picture
        /// </summary>
        /// <param name="height">The height of the button</param>
        /// <param name="width">The width of the button</param>
        /// <param name="top">The tile's distance from the top of the form</param>
        /// <param name="left">The tile's distance from the left side of the form</param>
        /// <param name="text">The text to be displayed in the tile</param>
        /// <param name="row">The tile's row in the grid</param>
        /// <param name="col">The tile's column in the grid</param>
        /// <param name="game">The game form</param>
        public Tile(int height,
            int width,
            int top,
            int left,
            string text,
            int row,
            int col,
            FifteenPuzzle game)
        {
            this.Height = height;
            this.Width = width;
            this.Top = top;
            this.Left = left;
            this.Text = text;
            this.Click += Tile_Click;

            this.row = row;
            this.col = col;
            this.game = game;
        }
        /// <summary>
        /// Constructor for tiles using a picture
        /// </summary>
        /// <param name="height">The height of the button</param>
        /// <param name="width">The width of the button</param>
        /// <param name="top">The tile's distance from the top of the form</param>
        /// <param name="left">The tile's distance from the left side of the form</param>
        /// <param name="text">The text to be displayed in the tile</param>
        /// <param name="row">The tile's row in the grid</param>
        /// <param name="col">The tile's column in the grid</param>
        /// <param name="usePicture">Whether or not to use a picture</param>
        /// <param name="game">The game form</param>
        public Tile(int height,
            int width,
            int top,
            int left,
            string text,
            int row,
            int col,
            bool usePicture,
            FifteenPuzzle game)
        {
            this.Height = height;
            this.Width = width;
            this.Top = top;
            this.Left = left;
            this.Text = text;
            this.ForeColor = Color.Transparent;
            this.Click += Tile_Click;

            Bitmap b = new Bitmap(Properties.Resources.flowers);

            //get correct row and column based on square's number
            int x = (int.Parse(text) - 1) % game.Num_columns * 65;
            int y = int.Parse(text) / game.Num_columns * 65;

            //crop button's image to correct portion of original image
            Rectangle r = new Rectangle(x, y, 65, 65);
            Bitmap b1 = b.Clone(r, b.PixelFormat);
            this.Image = b1;

            this.row = row;
            this.col = col;
            this.game = game;

            //format tile's appearance
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
        /// <summary>
        /// Moves the clicked tile in the appropriate direction and checks if the game is won
        /// </summary>
        /// <param name="sender">The tile that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        public void Tile_Click(object sender, EventArgs e)
        {
            game.selectDirection(this);
            game.checkWin();
        }

    }
}
