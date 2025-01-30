using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.Media;

namespace SpaceWar1
{
    public class Game
    {
        public Form form;
        public Spaceship player;
        public bool up, down, right, left, shoot;
        public List<Bullet> enemyBullets = new List<Bullet>();// Mermi listesi
        public List<Enemy> enemies = new List<Enemy>(); // Düşman listesi
        private Random random;
        public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public int skor = 0;
        public int takipSayacı = 0, poweupSayacı = 0;
        public Point playerPosition;
        public Powerup powerup;
        public List<Control> gameovercontrols = new List<Control>();
        private string highScoreFilePath = "highscore.txt";
        public List <Meteor> meteors = new List<Meteor>();
        private Random meteorRandom = new Random();
        private int meteorSpawnCounter = 0;
        public System.Windows.Forms.Timer meteorTimer = new System.Windows.Forms.Timer();

        SoundPlayer musicplayer;
        public bool isGameOver { get; set; } = false;

        public Game(Form form)
        {
            this.form = form;
           // musicplayer = new SoundPlayer("Resources/background.wav");
           // musicplayer.PlayLooping();
            player = new Spaceship(this);
            random = new Random();
            timer.Interval = 500;
            timer.Start();
            timer.Tick += gameTimer;

            meteorTimer.Interval = 5000; // 5 saniyede bir meteor 
            meteorTimer.Start();
            meteorTimer.Tick += MeteorTimer_Tick;


        }

        public void UpdateGame()
        {
            player.Move();

            if(player.getHealth() <=0)
            {
                isGameOver = true;
                timer.Stop();
                ShowGameOver();
                return;
                
            }


            // Mermilerin hareketi
            foreach (var bullet in player.bullets.ToList())
            {
                bullet.Move();
            }

            
            if (shoot)
            {
                player.Shoot();
                shoot = false;
            }

            // Düşmanların hareketi
            foreach (var enemy in enemies.ToList())
            {
                enemy.Move();
                CollisionDetector.CheckCollision(player, enemy);
            }

            foreach (var bullet in player.bullets.ToList())
            {
                bullet.Move();
            }

            // Düşman mermileri
            foreach (var bullet in enemyBullets.ToList())
            {
                bullet.Move();
            }

            // Çarpışma kontrolü (düşman mermilerinin X oyuncuya çarpması)
            foreach (var bullet in enemyBullets.ToList())
            {
                if (bullet.bulletImg.Bounds.IntersectsWith(player.GetSpaceshipImg().Bounds))
                {
                    player.TakeDamage(bullet.Damage); 
                    bullet.Destroy();
                }
            }

            foreach (var meteor in meteors.ToList())
            {
                meteor.Move();

                // Meteor-oyuncu çarpışma kontrolü
                if (meteor.meteorImg.Bounds.IntersectsWith(player.GetSpaceshipImg().Bounds))
                {
                    player.TakeDamage(meteor.getDamage());
                    meteor.Destroy();
                }

                // Meteor-düşman çarpışma kontrolü
                foreach (var enemy in enemies.ToList())
                {
                    if (meteor.meteorImg.Bounds.IntersectsWith(enemy.enemyImg.Bounds))
                    {
                        enemy.TakeDamage(meteor.getDamage());
                        meteor.Destroy();
                    }
                }
            }

            if (powerup != null) powerup.CheckCollision(player);
            // Çarpışma kontrolü
            CollisionDetector.CheckBulletCollision(player.bullets, enemies); // Mermi-düşman çarpışma kontrolü
            
            if(skor < 0)
                skor = 0;

        }
        

        public void gameTimer(object sender, EventArgs e)
        {
            SpawnEnemies();
            if(takipSayacı == 8)
            {
                playerPosition = player.GetPosition();
                takipSayacı = 0;
            }
            else
            {
                takipSayacı++;
            }
            if (poweupSayacı == 21)
            {
                this.powerup = new Powerup(this);
                poweupSayacı = 0;
            }
            else poweupSayacı++;
        }


        private void MeteorTimer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();

            
            int xPosition = random.Next(0, form.ClientSize.Width - 50);
            int yPosition = -50; // Ekranın üstü

            
            int meteorType = random.Next(0, 2); 

            if (meteorType == 0) 
            {
                Meteor smallMeteor = new Meteor(this, 0, 10); 
                smallMeteor.Position = new Point(xPosition, yPosition);
                meteors.Add(smallMeteor); 
                form.Controls.Add(smallMeteor.meteorImg); 
            }
            else 
            {
                Meteor largeMeteor = new Meteor(this, 1, 20); 
                largeMeteor.Position = new Point(xPosition, yPosition);
                meteors.Add(largeMeteor); 
                form.Controls.Add(largeMeteor.meteorImg); 
            }

        }




        // Düşmanları spawn eden metot
        public void SpawnEnemies()
        {
            
            if (enemies.Count < 10) // Ekrandaki maksimum düşman sayısı
            {
                int xPosition = form.ClientSize.Width + random.Next(50, 200); // Sağdan başladı
                int yPosition = random.Next(0, form.ClientSize.Height - 100); 

                if (skor < 200) 
                {
                    rndEnemy(1);
                }
                else if(skor < 400) 
                {
                    rndEnemy(2);
                }
                else if(skor < 600)
                {
                    rndEnemy(3);
                }
                else
                {
                    int rnd = random.Next(1, 5);
                    rndEnemy(rnd);
                }                
            }
        }
        public void rndEnemy(int rnd)
        {
            if(rnd == 1)
            {
                BasicEnemy newEnemy = new BasicEnemy(this);
                enemies.Add(newEnemy);
            }
            if(rnd == 2)
            {
                FastEnemy newEnemy = new FastEnemy(this);
                enemies.Add(newEnemy);
            }
            if(rnd == 3)
            {
                StrongEnemy newEnemy = new StrongEnemy(this);
                enemies.Add(newEnemy);
            }
            if(rnd == 4)
            {
                BossEnemy newEnemy = new BossEnemy(this);
                enemies.Add(newEnemy);
            }
        }

        

        // En yüksek skoru dosyadan okuyup yazdıran metod
        private int GetHighScore()
        {
            if (File.Exists(highScoreFilePath))
            {
                string content = File.ReadAllText(highScoreFilePath);
                if (int.TryParse(content, out int highScore))
                {
                    return highScore;
                }
            }
            return 0; 
        }

        // Mevcut skoru ? en yüksek skor < ya da >
        private void UpdateHighScore(int currentScore)
        {
            int highScore = GetHighScore();
            if (currentScore > highScore)
            {
                File.WriteAllText(highScoreFilePath, currentScore.ToString());
            }
        }

        private void CreateMeteor()
        {
            int sizeType = meteorRandom.Next(0, 2); 
            int spawnX = meteorRandom.Next(0, form.ClientSize.Width - 50); 
            int spawnY = -50; //ekranın üstü

            Meteor meteor;

            if (sizeType == 0)
            {
                meteor = new Meteor(this,10,1); 
            }
            else
            {
                meteor = new Meteor(this,20 ,2); 
            }

            meteor.Position = new Point(spawnX, spawnY);
            meteors.Add(meteor);
            form.Controls.Add(meteor.meteorImg); // Form'a ekle
        }




        public void ShowGameOver()
        {
            //musicplayer.Stop();
            UpdateHighScore(skor);

            int highScore = GetHighScore();

           
            PictureBox gameOverImage = new PictureBox
            {
                Image = Image.FromFile("Resources/gameover2.png"), 
                SizeMode = PictureBoxSizeMode.StretchImage, 
                Dock = DockStyle.Fill // Formu tamamen kaplaması için
            };



            form.Controls.Add(gameOverImage);
            gameOverImage.BringToFront();
            gameovercontrols.Add(gameOverImage);
            // Skor
            Label scoreLabel = new Label
            {
                Text = $"{skor}\t     \t                  {highScore}",
                Font = new Font("Arial", 32, FontStyle.Regular),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                AutoSize = false,
                Size = new Size(form.ClientSize.Width, 100),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(form.ClientSize.Width * 1 / 500, form.ClientSize.Height *  90/ 100),
            };

            form.Controls.Add(scoreLabel);
            scoreLabel.BringToFront();
            gameovercontrols.Add(gameOverImage);



            foreach (var enemy in enemies)
            {
                enemy.StopMovement(); // Tüm düşmanları durdur
            }


        }



    }
}
