using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mickey.GL
{
    class Game
    {
        GamePlayerMickey mickey;
        GameGrid grid;
        bool isQuestReplaced = false;
        public List<GameGhost> ghosts;
        public List<Bullet> Bullets = new List<Bullet>();
        public List<EnemyBullet> enemyBullets = new List<EnemyBullet>();
        int score = 0;
        int keys = 0;
        Form gameGUI;

        public GameGrid Grid { get => grid; set => grid = value; }
        public bool IsQuestReplaced { get => isQuestReplaced; set => isQuestReplaced = value; }
        public int Keys { get => keys; set => keys = value; }

        public Game(Form gameGUI)
        {
            this.gameGUI = gameGUI;
            Grid = new GameGrid("maze.txt", 29, 55);
            Image mickeyImage = Game.getGameObjectImage('M');
            ghosts = new List<GameGhost>();
            GameCell startCell = Grid.getCell(8, 10);
            mickey = new GamePlayerMickey(mickeyImage, startCell);
            mickey.Health = 50;
            mickey.Lives = 3;
            printMaze(Grid);

        }
        public GameCell getCell(int x, int y)
        {
            return Grid.getCell(x, y);
        }
        public void addGhost(GameGhost ghost)
        {
            ghosts.Add(ghost);
        }
        public void createBullet(GamePlayerMickey m, GameDirection direction)
        {
            Bullet b = Bullet.GenerateBullet(m, direction);
            Bullets.Add(b);
        }
        
        public void createEnemyBullet(GameGhost ghost, GameDirection direction)
        {
            EnemyBullet bullet = EnemyBullet.GenerateEnemyBullet(ghost, direction);
            enemyBullets.Add(bullet);
        }

        public GamePlayerMickey getGamePlayerMickey()
        {
            return mickey;
        }
        public void addScorePoints(int points)
        {
            this.score = score + points;
        }
        public void reduceMickeyLife(GamePlayerMickey mickey)
        {
            mickey.Lives -= 1;
        }
        public void addMickeyHealth(GamePlayerMickey mickey, int value)
        {
            mickey.Health += value;
        }
        public void refillHealth(GamePlayerMickey mickey)
        {
            mickey.Health = 50;
        }
        public int getScore()
        {
            return score;
        }
        void printMaze(GameGrid grid)
        {
            for (int x = 0; x < grid.Rows; x++)
            {

                for (int y = 0; y < grid.Cols; y++)
                {
                    GameCell cell = grid.getCell(x, y);
                    gameGUI.Controls.Add(cell.PictureBox);
                }

            }
            
        }

        public static GameObject getBlankGameObject()
        {
            GameObject blankGameObject = new GameObject(GameObjectType.NONE, Mickey.Properties.Resources.blackbox);
            return blankGameObject;
        }

        public Image getSmartGhostImage()
        {
            return Mickey.Properties.Resources.Ghost;
        }

        public Image getYellowGhostImage()
        {
            return Mickey.Properties.Resources.YellowGhost;
        }
        public Image getWhiteGhostImage()
        {
            return Mickey.Properties.Resources.whiteghost;
        }
        public Image getKeyImage()
        {
            return Mickey.Properties.Resources.key;
        }
        public static Image getGameObjectImage(char displayCharacter)
        {

            Image img = Mickey.Properties.Resources.blackbox;


            if (displayCharacter == '$' || displayCharacter == '*' )
            {
                img = Mickey.Properties.Resources.greytile;
            }

            if (displayCharacter == '!')
            {
                img = Mickey.Properties.Resources.browntile;
            }

            if (displayCharacter == 'k')
            {
                img = Mickey.Properties.Resources.coin;
            }
            if (displayCharacter == 'M' || displayCharacter == 'm')
            {
                img = Mickey.Properties.Resources.mickey1;
            }
            if(displayCharacter == '%')
            {
                img = Mickey.Properties.Resources.energyportion;
            }
            if (displayCharacter == '.')
            {
                img = Mickey.Properties.Resources.bullet;
            }
            if (displayCharacter == '-')
            {
                img = Mickey.Properties.Resources.VerticalBullet;
            }
            if (displayCharacter == 'q')
            {
                img = Mickey.Properties.Resources.Quest;
            }
            if (displayCharacter == 'K')
            {
                img = Mickey.Properties.Resources.key;
            }
            return img;
        }

    }
}
