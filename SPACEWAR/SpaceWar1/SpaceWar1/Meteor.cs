using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceWar1
{
    public class Meteor
    {
        private int Speed;
        private int Damage;
        private Game game;
        public PictureBox meteorImg;
        public Point Position;

        public Meteor(Game game, int sizeType, int damage)
        {
            this.game = game;
            this.Damage = damage; 

            
            if (sizeType == 0) 
            {
                Speed = 5;
                meteorImg = new PictureBox
                {
                    Size = new Size(40, 40),
                    Image = Image.FromFile("Resources/MeteorSmall.png"),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
            }
            else 
            {
                Speed = 3;
                meteorImg = new PictureBox
                {
                    Size = new Size(80, 80),
                    Image = Image.FromFile("Resources/MeteorLarge.png"),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
            }

            game.form.Controls.Add(meteorImg);
        }


        public int getDamage()
        {
            return Damage;
        }

        public void Move()
        {
            // Çapraz hareket
            Position = new Point(Position.X + 1, Position.Y + Speed); // Sağ alt çapraz
            meteorImg.Location = Position;

           
            if (Position.Y > game.form.ClientSize.Height || Position.X > game.form.ClientSize.Width)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            game.form.Controls.Remove(meteorImg); // Ekrandan kaldır
            game.meteors.Remove(this); // Listeden kaldır
            meteorImg.Dispose(); // Hafızadan kaldır
        }
    }
}

