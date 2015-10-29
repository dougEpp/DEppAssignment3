using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
    public partial class FifteenPuzzle : Form
    {
        const int DEFAULT_NUM_COLUMNS = 4;
        const int DEFAULT_NUM_ROWS = 4;
        const int HEIGHT = 50;
        const int WIDTH = 50;
        const int TOP = 10;
        const int LEFT = 10;
        const int NUM_SCRAMBLE = 100;
        const int NUM_DIRECTIONS = 4;

        Tile[,] tiles;
        int num_rows;
        int num_columns;
        string winString;
        List<string> allMoves;
        public FifteenPuzzle()
        {
            InitializeComponent();

            num_rows = DEFAULT_NUM_ROWS;
            num_columns = DEFAULT_NUM_COLUMNS;

            tiles = new Tile[num_rows, num_columns];

            int x;
            int y = TOP;
            for (int i = 0; i < DEFAULT_NUM_ROWS; i++)
            {
                x = LEFT;
                for (int j = 0; j < DEFAULT_NUM_COLUMNS; j++)
                {
                    int num = num_rows * i + j + 1;
                    if (num != num_rows * num_columns)
                    {
                        tiles[i, j] = new Tile(HEIGHT, WIDTH, y, x, num.ToString(), i, j, this);
                        winString += num.ToString() + "_";
                    }
                    x += WIDTH;
                }
                y += HEIGHT;
            }
            foreach (Tile tile in tiles)
            {
                Controls.Add(tile);
            }
            allMoves = new List<string>();
            scramble(NUM_SCRAMBLE);
        }
        /// <summary>
        /// Selects the direction to move the clicked tile
        /// </summary>
        /// <param name="tile">the tile that has been clicked</param>
        public void selectDirection(Tile tile)
        {
            char direction;
            direction = ' ';
            Tile temp;
            temp = tile;

            if ((tile.Col != num_columns - 1) && (tiles[tile.Row, tile.Col + 1] == null))
            {
                direction = 'r';
                allMoves.Add("A");
            }
            else if ((tile.Col != 0) && (tiles[tile.Row, tile.Col - 1] == null))
            {
                direction = 'l';
                allMoves.Add("D");
            }
            else if ((tile.Row != num_rows - 1) && (tiles[tile.Row + 1, tile.Col] == null))
            {
                direction = 'd';
                allMoves.Add("W");
            }
            else if ((tile.Row != 0) && (tiles[tile.Row - 1, tile.Col] == null))
            {
                direction = 'u';
                allMoves.Add("S");
            }
            moveTile(tile, direction);
        }
        /// <summary>
        /// Moves the selected tile according to a key code
        /// </summary>
        /// <param name="keyCode">The specified key code</param>
        /// <param name="showMoves">Whether or not to pause between moves</param>
        /// <returns>Whether the move was successful</returns>
        private bool moveByButton(string keyCode, bool showMoves)
        {
            int emptyTileRow = findEmptyTile()[0];
            int emptyTileCol = findEmptyTile()[1];

            if(showMoves)
            {
                Thread.Sleep(100);
            }

            switch (keyCode)
            {
                case "A":
                    if (emptyTileCol != num_columns - 1)
                    {
                        moveTile(tiles[emptyTileRow, emptyTileCol + 1], 'l');
                        return true;
                    }
                    break;

                case "D":
                    if (emptyTileCol != 0)
                    {
                        moveTile(tiles[emptyTileRow, emptyTileCol - 1], 'r');
                        return true;
                    }
                    break;
                case "S":
                    if (emptyTileRow != 0)
                    {
                        moveTile(tiles[emptyTileRow - 1, emptyTileCol], 'd');
                        return true;
                    }
                    break;
                case "W":
                    if (emptyTileRow != num_rows - 1)
                    {
                        moveTile(tiles[emptyTileRow + 1, emptyTileCol], 'u');
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        /// <summary>
        /// Moves the selected tile in the specified direction
        /// </summary>
        /// <param name="tile">The selected tile</param>
        /// <param name="direction">The specified direction</param>
        private void moveTile(Tile tile, char direction)
        {
            switch (direction)
            {
                case 'l':
                    tiles[tile.Row, tile.Col - 1] = tile;
                    tiles[tile.Row, tile.Col] = null;
                    tile.Col--;
                    tile.Left -= tile.Width;
                    break;
                case 'r':
                    tiles[tile.Row, tile.Col + 1] = tile;
                    tiles[tile.Row, tile.Col] = null;
                    tile.Col++;
                    tile.Left += tile.Width;
                    break;
                case 'u':
                    tiles[tile.Row - 1, tile.Col] = tile;
                    tiles[tile.Row, tile.Col] = null;
                    tile.Row--;
                    tile.Top -= tile.Height;
                    break;
                case 'd':
                    tiles[tile.Row + 1, tile.Col] = tile;
                    tiles[tile.Row, tile.Col] = null;
                    tile.Row++;
                    tile.Top += tile.Height;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// scrambles the tiles a specified number of times
        /// </summary>
        /// <param name="num">the number of times to scramble the tiles</param>
        public void scramble(int num)
        {
            Random rand = new Random();
            for (int i = 0; i < num; i++)
            {
                int val = rand.Next(NUM_DIRECTIONS);
                switch (val)
                {
                    case 0:
                        if (moveByButton("A", false))
                        {
                            allMoves.Add("D");
                        }
                        break;
                    case 1:
                        if (moveByButton("S", false))
                        {
                            allMoves.Add("W");
                        }
                        break;
                    case 2:
                        if (moveByButton("D", false))
                        {
                            allMoves.Add("A");
                        }
                        break;
                    case 3:
                        if (moveByButton("W", false))
                        {
                            allMoves.Add("S");
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Calls the check method to see if the game has been solved
        /// </summary>
        /// <param name="sender">The button pressed</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            checkWin();
        }
        private int[] findEmptyTile()
        {
            int[] position = new int[2];
            for (int i = 0; i < num_rows; i++)
            {
                for (int j = 0; j < num_columns; j++)
                {
                    if (tiles[i, j] == null)
                    {
                        position[0] = i;
                        position[1] = j;
                    }
                }
            }
            return position;
        }
        /// <summary>
        /// Checks if the game has been solved
        /// </summary>
        /// <returns>True if the game has been solved</returns>
        public bool checkWin()
        {
            bool result = false;
            string order = "";
            foreach (var item in tiles)
            {
                if (item != null)
                {
                    order += item.Text + "_";
                }
            }
            if (order == winString && tiles[num_rows - 1, num_columns - 1] == null)
            {
                MessageBox.Show("You Win!");
                result = true;
            }
            return result;
        }
        /// <summary>
        /// solves the puzzle based on recorded moves
        /// </summary>
        private void solvePuzzle()
        {
            allMoves.Reverse();
            foreach (var direction in allMoves)
            {
                moveByButton(direction, true);
            }
            allMoves = new List<string>();
        }
        /// <summary>
        /// Moves the appropriate tile based on a key press
        /// </summary>
        /// <param name="sender">The key that was pressed</param>
        /// <param name="e">Event arguments for the key press event</param>
        private void FifteenPuzzle_KeyDown(object sender, KeyEventArgs e)
        {
            string key = e.KeyCode.ToString();
            if (moveByButton(key, false))
            {
                switch(key)
                {
                    case "A":
                        allMoves.Add("D");
                        break;
                    case "S":
                        allMoves.Add("W");
                        break;
                    case "D":
                        allMoves.Add("A");
                        break;
                    case "W":
                        allMoves.Add("S");
                        break;
                    default: break;
                }
            }
            checkWin();
        }
        /// <summary>
        /// Solves the puzzle
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void Solve_Click(object sender, EventArgs e)
        {
            solvePuzzle();
            checkWin();
        }
        /// <summary>
        /// Scrambles the tiles
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnScramble_Click(object sender, EventArgs e)
        {
            scramble(NUM_SCRAMBLE);
        }
    }
}
