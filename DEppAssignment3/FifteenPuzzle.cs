﻿/* DEppAssignment3
 * FifteenPuzzle.cs
 * A simple 15-puzzle game, including scrambling, solving, saving and loading
 * 
 * 2015-10-16 : Created game form and tile class
 *               - Doug Epp
 * 2015-10-28 : Added tile movement and win checking
 *              Added hotkeys, scrambling and solving
 *               - Doug Epp
 * 2015-10-29 : Fixed bugs and added comments
 *              Added saving and loading
 *              Added comments and cleaned up code
 *               - Doug Epp
 * 2015-10-30 : Added custom row and columns
 *              Added the ability to play with a picture
 *               - Doug Epp
 * 2015-10-31 : Fixed bugs, tweaked comments
 *              Finished game
 *               - Doug Epp
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEppAssignment3
{
    /// <summary>
    /// A class to model a fifteen-puzzle game
    /// </summary>
    public partial class FifteenPuzzle : Form
    {
        //Global constants
        const int DEFAULT_NUM_COLUMNS = 4;
        const int DEFAULT_NUM_ROWS = 4;
        const int MAX_ROWS = 7;
        const int MIN_COLUMNS = 2;
        const int MIN_ROWS = 2;
        const int MAX_COLUMNS = 7;
        const int GAME_HEIGHT = 430;
        const int GAME_WIDTH = 430;
        const int TOP = 75;
        const int LEFT = 10;
        const int SCRAMBLE_FACTOR = 5;
        const int NUM_DIRECTIONS = 4;

        //Global variable declaration
        Tile[,] tiles;
        int num_rows;
        int num_columns;
        public int Num_rows
        {
            get { return num_rows; }
            set { num_rows = value; }
        }
        public int Num_columns
        {
            get { return num_columns; }
            set { num_columns = value; }
        }
        string winString;
        List<char> allMoves;
        bool usingPicture;

        /// <summary>
        /// Initializes the game with a new, scrambled grid of tiles
        /// </summary>
        public FifteenPuzzle()
        {
            InitializeComponent();

            num_rows = DEFAULT_NUM_ROWS;
            num_columns = DEFAULT_NUM_COLUMNS;

            tiles = new Tile[num_rows, num_columns];

            string defaultOrder = "";
            for (int i = 1; i < num_rows * num_columns; i++)
            {
                defaultOrder += i.ToString() + "_";
            }
            defaultOrder += "-1";
            usingPicture = false;
            generateGrid(num_rows, num_columns, defaultOrder);

            scramble();
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
                allMoves.Add('A');
            }
            else if ((tile.Col != 0) && (tiles[tile.Row, tile.Col - 1] == null))
            {
                direction = 'l';
                allMoves.Add('D');
            }
            else if ((tile.Row != num_rows - 1) && (tiles[tile.Row + 1, tile.Col] == null))
            {
                direction = 'd';
                allMoves.Add('W');
            }
            else if ((tile.Row != 0) && (tiles[tile.Row - 1, tile.Col] == null))
            {
                direction = 'u';
                allMoves.Add('S');
            }
            moveTile(tile, direction);
        }
        /// <summary>
        /// Moves the selected tile according to a key code
        /// </summary>
        /// <param name="direction">The specified key code</param>
        /// <param name="showMoves">Whether or not to pause between moves</param>
        /// <returns>Whether the move was successful</returns>
        private bool moveByDirection(string direction, bool showMoves)
        {
            int emptyTileRow = findEmptyTile()[0];
            int emptyTileCol = findEmptyTile()[1];

            if (showMoves)
            {
                Thread.Sleep(100);
            }

            switch (direction)
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
        /// Finds the position of the empty tile
        /// </summary>
        /// <returns>The row and column of the empty tile</returns>
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
        /// Calls the check method to see if the game has been solved
        /// </summary>
        /// <param name="sender">The button pressed</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (!checkWin())
            {
                MessageBox.Show("Not solved yet");
            }
        }
        /// <summary>
        /// Checks if the game has been solved
        /// </summary>
        /// <returns>True if the game has been solved</returns>
        public bool checkWin()
        {
            bool result = false;
            string order = "";
            foreach (Tile tile in tiles)
            {
                if (tile != null)
                {
                    order += tile.Text + "_";
                }
            }
            if (order == winString && tiles[num_rows - 1, num_columns - 1] == null)
            {
                MessageBox.Show("You Win!");
                result = true;
                allMoves = new List<char>();
                //wait 1 second then scramble tiles again
                Thread.Sleep(1000);
                scramble();
            }
            return result;
        }
        /// <summary>
        /// Moves the appropriate tile based on a key press
        /// </summary>
        /// <param name="sender">The key that was pressed</param>
        /// <param name="e">Event arguments for the key press event</param>
        private void FifteenPuzzle_KeyDown(object sender, KeyEventArgs e)
        {
            if (!txtRows.ContainsFocus && !txtColumns.ContainsFocus)
            {
                string key = e.KeyCode.ToString();
                if (moveByDirection(key, false))
                {
                    switch (key)
                    {
                        case "S":
                            allMoves.Add('W');
                            break;
                        case "A":
                            allMoves.Add('D');
                            break;
                        case "D":
                            allMoves.Add('A');
                            break;
                        case "W":
                            allMoves.Add('S');
                            break;
                        default: break;
                    }
                    checkWin();
                }
            }
        }
        /// <summary>
        /// Scrambles the tiles
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnScramble_Click(object sender, EventArgs e)
        {
            scramble();
        }
        /// <summary>
        /// scrambles the tiles
        /// </summary>
        public void scramble()
        {
            //scramble larger grids more times
            int timesToSCramble = num_rows * num_columns * SCRAMBLE_FACTOR;
            Random rand = new Random();
            for (int i = 0; i < timesToSCramble; i++)
            {
                int val = rand.Next(NUM_DIRECTIONS);
                switch (val)
                {
                    case 0:
                        if (moveByDirection("A", false))
                        {
                            allMoves.Add('D');
                        }
                        break;
                    case 1:
                        if (moveByDirection("S", false))
                        {
                            allMoves.Add('W');
                        }
                        break;
                    case 2:
                        if (moveByDirection("D", false))
                        {
                            allMoves.Add('A');
                        }
                        break;
                    case 3:
                        if (moveByDirection("W", false))
                        {
                            allMoves.Add('S');
                        }
                        break;
                    default:
                        break;
                }
            }
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
        /// solves the puzzle based on recorded moves
        /// </summary>
        private void solvePuzzle()
        {
            allMoves.Reverse();
            foreach (char direction in allMoves)
            {
                moveByDirection(direction.ToString(), true);
            }
            allMoves = new List<char>();
        }
        /// <summary>
        /// Generates the grid of tiles given a number of rows, columns and a string of numbers
        /// </summary>
        /// <param name="rows">The number of rows in the grid</param>
        /// <param name="columns">The number of columns in the grid</param>
        /// <param name="numbers">The string of numbers in order</param>
        private void generateGrid(int rows, int columns, string numbers)
        {
            foreach (Tile tile in tiles)
            {
                Controls.Remove(tile);
            }
            tiles = new Tile[rows, columns];

            int x;
            int y = TOP;
            int height = GAME_HEIGHT / num_rows;
            int width = GAME_WIDTH / num_columns;
            string[] allNumbers = numbers.Split(new char[] { '_' });

            int counter = 0;
            for (int i = 0; i < num_rows; i++)
            {
                x = LEFT;
                for (int j = 0; j < num_columns; j++)
                {
                    int num = int.Parse(allNumbers[counter]);
                    if (num > 0) //Square should be blank
                    {
                        if (usingPicture) //generate a grid using a picture
                        {
                            tiles[i, j] = new Tile(height, width, y, x, num.ToString(), i, j, true, this);
                        }
                        else //generate a grid using numbers
                        {
                            tiles[i, j] = new Tile(height, width, y, x, num.ToString(), i, j, this);
                        }
                    }
                    counter++;
                    x += width;
                }
                y += height;
            }
            foreach (Tile tile in tiles)
            {
                Controls.Add(tile);
            }

            allMoves = new List<char>();

            winString = "";
            for (int i = 1; i < num_rows * num_columns; i++)
            {
                winString += i + "_";
            }
        }

        /// <summary>
        /// Generates the grid based on user input rows and columns
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                num_rows = int.Parse(txtRows.Text);
                num_columns = int.Parse(txtColumns.Text);
                if (num_rows > MAX_ROWS || num_columns > MAX_COLUMNS || num_rows < MIN_ROWS || num_columns < MIN_COLUMNS)
                {
                    throw new ArgumentOutOfRangeException();
                }
                usingPicture = false;

                string defaultOrder = "";
                for (int i = 1; i < num_rows * num_columns; i++)
                {
                    defaultOrder += i.ToString() + "_";
                }
                defaultOrder += "-1";

                generateGrid(num_rows, num_columns, defaultOrder);

                scramble();
                this.Focus();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Rows and columns must be between " + MIN_ROWS + " and " + MAX_ROWS + ".");
                txtRows.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a number of rows and a number of columns.");
                txtRows.Focus();
            }
        }
        /// <summary>
        /// Generates the grid using a picture
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnUsePicture_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsInput = int.Parse(txtRows.Text);
                int columnsInput = int.Parse(txtColumns.Text);

                if (rowsInput > MAX_ROWS || columnsInput > MAX_ROWS || rowsInput < MIN_ROWS || columnsInput < MIN_ROWS)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (rowsInput != columnsInput)
                {
                    throw new ArgumentException();
                }
                num_rows = rowsInput;
                num_columns = columnsInput;


                txtRows.Text = num_rows.ToString();
                txtColumns.Text = num_columns.ToString();
                usingPicture = true;

                string defaultOrder = "";
                for (int i = 1; i < num_rows * num_columns; i++)
                {
                    defaultOrder += i.ToString() + "_";
                }
                defaultOrder += "-1";

                generateGrid(num_rows, num_columns, defaultOrder);
                //Show the picture for 1 second before scrambling
                this.Refresh();
                Thread.Sleep(1000);

                //scramble the picture puzzle
                scramble();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Rows and Columns must be between " + MIN_ROWS + " and " + MAX_ROWS + ".");
                txtRows.Focus();
            }
            catch (ArgumentException)
            {
                MessageBox.Show("To use a picture, number of rows must equal number of columns.");
                txtRows.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter a number of rows and a number of columns.");
                txtRows.Focus();
            }
        }
        /// <summary>
        /// Saves the game on a button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult r = dlgSave.ShowDialog();
            switch (r)
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string fileName = dlgSave.FileName;
                    saveGame(fileName);
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Saves the current game
        /// </summary>
        /// <param name="fileName">The file to save the game to</param>
        private void saveGame(string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(usingPicture.ToString());
            writer.WriteLine(num_rows);
            writer.WriteLine(num_columns);

            for (int i = 0; i < num_rows; i++)
            {
                for (int j = 0; j < num_columns; j++)
                {
                    if (tiles[i, j] != null)
                    {
                        writer.WriteLine(tiles[i, j].Text);
                    }
                    else
                    {
                        writer.WriteLine("-1");
                    }
                }
            }
            //save the moves that have been made so far
            writer.WriteLine(new string(allMoves.ToArray()));
            writer.Close();
        }
        /// <summary>
        /// Loads a saved game
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Event arguments for the click event</param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            DialogResult r = dlgOpen.ShowDialog();
            switch (r)
            {
                case DialogResult.Abort:
                    break;
                case DialogResult.Cancel:
                    break;
                case DialogResult.Ignore:
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.None:
                    break;
                case DialogResult.OK:
                    string filename = dlgOpen.FileName;
                    loadGame(filename);
                    break;
                case DialogResult.Retry:
                    break;
                case DialogResult.Yes:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Loads the selected saved game
        /// </summary>
        /// <param name="filename">The filename of the saved game</param>
        private void loadGame(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            usingPicture = bool.Parse(reader.ReadLine());
            num_rows = int.Parse(reader.ReadLine());
            num_columns = int.Parse(reader.ReadLine());
            string gameString = "";

            for (int i = 0; i < num_rows * num_columns; i++)
            {
                gameString += reader.ReadLine() + "_";
            }

            generateGrid(num_rows, num_columns, gameString);
            //get list of moves that have been made int the saved game
            allMoves = reader.ReadLine().ToList<char>();

            reader.Close();
        }
    }
}
