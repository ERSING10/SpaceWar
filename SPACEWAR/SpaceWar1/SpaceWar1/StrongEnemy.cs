using System;
using System.Drawing;

namespace SpaceWar1
{
    internal class StrongEnemy : Enemy
    {
        private Random random;

        public StrongEnemy(Game game) : base(game)
        {
            this.setHealth(5); 
            this.setSpeed(2); 
            this.setDamage(20); 
            random = new Random();
            this.puan = 40; 

           
            int spawnX = game.form.ClientSize.Width; // Ekranın sağ tarafı
            int spawnY = random.Next(0, game.form.ClientSize.Height - enemyImg.Height); 
            Position = new Point(spawnX, spawnY);

            enemyImg.Location = Position;
            enemyImg.Image = Image.FromFile("Resources/StrongEnemy.png"); 
        }

        public override void Move()
        {

            if (game.isGameOver) return;
            // Oyuncunun mevcut pozisyonunu al
            Point playerPosition = game.player.GetSpaceshipImg().Location;

            // Pozisyon farkını hesapla
            int dx = playerPosition.X - Position.X;
            int dy = playerPosition.Y - Position.Y;

            double distance = CalculateDistance(Position, playerPosition);

            if (distance > 0)
            {
                // Oyuncuya doğru hareket et
                int stepX = (int)(getSpeed() * dx / distance);
                int stepY = (int)(getSpeed() * dy / distance);
                Position = new Point(Position.X + stepX, Position.Y + stepY);
            }

            enemyImg.Location = Position;

            
            if (Position.X + enemyImg.Width < 0)
            {
                Destroy();
            }
        }

        public override void Attack()
        {
            //bunda da yok
        }
    }
}
