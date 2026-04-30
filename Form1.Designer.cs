namespace Lab8
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
            BlackJack = new Button();
            Poker = new Button();
            button3 = new Button();
            Dod = new Button();
            His = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // BlackJack
            // 
            BlackJack.Location = new Point(37, 115);
            BlackJack.Name = "BlackJack";
            BlackJack.Size = new Size(203, 66);
            BlackJack.TabIndex = 0;
            BlackJack.Text = "Blackjack";
            BlackJack.UseVisualStyleBackColor = true;
            BlackJack.Click += BlackJack_Click;
            // 
            // Poker
            // 
            Poker.Location = new Point(298, 115);
            Poker.Name = "Poker";
            Poker.Size = new Size(203, 66);
            Poker.TabIndex = 1;
            Poker.Text = "Poker";
            Poker.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(559, 115);
            button3.Name = "button3";
            button3.Size = new Size(203, 66);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // Dod
            // 
            Dod.Location = new Point(298, 244);
            Dod.Name = "Dod";
            Dod.Size = new Size(203, 34);
            Dod.TabIndex = 3;
            Dod.Text = "Dodaj gracza";
            Dod.UseVisualStyleBackColor = true;
            // 
            // His
            // 
            His.Location = new Point(298, 347);
            His.Name = "His";
            His.Size = new Size(203, 30);
            His.TabIndex = 4;
            His.Text = "Historia";
            His.UseVisualStyleBackColor = true;
            His.Click += His_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 34);
            label1.Name = "label1";
            label1.Size = new Size(113, 20);
            label1.TabIndex = 5;
            label1.Text = "Aktualny gracz: ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(His);
            Controls.Add(Dod);
            Controls.Add(button3);
            Controls.Add(Poker);
            Controls.Add(BlackJack);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BlackJack;
        private Button Poker;
        private Button button3;
        private Button Dod;
        private Button His;
        private Label label1;
    }
}
