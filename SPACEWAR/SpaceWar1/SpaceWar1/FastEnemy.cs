using System;
using System.Drawing;

namespace SpaceWar1
{
    internal class FastEnemy : Enemy
    {
        private Random random;
        private int followCounter = 0; // Takip için sayaç

        public FastEnemy(Game game) : base(game)
        {
            this.setHealth(1); 
            this.setSpeed(3); 
            this.setDamage(15); 
            random = new Random();
            this.puan = 20; 

            
            int spawnX = game.form.ClientSize.Width; // Ekranın sağ tarafı
            int spawnY = random.Next(0, game.form.ClientSize.Height - enemyImg.Height); 
            Position = new Point(spawnX, spawnY);

            enemyImg.Location = Position;
            enemyImg.Image = Image.FromFile("Resources/FastEnemy.png"); 
        }

        public override void Move()
        {
            if (game.isGameOver) return;

            followCounter++;

            if (followCounter % 2 == 0) // Belirli bir süre sonra oyuncuyu hedef al
            {
                // Oyuncunun mevcut pozisyonunu al
                Point playerPosition = game.player.GetSpaceshipImg().Location;

                // Pozisyon farkı
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
            }
            else
            {
                // Normalde yatay hareket
                Position = new Point(Position.X - getSpeed(), Position.Y);
            }

            enemyImg.Location = Position;

            
            if (Position.X + enemyImg.Width < 0)
            {
                Destroy();
            }
        }

        public override void Attack()
        {
            // yoook
        }
    }
}
