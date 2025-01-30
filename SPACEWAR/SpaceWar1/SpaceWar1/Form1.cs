using System.Windows.Forms;

namespace SpaceWar1
{
    public partial class Form1 : Form
    {
        Game game;
        public Form1()
        {
            this.game = new Game(this);
            InitializeComponent();
            timer1.Start();
            timer1.Interval = 16;
            timer1.Tick += timer1_Tick;
            this.BackColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BringToFront();
            progressBar1.BringToFront();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) game.up = true;
            if (e.KeyCode == Keys.Down) game.down = true;
            if (e.KeyCode == Keys.Left) game.left = true;
            if (e.KeyCode == Keys.Right) game.right = true;
            if (e.KeyCode == Keys.Space) game.shoot = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) game.up = false;
            if (e.KeyCode == Keys.Down) game.down = false;
            if (e.KeyCode == Keys.Left) game.left = false;
            if (e.KeyCode == Keys.Right) game.right = false;
        }


        private void timer1_Tick(object? sender, EventArgs e)
        {
            game.UpdateGame();
            if (game.player.getHealth() >= 0)
                progressBar1.Value = game.player.getHealth();

            label1.Text = "" + game.skor;
        }

       
    }
}
