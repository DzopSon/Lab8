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
            Uno_Button = new Button();
            Dod = new Button();
            His = new Button();
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
            // 
            // Poker
            // 
            Poker.Location = new Point(298, 115);
            Poker.Name = "Poker";
            Poker.Size = new Size(203, 66);
            Poker.TabIndex = 1;
            Poker.Text = "Poker";
            Poker.UseVisualStyleBackColor = true;
            Poker.Click += Poker_Click;
            // 
            // Uno_Button
            // 
            Uno_Button.Location = new Point(559, 115);
            Uno_Button.Name = "Uno_Button";
            Uno_Button.Size = new Size(203, 66);
            Uno_Button.TabIndex = 2;
            Uno_Button.Text = "Uno";
            Uno_Button.UseVisualStyleBackColor = true;
            Uno_Button.Click += Uno_Button_Click;
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
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(His);
            Controls.Add(Dod);
            Controls.Add(Uno_Button);
            Controls.Add(Poker);
            Controls.Add(BlackJack);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button BlackJack;
        private Button Poker;
        private Button Uno_Button;
        private Button Dod;
        private Button His;
    }
}
