using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class HorizontalGhost:GameGhost
    {
        GameDirection direction = GameDirection.Left;
        public HorizontalGhost(Image ghostImage, GameCell startCell) : base(startCell, ghostImage)
        {
            this.CurrentCell = startCell;
            this.GhostType = GameGhostType.Horizontal;
        }

        

        public override void move(GameCell gameCell)
        {
            GameCell nextCell = this.nextCell(); 

            if (nextCell != null && nextCell.CurrentGameObject.GameObjectType == GameObjectType.NONE)
            {
                this.CurrentCell.setGameObject(Game.getBlankGameObject()); // Clear the current cell
                CurrentCell = nextCell; // Move the ghost to the next cell
                nextCell.setGameObject(this); // Set the ghost as the game object in the next cell
            }
        }

        public override GameCell nextCell()
        {
            GameCell nextCell = this.CurrentCell;
            GameCell potentialNextCell = this.CurrentCell.nextCell(direction);

            if (potentialNextCell == nextCell)
            {
                changeDirection();
            }
            else
            {
                // If the next cell is different, check if it contains a wall or a reward (coin)
                if (potentialNextCell.CurrentGameObject.GameObjectType == GameObjectType.WALL ||
                    potentialNextCell.CurrentGameObject.GameObjectType == GameObjectType.REWARD)
                {
                    changeDirection();
                }
                else
                {
                    nextCell = potentialNextCell;
                }
            }

            return nextCell;
        }
        private void changeDirection()
        {
            if (direction == GameDirection.Left)
            {
                direction = GameDirection.Right;
            }
            else if (direction == GameDirection.Right)
            {
                direction = GameDirection.Left;
            }
        }

    }
}

