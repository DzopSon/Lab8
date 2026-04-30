namespace Lab8
{
    partial class Historia
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
            dataGridView1 = new DataGridView();
            button1 = new Button();
            Gracz = new DataGridViewTextBoxColumn();
            Gra = new DataGridViewTextBoxColumn();
            Wynik = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Gracz, Gra, Wynik });
            dataGridView1.Location = new Point(81, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(653, 348);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(320, 386);
            button1.Name = "button1";
            button1.Size = new Size(147, 52);
            button1.TabIndex = 1;
            button1.Text = "Wyjdź";
            button1.UseVisualStyleBackColor = true;
            // 
            // Gracz
            // 
            Gracz.HeaderText = "Gracz";
            Gracz.MinimumWidth = 6;
            Gracz.Name = "Gracz";
            Gracz.Width = 200;
            // 
            // Gra
            // 
            Gra.HeaderText = "Gra";
            Gra.MinimumWidth = 6;
            Gra.Name = "Gra";
            Gra.Width = 200;
            // 
            // Wynik
            // 
            Wynik.HeaderText = "Wynik";
            Wynik.MinimumWidth = 6;
            Wynik.Name = "Wynik";
            Wynik.Width = 200;
            // 
            // Historia
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "Historia";
            Text = "Historia";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private DataGridViewTextBoxColumn Gracz;
        private DataGridViewTextBoxColumn Gra;
        private DataGridViewTextBoxColumn Wynik;
    }
}