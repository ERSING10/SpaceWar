using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace SpaceWar1
{
    public class Powerup
    {
        private PictureBox powerUpImg; 
        private Game game; 
        private string type; 
        private int value;  
        private System.Windows.Forms.Timer timer; // 5 saniye zamanlayıcı
        private Random random = new Random();
        private int counter = 0, typeNum;
        private bool isTaken = false;

        public Powerup(Game game)
        {
            typeNum = this.random.Next(1, 4);
            setType();
            this.game = game;
            
            
            powerUpImg = new PictureBox
            {
                Size = new Size(50, 50),
                BackColor = Color.Transparent,
                Image = Image.FromFile($"Resources/{type}Powerup.png"), 
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            
            Random random = new Random();
            int spawnX = random.Next(0, game.form.ClientSize.Width - powerUpImg.Width);
            int spawnY = random.Next(0, game.form.ClientSize.Height - powerUpImg.Height);
            powerUpImg.Location = new Point(spawnX, spawnY);

            game.form.Controls.Add(powerUpImg);

            // zamanlayıcı başlat
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; 
            timer.Tick += powerupTimer; // Süre dolunca yok et
            timer.Start();
        }

        // Oyuncu ile çarpışma kontrolü
        public void CheckCollision(Spaceship player)
        {
            if (player.GetSpaceshipImg().Bounds.IntersectsWith(powerUpImg.Bounds))
            {
                ApplyEffect(); // Etki uygula
                powerUpImg.Visible = false; // Yok et
            }
        }
        private void setType()
        {
            if (typeNum == 1) type = "health";
            if (typeNum == 2) type = "speed";
            if (typeNum == 3) type = "damage";
        }


        
        private void ApplyEffect()// Etki uygulama
        {
            switch (typeNum)
            {
                case 1: 
                    if(game.player.getHealth() >= 90)
                    {
                        game.player.setHealth(100);
                    }
                    else game.player.setHealth(game.player.getHealth() + 10);
                    break;

                case 2:
                    game.player.setSpeed(11);
                    break;

                case 3:
                    game.player.setDamage(2);
                    break;
            }
            isTaken = true;
        }
        private void UndoEffect()
        {
            if(typeNum == 2)
             {
                game.player.setSpeed(10);
            }
            if(typeNum == 3)
            {
                game.player.setDamage(1);
            }
            isTaken=false;
        }

        private void powerupTimer(object sender, EventArgs e)
        {
            if(isTaken)
            {
                if(counter == 10)
                {
                    UndoEffect();
                    Destroy();
                }
                else
                {
                    counter++;
                }
            }
            else if(counter == 5) Destroy();
            else counter++;
        }
        // Power-Up'ı yok etme
        private void Destroy()
        {
            timer.Stop();
            timer.Dispose();
            game.form.Controls.Remove(powerUpImg);
            powerUpImg.Dispose();
        }
    }
}
