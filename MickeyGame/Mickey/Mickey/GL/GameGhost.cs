using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    abstract class GameGhost: GameObject
    {
        protected int health;
        protected GameGhostType Ghosttype;
        private bool isAlive;
        public int Health { get => health; set => health = value; }
        public GameGhostType GhostType { get => Ghosttype; set => Ghosttype = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }

        public GameGhost(GameCell startCell,Image ghostImage) : base(GameObjectType.ENEMY, ghostImage)
        {
            this.CurrentCell = startCell;
        }

        
        public abstract GameCell nextCell();
        public abstract void move(GameCell gameCell);
    }
}
