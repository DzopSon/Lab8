using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Dodawanie : Form
    {
        public event EventHandler<string>? PlayerAdded;

        public Dodawanie()
        {
            InitializeComponent();
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            var name = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Wpisz nazwę gracza.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PlayerAdded?.Invoke(this, name);
            this.Close();
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            this.Close();
        }
    }
}
