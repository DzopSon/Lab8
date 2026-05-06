namespace Lab8
{
    partial class Poker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnP1 = new Button();
            btnP2 = new Button();
            btnO1 = new Button();
            btnO2 = new Button();
            btnC1 = new Button();
            btnC2 = new Button();
            btnC3 = new Button();
            btnC5 = new Button();
            btnC4 = new Button();
            numBet = new NumericUpDown();
            lblPlayerMoney = new Label();
            lblOppMoney = new Label();
            lblPot = new Label();
            lblStatus = new Label();
            btnFold = new Button();
            btnDeal = new Button();
            btnCall = new Button();
            btnRaise = new Button();
            btnRules = new Button();
            ((System.ComponentModel.ISupportInitialize)numBet).BeginInit();
            SuspendLayout();
            // 
            // btnP1
            // 
            btnP1.Location = new Point(260, 335);
            btnP1.Name = "btnP1";
            btnP1.Size = new Size(94, 29);
            btnP1.TabIndex = 0;
            btnP1.Text = "btnP1";
            btnP1.UseVisualStyleBackColor = true;
            // 
            // btnP2
            // 
            btnP2.Location = new Point(439, 335);
            btnP2.Name = "btnP2";
            btnP2.Size = new Size(94, 29);
            btnP2.TabIndex = 1;
            btnP2.Text = "btnP2";
            btnP2.UseVisualStyleBackColor = true;
            // 
            // btnO1
            // 
            btnO1.Location = new Point(249, 36);
            btnO1.Name = "btnO1";
            btnO1.Size = new Size(94, 29);
            btnO1.TabIndex = 2;
            btnO1.Text = "btnO1";
            btnO1.UseVisualStyleBackColor = true;
            // 
            // btnO2
            // 
            btnO2.Location = new Point(430, 36);
            btnO2.Name = "btnO2";
            btnO2.Size = new Size(94, 29);
            btnO2.TabIndex = 3;
            btnO2.Text = "btnO2";
            btnO2.UseVisualStyleBackColor = true;
            // 
            // btnC1
            // 
            btnC1.Location = new Point(104, 172);
            btnC1.Name = "btnC1";
            btnC1.Size = new Size(94, 29);
            btnC1.TabIndex = 4;
            btnC1.Text = "Karta_1";
            btnC1.UseVisualStyleBackColor = true;
            // 
            // btnC2
            // 
            btnC2.Location = new Point(249, 172);
            btnC2.Name = "btnC2";
            btnC2.Size = new Size(94, 29);
            btnC2.TabIndex = 5;
            btnC2.Text = "Karta_2";
            btnC2.UseVisualStyleBackColor = true;
            // 
            // btnC3
            // 
            btnC3.Location = new Point(389, 172);
            btnC3.Name = "btnC3";
            btnC3.Size = new Size(94, 29);
            btnC3.TabIndex = 6;
            btnC3.Text = "Karta_3";
            btnC3.UseVisualStyleBackColor = true;
            // 
            // btnC5
            // 
            btnC5.Location = new Point(643, 172);
            btnC5.Name = "btnC5";
            btnC5.Size = new Size(94, 29);
            btnC5.TabIndex = 7;
            btnC5.Text = "Karta_5";
            btnC5.UseVisualStyleBackColor = true;
            // 
            // btnC4
            // 
            btnC4.Location = new Point(523, 172);
            btnC4.Name = "btnC4";
            btnC4.Size = new Size(94, 29);
            btnC4.TabIndex = 7;
            btnC4.Text = "Karta_4";
            btnC4.UseVisualStyleBackColor = true;
            // 
            // numBet
            // 
            numBet.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numBet.Location = new Point(561, 396);
            numBet.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numBet.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numBet.Name = "numBet";
            numBet.Size = new Size(150, 27);
            numBet.TabIndex = 12;
            numBet.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblPlayerMoney
            // 
            lblPlayerMoney.AutoSize = true;
            lblPlayerMoney.Location = new Point(594, 335);
            lblPlayerMoney.Name = "lblPlayerMoney";
            lblPlayerMoney.Size = new Size(111, 20);
            lblPlayerMoney.TabIndex = 13;
            lblPlayerMoney.Text = "lblPlayerMoney";
            // 
            // lblOppMoney
            // 
            lblOppMoney.AutoSize = true;
            lblOppMoney.Location = new Point(594, 45);
            lblOppMoney.Name = "lblOppMoney";
            lblOppMoney.Size = new Size(100, 20);
            lblOppMoney.TabIndex = 14;
            lblOppMoney.Text = "lblOppMoney";
            // 
            // lblPot
            // 
            lblPot.AutoSize = true;
            lblPot.Location = new Point(389, 272);
            lblPot.Name = "lblPot";
            lblPot.Size = new Size(30, 20);
            lblPot.TabIndex = 15;
            lblPot.Text = "Pot";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(389, 455);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(66, 20);
            lblStatus.TabIndex = 16;
            lblStatus.Text = "lblStatus";
            // 
            // btnFold
            // 
            btnFold.Location = new Point(38, 408);
            btnFold.Name = "btnFold";
            btnFold.Size = new Size(94, 29);
            btnFold.TabIndex = 17;
            btnFold.Text = "Pasuj";
            btnFold.UseVisualStyleBackColor = true;
            // 
            // btnDeal
            // 
            btnDeal.Location = new Point(413, 408);
            btnDeal.Name = "btnDeal";
            btnDeal.Size = new Size(94, 29);
            btnDeal.TabIndex = 18;
            btnDeal.Text = "Nowe Rozdanie";
            btnDeal.UseVisualStyleBackColor = true;
            // 
            // btnCall
            // 
            btnCall.Location = new Point(147, 408);
            btnCall.Name = "btnCall";
            btnCall.Size = new Size(149, 29);
            btnCall.TabIndex = 18;
            btnCall.Text = "Czekaj / Sprawdź";
            btnCall.UseVisualStyleBackColor = true;
            // 
            // btnRaise
            // 
            btnRaise.Location = new Point(302, 408);
            btnRaise.Name = "btnRaise";
            btnRaise.Size = new Size(94, 29);
            btnRaise.TabIndex = 19;
            btnRaise.Text = "Podbij";
            btnRaise.UseVisualStyleBackColor = true;
            // 
            // btnRules
            // 
            btnRules.Location = new Point(617, 446);
            btnRules.Name = "btnRules";
            btnRules.Size = new Size(94, 29);
            btnRules.TabIndex = 20;
            btnRules.Text = "Zasady";
            btnRules.UseVisualStyleBackColor = true;
            // 
            // Poker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 513);
            Controls.Add(btnRules);
            Controls.Add(btnRaise);
            Controls.Add(btnCall);
            Controls.Add(btnDeal);
            Controls.Add(btnFold);
            Controls.Add(lblStatus);
            Controls.Add(lblPot);
            Controls.Add(lblOppMoney);
            Controls.Add(lblPlayerMoney);
            Controls.Add(numBet);
            Controls.Add(btnC4);
            Controls.Add(btnC5);
            Controls.Add(btnC3);
            Controls.Add(btnC2);
            Controls.Add(btnC1);
            Controls.Add(btnO2);
            Controls.Add(btnO1);
            Controls.Add(btnP2);
            Controls.Add(btnP1);
            Name = "Poker";
            Text = "Poker";
            ((System.ComponentModel.ISupportInitialize)numBet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnP1;
        private Button btnP2;
        private Button btnO1;
        private Button btnO2;
        private Button btnC1;
        private Button btnC2;
        private Button btnC3;
        private Button btnC5;
        private Button btnC4;
        private NumericUpDown numBet;
        private Label lblPlayerMoney;
        private Label lblOppMoney;
        private Label lblPot;
        private Label lblStatus;
        private Button btnFold;
        private Button btnDeal;
        private Button btnCall;
        private Button btnRaise;
        private Button btnRules;
    }
}