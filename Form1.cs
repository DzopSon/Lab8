namespace Lab8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Poker_Click(object sender, EventArgs e)
        {
            Poker oknoGry = new Poker();
            oknoGry.ShowDialog();
        }

        private void Uno_Button_Click(object sender, EventArgs e)
        {
            Uno graUno = new Uno();

            // 2. Otwieramy okno gry
            graUno.ShowDialog();
        }
    }
}
