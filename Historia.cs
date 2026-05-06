using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab8
{
    public partial class Historia : Form
    {
        public Historia()
        {
            InitializeComponent();

            // wire button
            button1.Click += Button1_Click;

            // populate existing entries (without date)
            LoadHistory();

            // subscribe to future entries
            GameHistory.EntryAdded += GameHistory_EntryAdded;
            FormClosed += Historia_FormClosed;
        }

        private void LoadHistory()
        {
            dataGridView1.Rows.Clear();
            foreach (var e in GameHistory.Entries.OrderBy(x => x.Time))
            {
                dataGridView1.Rows.Add(e.Player, e.Game, e.Result);
            }
        }

        private void GameHistory_EntryAdded(object? sender, HistoryEntry e)
        {
            // ensure we're on UI thread and add only the result (no date)
            if (InvokeRequired)
            {
                Invoke(new Action(() => dataGridView1.Rows.Add(e.Player, e.Game, e.Result)));
            }
            else
            {
                dataGridView1.Rows.Add(e.Player, e.Game, e.Result);
            }
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void Historia_FormClosed(object? sender, FormClosedEventArgs e)
        {
            GameHistory.EntryAdded -= GameHistory_EntryAdded;
        }
    }
}
