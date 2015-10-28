using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
    public partial class Form1 : Form
    {
        const int DEFAULT_NUM_COLUMNS = 4;
        const int DEFAULT_NUM_ROWS = 4;
        const int HEIGHT = 30;
        const int WIDTH = 30;
        const int TOP = 10;
        const int LEFT = 10;

        Tile[,] tiles;
        public Form1()
        {
            InitializeComponent();

            int num_rows = DEFAULT_NUM_ROWS;
            int num_columns = DEFAULT_NUM_COLUMNS;

            tiles = new Tile[num_rows, num_columns];

            int x; 
            int y = TOP;
            for (int i = 0; i < DEFAULT_NUM_ROWS; i++)
            {
                x = LEFT;
                for (int j = 0; j < DEFAULT_NUM_COLUMNS; j++)
                {
                    tiles[i,j] = new Tile(HEIGHT, WIDTH, y, x);
                    x += WIDTH;
                }
                y += HEIGHT;
            }
            foreach(Tile tile in tiles)
            {
                Controls.Add(tile);
            }
        }
    }
}
