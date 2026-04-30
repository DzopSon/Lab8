using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Form1 : Form
    {
        private readonly List<Player> _players = new();
        private int _nextPlayerId = 1;

        public Form1()
        {
            InitializeComponent();
            Dod.Click += Dod_Click;
        }

        private void Dod_Click(object? sender, EventArgs e)
        {
            var dlg = new Dodawanie();
            dlg.PlayerAdded += Dod_PlayerAdded;
            dlg.ShowDialog(this);
            dlg.PlayerAdded -= Dod_PlayerAdded;
        }

        private void Dod_PlayerAdded(object? sender, string name)
        {
            var player = new Player(_nextPlayerId++, name);
            _players.Add(player);
            label1.Text = $"Aktualny gracz: {player.Name} (ID: {player.Id})";
        }
    }
}
