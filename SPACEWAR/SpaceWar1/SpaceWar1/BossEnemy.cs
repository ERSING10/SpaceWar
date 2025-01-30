using System;
using System.Drawing;

namespace SpaceWar1
{
    internal class BossEnemy : Enemy
    {
        private Random random;
        private int fireCooldown = 400; // Ateş etme süresi döngü olarak yani
        private int cooldownCounter = 0; // Bekleme süresi sayacı

        public BossEnemy(Game game) : base(game)
        {
            this.setHealth(5); 
            this.setSpeed(1); 
            this.setDamage(25); 
            random = new Random();
            this.puan = 40; 

            
            int spawnX = game.form.ClientSize.Width; // Ekranın sağ tarafı
            int spawnY = random.Next(0, game.form.ClientSize.Height - enemyImg.Height); // Rastgele bir yer
            Position = new Point(spawnX, spawnY);

            enemyImg.Location = Position;
            enemyImg.Image = Image.FromFile("Resources/BossEnemy1.png"); 
        }

        public override void Move()
        {
            if (game.isGameOver) return;
            Position = new Point(Position.X - getSpeed(), Position.Y);
            enemyImg.Location = Position;

            
            if (Position.X + enemyImg.Width < 0)
            {
                Destroy();
            }

            // Saldırı mekanizması
            if (cooldownCounter >= fireCooldown)
            {
                Attack(); // Ateş etme
                cooldownCounter = 0; // Sayacı sıfırlama
            }
            else
            {
                cooldownCounter++;
            }
        }

        public override void Attack()
        {
            Bullet bullet = new Bullet(game, true) // Düşman mermisi oluştur
            {
                direction = -1, 
                Damage = 10 // Merminin hasarı
            };

            // Mermiyi boss düşmanın pozisyonuna yerleştirdi
            bullet.bulletImg.Location = new Point(Position.X, Position.Y + (enemyImg.Height / 2));
        }

    }
}
