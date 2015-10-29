using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
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

        public void Tile_Click(object sender, EventArgs e)
        {
            game.selectDirection(this);
        }

    }
}
