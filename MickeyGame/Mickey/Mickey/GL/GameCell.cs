using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mickey.GL
{
    class GameCell
    {
        int row;
        int col;
        GameObject currentGameObject;
        GameGrid grid;
        PictureBox pictureBox;
        const int width = 20;
        const int height = 20;
        static int score = 0;
        public GameCell()
        {

        }
        public GameCell(int row, int col, GameGrid grid)
        {
            this.row = row;
            this.col = col;
            pictureBox = new PictureBox();
            pictureBox.Left = col * width;
            pictureBox.Top = row * height;
            pictureBox.Size = new Size(width, height);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.Transparent;
            this.grid = grid;
        }
        public void setGameObject(GameObject gameObject)
        {
            currentGameObject = gameObject;
            pictureBox.Image = gameObject.Image;

        }

        public GameCell nextCell(GameDirection direction)
        {
            if (direction == GameDirection.Left)
            {
                if (this.col > 0)
                {
                    GameCell ncell = grid.getCell(row, col - 1);
                    if (ncell.CurrentGameObject.GameObjectType != GameObjectType.WALL && ncell.CurrentGameObject.GameObjectType != GameObjectType.ENEMY && ncell.CurrentGameObject.GameObjectType != GameObjectType.Quest)
                    {
                        return ncell;
                    }
                }
            }

            if (direction == GameDirection.Right)
            {
                if (this.col < grid.Cols - 1)
                {
                    GameCell ncell = grid.getCell(this.row, this.col + 1);
                    if (ncell.CurrentGameObject.GameObjectType != GameObjectType.WALL && ncell.CurrentGameObject.GameObjectType != GameObjectType.ENEMY && ncell.CurrentGameObject.GameObjectType != GameObjectType.Quest)
                    {
                        return ncell;
                    }

                }
            }

            if (direction == GameDirection.Up)
            {
                if (this.row > 0)
                {
                    GameCell ncell = grid.getCell(this.row - 1, this.col);
                    if (ncell.CurrentGameObject.GameObjectType != GameObjectType.WALL && ncell.CurrentGameObject.GameObjectType != GameObjectType.ENEMY && ncell.CurrentGameObject.GameObjectType != GameObjectType.Quest)
                    {
                        return ncell;
                    }

                }
            }

            if (direction == GameDirection.Down)
            {
                if (this.row < grid.Rows - 1)
                {
                    GameCell ncell = grid.getCell(this.row + 1, this.col);
                    if (ncell.CurrentGameObject.GameObjectType != GameObjectType.WALL && ncell.CurrentGameObject.GameObjectType != GameObjectType.ENEMY && ncell.CurrentGameObject.GameObjectType != GameObjectType.Quest)
                    {
                        return ncell;
                    }
                }
            }
            return this; 
        }
        public List<GameCell> GetAdjacentCells()
        {
            List<GameCell> adjacentCells = new List<GameCell>();

            if (row > 0)
                adjacentCells.Add(grid.getCell(row - 1, col)); // Up

            if (row < grid.Rows - 1)
                adjacentCells.Add(grid.getCell(row + 1, col)); // Down

            if (col > 0)
                adjacentCells.Add(grid.getCell(row, col - 1)); // Left

            if (col < grid.Cols - 1)
                adjacentCells.Add(grid.getCell(row, col + 1)); // Right

            return adjacentCells;
        }

        public int X { get => row; set => row = value; }
        public int Y { get => col; set => col = value; }
        public GameObject CurrentGameObject { get => currentGameObject; }
        public PictureBox PictureBox { get => pictureBox; set => pictureBox = value; }
        public static int Score { get => score; set => score = value; }

    }
}
