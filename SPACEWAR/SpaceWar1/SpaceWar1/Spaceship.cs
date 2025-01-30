using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceWar1
{
    public class Spaceship
    {
        private int Health;
        private int Speed = 10;
        private int Damage = 1;
        private PictureBox SpaceshipImg;
        public List<Bullet> bullets = new List<Bullet>();
        Game game;
        public void setHealth(int health) 
        { 
            this.Health = health; 
        }
        public void setDamage(int damage) { this.Damage = damage; }
        public void setSpeed(int speed) { this.Speed = speed; }
        public int getHealth() { return this.Health; }
        public int getSpeed() { return this.Speed; }
        public int getDamage() { return this.Damage; }
        public Spaceship(Game game)
        {
            this.game = game;
            this.Health = 100;
            SpaceshipImg = new PictureBox
            {

                BackColor = Color.Transparent, 
                Size = new Size(150, 150), 
                Location = new Point(100, 100),
                Image = Image.FromFile("Resources/player.png"), 
                SizeMode = PictureBoxSizeMode.StretchImage 
            };
            game.form.Controls.Add(SpaceshipImg);
            SpaceshipImg.SendToBack(); 

        }

        public void Shoot()
        {
            if (Health <= 0) return; // Sağlık sıfır veya altındaysa ateş etmeyecek
            Bullet bullet = new Bullet(this.game,false);
            bullet.Damage = this.Damage;
            bullet.direction = 1;
            bullet.bulletImg.Location = new Point
            (
                SpaceshipImg.Location.X + SpaceshipImg.Width,
                SpaceshipImg.Location.Y + SpaceshipImg.Height / 2 - bullet.bulletImg.Height / 2
            );
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Destroy(); // oyuncu yok olunca 
            }
        }

        // Oyuncuyu yok et
        public void Destroy()
        {
            game.form.Controls.Remove(SpaceshipImg); // Ekrandan kaldır
        }

        public PictureBox GetSpaceshipImg()
        {
            return SpaceshipImg;
        }

        public void Move()
        {
            // Form'un genişliği ve yüksekliği
            int formWidth = game.form.Width;
            int formHeight = game.form.Height;

           
            if (game.up && SpaceshipImg.Location.Y > 0)
            {
                SpaceshipImg.Location = new Point(SpaceshipImg.Location.X, SpaceshipImg.Location.Y - Speed);
            }

           
            if (game.down && SpaceshipImg.Location.Y + SpaceshipImg.Height < formHeight)
            {
                SpaceshipImg.Location = new Point(SpaceshipImg.Location.X, SpaceshipImg.Location.Y + Speed);
            }

            if (game.left && SpaceshipImg.Location.X > 0)
            {
                SpaceshipImg.Location = new Point(SpaceshipImg.Location.X - Speed, SpaceshipImg.Location.Y);
            }

           
            if (game.right && SpaceshipImg.Location.X + SpaceshipImg.Width < formWidth)
            {
                SpaceshipImg.Location = new Point(SpaceshipImg.Location.X + Speed, SpaceshipImg.Location.Y);
            }
        }

        public Point GetPosition()
        {
            return SpaceshipImg.Location; // Oyuncunun mevcut pozisyonunu döndür
        }



    }
}
