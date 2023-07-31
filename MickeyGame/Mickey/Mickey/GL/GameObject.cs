using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.GL
{
    class GameObject
    {
        char displayCharacter;
        GameObjectType gameObjectType;
        GameCell currentCell;
        Image image;
        public GameObject(GameObjectType type, Image image)
        {
            this.gameObjectType = type;
            this.Image = image;
        }
        public GameObject(GameObjectType type, char displayCharacter)
        {
            this.gameObjectType = type;
            this.displayCharacter = displayCharacter;
        }

        public static GameObjectType getGameObjectType(char displayCharacter)
        {

            if (displayCharacter == '$' || displayCharacter == '*' || displayCharacter == '!')
            {
                return GameObjectType.WALL;
            }

            if (displayCharacter == 'k')
            {
                return GameObjectType.REWARD;
            }
            if (displayCharacter == 'G' || displayCharacter == 's')
            {
                return GameObjectType.ENEMY;
            }
            if (displayCharacter == '%')
            {
                return GameObjectType.EnergyPortion;
            }
            if (displayCharacter == 'q')
            {
                return GameObjectType.Quest;
            }
            if (displayCharacter == 'K')
            {
                return GameObjectType.Key;
            }
            return GameObjectType.NONE;
        }
        public char DisplayCharacter { get => displayCharacter; set => displayCharacter = value; }
        public GameObjectType GameObjectType { get => gameObjectType; set => gameObjectType = value; }
        public GameCell CurrentCell
        {
            get => currentCell;
            set
            {
                currentCell = value;
                currentCell.setGameObject(this);
            }
        }

        public Image Image { get => image; set => image = value; }
    }
}
    
