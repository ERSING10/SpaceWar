using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceWar1
{
    public abstract class Enemy
    {
        
        private int Health;
        private int Speed;
        private int Damage;
        public Point Position { get; set; }
        public int puan { get; set; }

        public PictureBox enemyImg;
        protected Game game;

        private bool isGameOver = false; // Oyun bitiş durumu

        public Enemy(Game game)
        {
            this.game = game;
            enemyImg = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            game.form.Controls.Add(enemyImg);
        }

        public void setHealth(int health) { this.Health = health; }
        public void setDamage(int damage) { this.Damage = damage; }
        public void setSpeed(int speed) { this.Speed = speed; }
        public int getHealth() { return this.Health; }
        public int getSpeed() { return this.Speed; }
        public int getDamage() { return this.Damage; }

        // Soyut metotlar
        public abstract void Move(); 
        public abstract void Attack();
        public void TakeDamage(int amount)
        { // Hasar alma
            Health -= amount;
            if (Health <= 0)
            {
                game.skor += puan; 
                Destroy();
            }
        }
        public void Destroy() // Yok etme metodu
        {
            if (Position.X + enemyImg.Width < 0)
            {
                if (enemyImg.Visible) // Daha önce yok edilmediyse
                {
                    enemyImg.Visible = false; // Hasar verdikten sonra tekrar işlem yapmasın
                    game.skor -= this.puan;
                    if (game.skor < 0) game.skor = 0; 
                }
            }
            
            game.form.Controls.Remove(enemyImg); 
            game.enemies.Remove(this);
            game.form.Invalidate(); // Yeniden çizim yapılmasını sağlar
            enemyImg.Dispose(); // Resim nesnesini de yok et

        }

        public double CalculateDistance(Point current, Point target)
        {
            int dx = target.X - current.X;
            int dy = target.Y - current.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }


        public void StopMovement()
        {
            isGameOver = true; // Hareketi durdur
        }


    }
}
