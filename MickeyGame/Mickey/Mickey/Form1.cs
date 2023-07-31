using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZInput;
using Mickey.GL;
namespace Mickey
{
    public partial class Form1 : Form
    {
        Game game;
        CollisionDetector collider;
        GamePlayerMickey mickey ;
        List<Bullet> removeBullets;
        List<EnemyBullet> removeBullet;
        List<GameGhost> deadGhosts;
        public Form1()
        {
            InitializeComponent();
            game = new Game(this);
            collider = new CollisionDetector();
            mickey = game.getGamePlayerMickey();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            moveMickey();
            moveBullets();
            moveEnemyBullets();
            removeExtraBullets();
            removeEnemyExtraBullets();
            checksMickeyHealth();
        }
        private void moveMickey()
        {
            
            GameCell potentialNewCell = mickey.CurrentCell;
            if (Keyboard.IsKeyPressed(Key.LeftArrow))
            {
                potentialNewCell = mickey.CurrentCell.nextCell(GameDirection.Left);
            }
            if (Keyboard.IsKeyPressed(Key.RightArrow))
            {
                potentialNewCell = mickey.CurrentCell.nextCell(GameDirection.Right);
            }
            if (Keyboard.IsKeyPressed(Key.UpArrow))
            {
                potentialNewCell = mickey.CurrentCell.nextCell(GameDirection.Up);
            }
            if (Keyboard.IsKeyPressed(Key.DownArrow))
            {
                potentialNewCell = mickey.CurrentCell.nextCell(GameDirection.Down);
            }
            if (Keyboard.IsKeyPressed(Key.Space))
            {
                game.createBullet(mickey,GameDirection.Right);
            }
            if (Keyboard.IsKeyPressed(Key.Num8))
            {
                game.createBullet(mickey, GameDirection.Up);
            }
            if (Keyboard.IsKeyPressed(Key.Num4))
            {
                game.createBullet(mickey, GameDirection.Left);
            }
            if (Keyboard.IsKeyPressed(Key.Num2))
            {
                game.createBullet(mickey, GameDirection.Down);
            }
            GameCell currentCell = mickey.CurrentCell;
            currentCell.setGameObject(Game.getBlankGameObject());
            if (collider.isMickeyCollideWithPallet(potentialNewCell))
            {
                game.addScorePoints(10);
            }
            if (collider.isMickeyCollideWithEnergyBooster(potentialNewCell))
            {
                game.addMickeyHealth(mickey,20);
            }
            if (collider.isEnemyBulletCollideWithMickey(mickey))
            {
             game.addMickeyHealth(mickey, -10);
            }
            if (collider.isMickeyCollideWithKey(potentialNewCell))
            {
                game.Keys +=1;
            }
            mickey.move(potentialNewCell);

        }
        public void checksMickeyHealth()
        {
            if(mickey.Health <5 && mickey.Lives > 0)
            {
                game.reduceMickeyLife(mickey);
                game.refillHealth(mickey);
            }
            
        }
        public void moveGhosts()
        {
            deadGhosts = new List<GameGhost>();
            foreach (GameGhost g in game.ghosts.ToList())
            {
                if (collider.isMickeyCollideWithGhost(g))
                {
                    if(g.GhostType == GameGhostType.Smart)
                    {
                        game.addMickeyHealth(mickey,-10);
                    }
                    else
                    {
                        game.addScorePoints(-1);
                    }
                }
                if (collider.isBulletCollideWithGhost(g))
                {
                    g.Health -= 10;
                    game.addScorePoints(10);

                    if (g.Health <= 0)
                    {
                        g.IsAlive = false;
                        deadGhosts.Add(g);
                    }
                }
                g.move(g.nextCell());
            }
            if (game.ghosts.Count == 0)
            {
                game.Grid.ReplaceQuestWithKey();
                game.IsQuestReplaced = true;
            }
        }
        private void removeDeadGhosts()
        {
         foreach(GameGhost g in deadGhosts)
            {
                g.CurrentCell.setGameObject(Game.getBlankGameObject());
                game.ghosts.Remove(g);
            }
        }
        public void moveEnemyBullets()
        {
            removeBullet = new List<EnemyBullet>();
            foreach(EnemyBullet bullet in game.enemyBullets)
            {
                EnemyBullet.move(bullet);
                if(bullet.IsActive == false)
                {
                    removeBullet.Add(bullet);
                }
            }
        }
        public void moveBullets()
        {
            removeBullets = new List<Bullet>();
            foreach (Bullet bullet in game.Bullets)
            {
                Bullet.move(bullet);
                if (bullet.IsActive == false)
                {
                    removeBullets.Add(bullet);
                }
            }
        }
        

        private void removeEnemyExtraBullets()
        {
            foreach (EnemyBullet bullet in removeBullet)
            {
                bullet.CurrentCell.setGameObject(Game.getBlankGameObject());
                game.enemyBullets.Remove(bullet);
            }
        }
        private void createEnemyBullets()
        {
            foreach(GameGhost g in game.ghosts)
            {
                GameGhostType type = g.GhostType;
                {
                    if(type != GameGhostType.Smart)
                    {
                        if(type== GameGhostType.Vertical)
                        {
                            game.createEnemyBullet(g, GameDirection.Left);
                        }
                        else
                        {
                            game.createEnemyBullet(g, GameDirection.Down);
                        }
                    }
                }
            }
        }
        private void removeExtraBullets()
        {
            foreach (Bullet bullet in removeBullets)
            {
                bullet.CurrentCell.setGameObject(Game.getBlankGameObject());
                game.Bullets.Remove(bullet);
            }
        }
        private void showScore()
        {
            score.Text = game.getScore().ToString();
        }
        private void showLives()
        {
            mickLives.Text = mickey.Lives.ToString();
        }
        public void showHealth()
        {
            healthMickey.Text = mickey.Health.ToString();
        }
        public void showKeys()
        {
            keysGrid.Text = game.Keys.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerticalGhost g1 = new VerticalGhost(game.getWhiteGhostImage(), game.getCell(3, 6));
            g1.Health = 50;
            g1.IsAlive = true;
            VerticalGhost g2 = new VerticalGhost(game.getWhiteGhostImage(), game.getCell(3, 50));
            g2.Health = 50;
            g2.IsAlive = true;
            HorizontalGhost g3 = new HorizontalGhost(game.getYellowGhostImage(), game.getCell(5, 6));
            g3.Health = 50;
            g3.IsAlive = true;
            HorizontalGhost g4 = new HorizontalGhost(game.getYellowGhostImage(), game.getCell(18, 22));
            g4.Health = 50;
            g4.IsAlive = true;
            SmartGhost g5 = new SmartGhost(game.getSmartGhostImage(), game.getCell(8, 30), mickey);
            g5.Health = 50;
            g5.IsAlive = true;

            game.addGhost(g1);
            game.addGhost(g2);
            game.addGhost(g3);
            game.addGhost(g4);
            game.addGhost(g5);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            moveGhosts();
            removeDeadGhosts();
            showScore();
            showHealth();
            showLives();
            showKeys();
            if (mickey.Lives == 0 && game.Keys != 4)
            {
                timer2.Enabled = false;
                this.Hide();
                Form m = new Lose();
                m.Show();
            }

            if (mickey.Lives != 0 && game.Keys == 4)
            {
                timer2.Enabled = false;
                this.Hide();
                Form m = new Win();
                m.Show();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            createEnemyBullets();
        }
    }
}
