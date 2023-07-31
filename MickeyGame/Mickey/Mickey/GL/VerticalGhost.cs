using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class VerticalGhost:GameGhost
    {
        GameDirection direction = GameDirection.Down;

        public VerticalGhost(Image ghostImage, GameCell startCell) : base(startCell,ghostImage)
        {
            this.CurrentCell = startCell;
            this.GhostType = GameGhostType.Vertical;
        }

        
        public override void move(GameCell gameCell)
        {
            GameCell nextCell = this.nextCell(); 

            if (nextCell != null && nextCell.CurrentGameObject.GameObjectType == GameObjectType.NONE)
            {
                this.CurrentCell.setGameObject(Game.getBlankGameObject());
                CurrentCell = nextCell; 
                nextCell.setGameObject(this); 
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
            if (direction == GameDirection.Up)
            {
                direction = GameDirection.Down;
            }
            else if (direction == GameDirection.Down)
            {
                direction = GameDirection.Up;
            }
        }
    }
}
