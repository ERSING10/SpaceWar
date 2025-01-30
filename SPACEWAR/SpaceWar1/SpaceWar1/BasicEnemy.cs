using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace SpaceWar1
{
    internal class BasicEnemy : Enemy
    {
        private Random random;
        
        public BasicEnemy(Game game) : base(game)
        {
            this.setHealth(2); 
            this.setSpeed(1); 
            this.setDamage(5); 
            random = new Random();
            this.puan = 10;

            
            int spawnX = game.form.ClientSize.Width; // Ekranın sağ tarafı
            int spawnY = random.Next(0, game.form.ClientSize.Height - enemyImg.Height); 
            Position = new Point(spawnX, spawnY);

            enemyImg.Location = Position;
            enemyImg.Image = Image.FromFile("Resources/BasicEnemy.png"); 
        }

        // sağdan sola doğru ilerliyor
        public override void Move()
        {
            if (game.isGameOver) return;

            Position = new Point(Position.X - getSpeed(), Position.Y);
            enemyImg.Location = Position;

            // Düşman ekran dışına çıktıysa yok olur
            if (Position.X + enemyImg.Width < 0)
            {
                Destroy();
            }
        }

        
        public override void Attack()
        {
            // bu saldırmıyor
        }

    }
}
