using SpaceWar1;
using System.IO;

public class Bullet
{
    private int Speed = 10; 
    public PictureBox bulletImg; 
    public int Damage;
    public int direction;
    Game game; 
    private bool isEnemy;
    private bool isTracking = true; // Başlangıçta takip modunda
    private int directionX = 0; // Sabit x yönü
    private int directionY = 0; // Sabit y yönü

    public Bullet(Game game , bool isEnemy)
    {
        this.game = game;
        this.isEnemy = isEnemy;
        
        bulletImg = new PictureBox
        {
            BackColor = Color.Transparent,
            Size = new Size(50, 50),
            Image = Image.FromFile(isEnemy ? "Resources/enemyBullet1.png" : "Resources/Bullet2.png"),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        //mermiyi forma ekleme
        game.form.Controls.Add(bulletImg);
        if (isEnemy)
        {
            game.enemyBullets.Add(this);
        }
        else
        {
            game.player.bullets.Add(this);
        }
    }

    public void Move()
    {
        if (direction == 1) 
        {
            
            bulletImg.Location = new Point(bulletImg.Location.X + Speed, bulletImg.Location.Y);

            // Mermi ekranın dışına çıktıysa yok et
            if (bulletImg.Location.X > game.form.ClientSize.Width)
            {
                Destroy();
            }
        }
        else if (direction == -1) 
        {
            if (isTracking) // Takip Modu
            {
                
                int dx = game.player.GetSpaceshipImg().Location.X - bulletImg.Location.X;
                int dy = game.player.GetSpaceshipImg().Location.Y - bulletImg.Location.Y;

                double distance = Math.Sqrt(dx * dx + dy * dy);

                if (distance > 300) // Eğer 300 pikselden uzaksa takip et
                {
                    int stepX = (int)(Speed * dx / distance);
                    int stepY = (int)(Speed * dy / distance);
                    bulletImg.Location = new Point(bulletImg.Location.X + stepX, bulletImg.Location.Y + stepY);
                }
                else
                {
                   
                    isTracking = false;
                    directionX = dx > 0 ? 1 : -1; 
                    directionY = dy > 0 ? 1 : -1; 
                }
            }
            else // Sabit Yön Modu
            {
                bulletImg.Location = new Point(bulletImg.Location.X + directionX * Speed, bulletImg.Location.Y + directionY * Speed);
            }

            // mermiyi yok et
            if (bulletImg.Location.X < 0 || bulletImg.Location.Y < 0 || bulletImg.Location.X > game.form.ClientSize.Width || bulletImg.Location.Y > game.form.ClientSize.Height)
            {
                Destroy();
            }
        }
    }



    public void Destroy()
    {
        game.form.Controls.Remove(bulletImg);
        bulletImg.Dispose();

        // Listeden kaldır
        if (isEnemy)
        {
            game.enemyBullets.Remove(this);
        }
        else
        {
            game.player.bullets.Remove(this);
        }
    }

    public void OnHit(Enemy enemy)
    {
        enemy.TakeDamage(Damage); 
        Destroy(); 
    }

    public Rectangle GetBounds()
    {
        return bulletImg.Bounds; // Çarpışma kontrolü için boyut
    }
}
