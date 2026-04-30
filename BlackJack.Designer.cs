namespace Lab8
{
    partial class BlackJack
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
            button1 = new Button();
            button2 = new Button();
            newRoundButton = new Button();
            dealerPanel = new Panel();
            playerPanel = new Panel();
            dealerTotalLabel = new Label();
            playerTotalLabel = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(42, 381);
            button1.Name = "button1";
            button1.Size = new Size(331, 57);
            button1.TabIndex = 0;
            button1.Text = "Dobierz";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(426, 381);
            button2.Name = "button2";
            button2.Size = new Size(362, 57);
            button2.TabIndex = 1;
            button2.Text = "Pass";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // newRoundButton
            // 
            newRoundButton.Location = new Point(340, 322);
            newRoundButton.Name = "newRoundButton";
            newRoundButton.Size = new Size(120, 40);
            newRoundButton.TabIndex = 2;
            newRoundButton.Text = "Nowa runda";
            newRoundButton.UseVisualStyleBackColor = true;
            newRoundButton.Click += newRoundButton_Click;
            // 
            // dealerPanel
            // 
            dealerPanel.Location = new Point(20, 20);
            dealerPanel.Name = "dealerPanel";
            dealerPanel.Size = new Size(760, 120);
            dealerPanel.TabIndex = 3;
            // 
            // playerPanel
            // 
            playerPanel.Location = new Point(20, 150);
            playerPanel.Name = "playerPanel";
            playerPanel.Size = new Size(760, 150);
            playerPanel.TabIndex = 4;
            // 
            // dealerTotalLabel
            // 
            dealerTotalLabel.AutoSize = true;
            dealerTotalLabel.Location = new Point(20, 9);
            dealerTotalLabel.Name = "dealerTotalLabel";
            dealerTotalLabel.Size = new Size(68, 20);
            dealerTotalLabel.TabIndex = 5;
            dealerTotalLabel.Text = "Dealer: 0";
            // 
            // playerTotalLabel
            // 
            playerTotalLabel.AutoSize = true;
            playerTotalLabel.Location = new Point(20, 293);
            playerTotalLabel.Name = "playerTotalLabel";
            playerTotalLabel.Size = new Size(83, 20);
            playerTotalLabel.TabIndex = 6;
            playerTotalLabel.Text = "Gracz: 0 (0)";
            // 
            // BlackJack
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(playerTotalLabel);
            Controls.Add(dealerTotalLabel);
            Controls.Add(playerPanel);
            Controls.Add(dealerPanel);
            Controls.Add(newRoundButton);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "BlackJack";
            Text = "BlackJack";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button newRoundButton;
        private System.Windows.Forms.Panel dealerPanel;
        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Label dealerTotalLabel;
        private System.Windows.Forms.Label playerTotalLabel;
    }
}