namespace SpaceWar1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            progressBar1 = new ProgressBar();
            label1 = new Label();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(0, 0);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(225, 40);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Stencil", 16.2F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(267, 7);
            label1.Name = "label1";
            label1.Size = new Size(31, 33);
            label1.TabIndex = 1;
            label1.Text = "0\r\n";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 653);
            Controls.Add(label1);
            Controls.Add(progressBar1);
            Name = "Form1";
            Text = "220229012 ERSİN GÜNERİGÖK";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private ProgressBar progressBar1;
        private Label label1;
    }
}
