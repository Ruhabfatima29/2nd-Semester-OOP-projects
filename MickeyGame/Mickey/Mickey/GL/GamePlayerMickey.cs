using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class GamePlayerMickey:GameObject
    {
        private int health;
        private int lives;
        public GamePlayerMickey(Image image, GameCell startCell) : base(GameObjectType.PLAYER, image)
        {
            this.CurrentCell = startCell;
        }

        public int Health { get => health; set => health = value; }
        public int Lives { get => lives; set => lives = value; }

        public void move(GameCell gameCell)
        {
            CurrentCell = gameCell;
        }
    }
}
