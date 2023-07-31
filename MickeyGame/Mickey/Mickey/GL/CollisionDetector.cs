using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class CollisionDetector
    {
        public bool isMickeyCollideWithGhost(GameGhost ghost)
        {
            bool flag = false;
            if (ghost.CurrentCell.CurrentGameObject.GameObjectType == GameObjectType.PLAYER)
            {
                flag = true;
            }
            return flag;
        }

        public bool isMickeyCollideWithPallet(GameCell cell)
        {
            bool flag = false;
            if (cell.CurrentGameObject.GameObjectType == GameObjectType.REWARD )
            {
                flag = true;
            }
            return flag;

        }
        public bool isMickeyCollideWithEnergyBooster(GameCell cell)
        {
            bool flag = false;
            if (cell.CurrentGameObject.GameObjectType == GameObjectType.EnergyPortion)
            {
                flag = true;
            }
            return flag;
        }
        public bool isMickeyCollideWithKey(GameCell cell)
        {
            bool flag = false;
            if (cell.CurrentGameObject.GameObjectType == GameObjectType.Key)
            {
                flag = true;
            }
            return flag;
        }
        public bool isBulletCollideWithGhost(GameGhost g)
        {
            foreach (GameCell cell in g.CurrentCell.GetAdjacentCells())
            {
                if (cell.CurrentGameObject.GameObjectType == GameObjectType.Bullet)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isEnemyBulletCollideWithMickey(GamePlayerMickey m)
        {
            foreach (GameCell cell in m.CurrentCell.GetAdjacentCells())
            {
                if (cell.CurrentGameObject.GameObjectType == GameObjectType.EnemyBullet)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
