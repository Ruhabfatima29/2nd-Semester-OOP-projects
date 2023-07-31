using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class SmartGhost : GameGhost
    {
        GameDirection direction;
        GamePlayerMickey mickey;
        public SmartGhost(Image image, GameCell startCell, GamePlayerMickey mick) : base(startCell, image)
        {
            this.CurrentCell = startCell;
            this.mickey = mick;
            this.GhostType = GameGhostType.Smart;
        }

        public double calculateDistance(GameCell startCell)
        {
            return Math.Sqrt(Math.Pow((mickey.CurrentCell.X - startCell.X), 2) + Math.Pow((mickey.CurrentCell.Y - startCell.Y), 2));
        }
        public override void move(GameCell gameCell)
        {
            if (this.CurrentCell != null)
            {
                this.CurrentCell.setGameObject(Game.getBlankGameObject());
            }
            CurrentCell = gameCell;
            
        }
        public override GameCell nextCell()
        {
            double[] distance = new double[4] { 10000, 10000, 10000, 10000 };
            if (this.CurrentCell.nextCell(GameDirection.Left).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[0] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Left));
            }
            if (this.CurrentCell.nextCell(GameDirection.Right).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[1] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Right));
            }
            if (this.CurrentCell.nextCell(GameDirection.Up).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[2] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Up));
            }
            if (this.CurrentCell.nextCell(GameDirection.Down).CurrentGameObject.GameObjectType != GameObjectType.WALL)
            {
                distance[3] = calculateDistance(this.CurrentCell.nextCell(GameDirection.Down));
            }
            if (distance[0] <= distance[1] && distance[0] <= distance[2] && distance[0] <= distance[3])
            {
                this.direction = GameDirection.Left;

            }
            if (distance[1] <= distance[0] && distance[1] <= distance[2] && distance[1] <= distance[3])
            {
                this.direction = GameDirection.Right;

            }
            if (distance[2] <= distance[0] && distance[2] <= distance[1] && distance[2] <= distance[3])
            {
                this.direction = GameDirection.Up;

            }
            if (distance[3] <= distance[0] && distance[3] <= distance[1] && distance[3] <= distance[2])
            {
                this.direction = GameDirection.Down;

            }
            GameCell currentCell = this.CurrentCell;
            GameCell nextCell = currentCell.nextCell(direction);
            if (nextCell != currentCell && nextCell.CurrentGameObject.GameObjectType == GameObjectType.WALL)
            {
                currentCell.setGameObject(Game.getBlankGameObject());
                this.CurrentCell = nextCell;
            }

            return nextCell;
        }
    }
}


