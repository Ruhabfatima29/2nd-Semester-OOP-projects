using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class Bullet:GameObject
    {
        GameDirection direction;
        bool isActive;
        public Bullet(Image ghostImage, GameCell startCell, GameDirection direction) : base(GameObjectType.Bullet, ghostImage)
        {
            this.CurrentCell = startCell;
            this.Direction = direction;
        }
        public static void move(Bullet bullet)
        {
            GameCell currentCell = bullet.CurrentCell;
            GameCell nextCell = currentCell.nextCell(bullet.Direction);
            if (nextCell != currentCell)
            {
                if (nextCell.CurrentGameObject.GameObjectType == GameObjectType.NONE || nextCell.CurrentGameObject.GameObjectType == GameObjectType.WALL)
                {
                    currentCell.setGameObject(Game.getBlankGameObject());
                    bullet.CurrentCell = nextCell;
                    nextCell.setGameObject(bullet);
                    bullet.IsActive = true;
                }
                else
                {
                    bullet.IsActive = false;
                }

            }
            else
            {
                bullet.IsActive = false;
            }
        }
        public static Bullet GenerateBullet(GamePlayerMickey mickey, GameDirection direction)
        {
            GameCell start = mickey.CurrentCell.nextCell(direction);
            Bullet bullet = null;
            if (start != null)
            {
                Image bulet = Game.getGameObjectImage('.');
                bullet = new Bullet(bulet, start, direction);
                start.setGameObject(bullet);

            }
            return bullet;
        }
        public GameDirection Direction { get => direction; set => direction = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
