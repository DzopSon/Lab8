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
    public partial class Poker : Form
    {
        enum Suit { Kier, Karo, Trefl, Pik }
        enum Rank { Dwa = 2, Trzy, Cztery, Piec, Szesc, Siedem, Osiem, Dziewiec, Dziesiec, Walet, Dama, Krol, As }

        class Card
        {
            public Suit Suit { get; set; }
            public Rank Rank { get; set; }
            public override string ToString()
            {
                string s = Suit == Suit.Kier ? "♥" : Suit == Suit.Karo ? "♦" : Suit == Suit.Trefl ? "♣" : "♠";
                string r;
                switch (Rank)
                {
                    case Rank.Walet: r = "J"; break;
                    case Rank.Dama: r = "Q"; break;
                    case Rank.Krol: r = "K"; break;
                    case Rank.As: r = "A"; break;
                    default: r = ((int)Rank).ToString(); break;
                }
                return $"{r}\n{s}";
            }
        }

        List<Card> deck = new List<Card>();
        List<Card> playerHole = new List<Card>();
        List<Card> oppHole = new List<Card>();
        List<Card> community = new List<Card>();

        Button[] playerButtons;
        Button[] oppButtons;
        Button[] communityButtons;

        string playerName;
        int playerMoney = 1000;
        int oppMoney = 1000;
        int pot = 0;
        int currentBetToMatch = 0;
        int gamePhase = -1;
        bool revealOppCards = false;

        public Poker(Player? currentPlayer = null)
        {
            InitializeComponent();

            playerName = currentPlayer?.Name ?? "Brak";

            FormatUI();
            StartNewHand();
        }

        void FormatUI()
        {
            this.BackColor = Color.DarkGreen;
            this.ClientSize = new Size(950, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Texas Hold'em - Gra o wszystko!";

            playerButtons = new Button[] { btnP1, btnP2 };
            oppButtons = new Button[] { btnO1, btnO2 };
            communityButtons = new Button[] { btnC1, btnC2, btnC3, btnC4, btnC5 };

            List<Button> allCards = new List<Button>();
            allCards.AddRange(playerButtons);
            allCards.AddRange(oppButtons);
            allCards.AddRange(communityButtons);

            foreach (var btn in allCards)
            {
                btn.Font = new Font("Arial", 16, FontStyle.Bold);
                btn.BackColor = Color.Navy;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.Size = new Size(80, 120);
                btn.Text = "🂠";
            }

            btnO1.Location = new Point(385, 40);
            btnO2.Location = new Point(485, 40);

            btnC1.Location = new Point(235, 220);
            btnC2.Location = new Point(335, 220);
            btnC3.Location = new Point(435, 220);
            btnC4.Location = new Point(535, 220);
            btnC5.Location = new Point(635, 220);

            btnP1.Location = new Point(385, 440);
            btnP2.Location = new Point(485, 440);

            lblOppMoney.Location = new Point(600, 80);
            lblPlayerMoney.Location = new Point(600, 490);
            lblPlayerMoney.ForeColor = Color.LightGreen;
            lblOppMoney.ForeColor = Color.LightCoral;
            lblPlayerMoney.Font = new Font("Arial", 12, FontStyle.Bold);
            lblOppMoney.Font = new Font("Arial", 12, FontStyle.Bold);
            lblPlayerMoney.AutoSize = true;
            lblOppMoney.AutoSize = true;

            lblPot.AutoSize = false;
            lblPot.Size = new Size(300, 45);
            lblPot.Location = new Point(325, 360);
            lblPot.TextAlign = ContentAlignment.MiddleCenter;
            lblPot.ForeColor = Color.Gold;
            lblPot.BackColor = Color.FromArgb(0, 64, 0);
            lblPot.Font = new Font("Arial", 18, FontStyle.Bold);

            Button[] actions = { btnFold, btnCall, btnRaise, btnDeal };
            foreach (var b in actions)
            {
                b.Font = new Font("Arial", 12, FontStyle.Bold);
                b.BackColor = Color.Gold;
                b.FlatStyle = FlatStyle.Flat;
                b.Cursor = Cursors.Hand;
            }

            btnFold.Text = "Pasuj";
            btnCall.Text = "Czekaj/Sprawdź";
            btnRaise.Text = "Podbij";
            btnDeal.Text = "Następna Runda";

            btnFold.Location = new Point(120, 600);
            btnFold.Size = new Size(100, 45);

            btnCall.Location = new Point(230, 600);
            btnCall.Size = new Size(180, 45);

            btnRaise.Location = new Point(420, 600);
            btnRaise.Size = new Size(100, 45);

            numBet.Location = new Point(530, 608);
            numBet.Size = new Size(80, 30);
            numBet.Font = new Font("Arial", 14, FontStyle.Bold);

            btnDeal.Location = new Point(620, 600);
            btnDeal.Size = new Size(190, 45);

            lblStatus.AutoSize = false;
            lblStatus.Size = new Size(800, 80);
            lblStatus.Location = new Point(75, 660);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            lblStatus.ForeColor = Color.White;
            lblStatus.Font = new Font("Arial", 14, FontStyle.Bold);

            btnRules.Text = "Zasady i Kombinacje";
            btnRules.Location = new Point(730, 20);
            btnRules.Size = new Size(180, 35);
            btnRules.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRules.BackColor = Color.LightBlue;
            btnRules.FlatStyle = FlatStyle.Flat;
            btnRules.Cursor = Cursors.Hand;
            btnRules.Click += btnRules_Click;

            btnFold.Click += btnFold_Click;
            btnCall.Click += btnCall_Click;
            btnRaise.Click += btnRaise_Click;
            btnDeal.Click += btnDeal_Click;
        }

        private void btnRules_Click(object sender, EventArgs e)
        {
            string zasady = "ZASADY TEXAS HOLD'EM:\n" +
                            "1. Każdy otrzymuje 2 zakryte karty.\n" +
                            "2. Na stół wykładanych jest 5 kart wspólnych.\n" +
                            "3. Wygrywa najlepszy 5-kartowy układ (z 7 dostępnych kart).\n\n" +
                            "HIERARCHIA KOMBINACJI (od najsłabszej):\n" +
                            "1. Wysoka karta - brak par, wygrywa najwyższa karta.\n" +
                            "2. Para - dwie takie same karty (np. 8, 8).\n" +
                            "3. Dwie pary - (np. 8, 8 i K, K).\n" +
                            "4. Trójka - trzy takie same karty.\n" +
                            "5. Strit - 5 kart po kolei (np. 5,6,7,8,9).\n" +
                            "6. Kolor - 5 kart w jednym kolorze (np. same piki).\n" +
                            "7. Full - trójka i para w jednym.\n" +
                            "8. Kareta - 4 takie same karty.\n" +
                            "9. Poker - strit w jednym kolorze.\n" +
                            "10. Poker Królewski - 10, J, Q, K, A w jednym kolorze.\n\n" +
                            "ALL-IN: Jeśli komuś skończą się żetony, można tylko 'Czekać', aż do pokazania kart.";

            MessageBox.Show(zasady, "Zasady Gry i Kombinacje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void ResetDeck()
        {
            deck.Clear();
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                    deck.Add(new Card { Suit = s, Rank = r });
            Random rng = new Random();
            deck = deck.OrderBy(x => rng.Next()).ToList();
        }

        void ResetTournament()
        {
            playerMoney = 1000;
            oppMoney = 1000;
            StartNewHand();
        }

        void StartNewHand()
        {
            ResetDeck();
            playerHole.Clear();
            oppHole.Clear();
            community.Clear();
            pot = 0;
            currentBetToMatch = 0;
            gamePhase = 0;
            revealOppCards = false;

            int blind = 10;
            int playerBlind = Math.Min(blind, playerMoney);
            int oppBlind = Math.Min(blind, oppMoney);

            playerMoney -= playerBlind;
            oppMoney -= oppBlind;
            pot += playerBlind + oppBlind;

            playerHole.Add(deck[0]); deck.RemoveAt(0);
            oppHole.Add(deck[0]); deck.RemoveAt(0);
            playerHole.Add(deck[0]); deck.RemoveAt(0);
            oppHole.Add(deck[0]); deck.RemoveAt(0);

            for (int i = 0; i < 5; i++)
            {
                community.Add(deck[0]);
                deck.RemoveAt(0);
            }

            UpdateUI();
            btnDeal.Enabled = false;
            EnableActionButtons(true);

            if (playerMoney == 0 || oppMoney == 0)
                lblStatus.Text = "Ktoś jest ALL-IN! Klikaj 'Czekaj', by zobaczyć resztę kart.";
            else
                lblStatus.Text = "Twój ruch. Co robisz?";
        }

        void UpdateUI()
        {
            lblPlayerMoney.Text = $"Twoje żetony: {playerMoney}$";
            lblOppMoney.Text = $"Żetony Bota: {oppMoney}$";
            lblPot.Text = $"[ PULA: {pot}$ ]";

            for (int i = 0; i < 2; i++)
            {
                playerButtons[i].BackColor = Color.White;
                playerButtons[i].Text = playerHole[i].ToString();
                playerButtons[i].ForeColor = (playerHole[i].Suit == Suit.Kier || playerHole[i].Suit == Suit.Karo) ? Color.Red : Color.Black;
            }

            if (gamePhase < 4 && !revealOppCards)
            {
                for (int i = 0; i < 2; i++) { oppButtons[i].Text = "🂠"; oppButtons[i].BackColor = Color.Navy; oppButtons[i].ForeColor = Color.White; }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    oppButtons[i].BackColor = Color.White;
                    oppButtons[i].Text = oppHole[i].ToString();
                    oppButtons[i].ForeColor = (oppHole[i].Suit == Suit.Kier || oppHole[i].Suit == Suit.Karo) ? Color.Red : Color.Black;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if ((gamePhase >= 1 && i < 3) || (gamePhase >= 2 && i < 4) || (gamePhase >= 3 && i < 5))
                {
                    communityButtons[i].BackColor = Color.White;
                    communityButtons[i].Text = community[i].ToString();
                    communityButtons[i].ForeColor = (community[i].Suit == Suit.Kier || community[i].Suit == Suit.Karo) ? Color.Red : Color.Black;
                }
                else
                {
                    communityButtons[i].Text = "🂠";
                    communityButtons[i].BackColor = Color.Navy;
                    communityButtons[i].ForeColor = Color.White;
                }
            }
        }

        void EnableActionButtons(bool enabled)
        {
            btnFold.Enabled = enabled;
            btnCall.Enabled = enabled;

            bool canRaise = enabled && playerMoney > 0 && oppMoney > 0;
            btnRaise.Enabled = canRaise;
            numBet.Enabled = canRaise;
        }

        private void btnFold_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Spasowałeś! Runda przerwana, Bot zgarnia pulę.";
            oppMoney += pot;
            pot = 0;
            revealOppCards = true;
            EndHand();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (currentBetToMatch > 0)
            {
                if (playerMoney < currentBetToMatch) currentBetToMatch = playerMoney;
                playerMoney -= currentBetToMatch;
                pot += currentBetToMatch;
                lblStatus.Text = $"Sprawdzasz za {currentBetToMatch}$.";
                currentBetToMatch = 0;
            }
            else
            {
                lblStatus.Text = "Czekasz.";
            }

            BotTurn(false, 0);
        }

        private void btnRaise_Click(object sender, EventArgs e)
        {
            int amount = (int)numBet.Value;

            if (amount > playerMoney) amount = playerMoney;
            if (amount > oppMoney) amount = oppMoney;

            if (amount <= 0) return;

            playerMoney -= amount;
            pot += amount;
            lblStatus.Text = $"Podbijasz o {amount}$!";

            BotTurn(true, amount);
        }

        void BotTurn(bool playerRaised, int raiseAmount)
        {
            UpdateUI();

            if (playerRaised)
            {
                Random r = new Random();
                if (r.Next(100) > 30)
                {
                    if (oppMoney < raiseAmount) raiseAmount = oppMoney;
                    oppMoney -= raiseAmount;
                    pot += raiseAmount;
                    MessageBox.Show($"Bot sprawdza Twoje {raiseAmount}$!", "Ruch Bota");
                }
                else
                {
                    revealOppCards = true;
                    UpdateUI();

                    MessageBox.Show("Bot pasuje! Wygrywasz pulę!", "Ruch Bota");
                    playerMoney += pot;
                    pot = 0;
                    lblStatus.Text = "Bot spasował. Zobacz z czym blefował!";
                    EndHand();
                    return;
                }
            }
            else
            {
                if (oppMoney > 0 && playerMoney > 0)
                {
                    MessageBox.Show("Bot czeka.", "Ruch Bota");
                }
            }

            AdvanceGamePhase();
        }

        void AdvanceGamePhase()
        {
            gamePhase++;
            currentBetToMatch = 0;
            numBet.Value = 10;

            if (gamePhase == 4) Showdown();
            else UpdateUI();
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            if (playerMoney <= 0 || oppMoney <= 0)
            {
                ResetTournament();
            }
            else
            {
                StartNewHand();
            }
        }

        void EndHand()
        {
            EnableActionButtons(false);
            btnDeal.Enabled = true;

            if (playerMoney <= 0)
            {
                lblStatus.Text += "\nZBANKRUTOWAŁEŚ! Koniec gry.";
                btnDeal.Text = "Zagraj od nowa";
                GameHistory.AddEntry(new HistoryEntry(playerName, "Poker", "Przegrana (Bankrut)", DateTime.Now));
            }
            else if (oppMoney <= 0)
            {
                lblStatus.Text += "\nBOT ZBANKRUTOWAŁ! Wygrywasz turniej!";
                btnDeal.Text = "Zagraj od nowa";
                GameHistory.AddEntry(new HistoryEntry(playerName, "Poker", "Wygrana", DateTime.Now));
            }
            else
            {
                btnDeal.Text = "Następna Runda";
            }

            UpdateUI();
        }

        void Showdown()
        {
            UpdateUI();

            string playerHandName, oppHandName;

            int playerScore = Evaluate7Cards(playerHole, community, out playerHandName);
            int oppScore = Evaluate7Cards(oppHole, community, out oppHandName);

            if (playerScore > oppScore)
            {
                lblStatus.Text = $"Ty: {playerHandName} \nBot: {oppHandName} \nWYGRYWASZ {pot}$!";
                playerMoney += pot;
            }
            else if (oppScore > playerScore)
            {
                lblStatus.Text = $"Ty: {playerHandName} \nBot: {oppHandName} \nPRZEGRYWASZ!";
                oppMoney += pot;
            }
            else
            {
                lblStatus.Text = $"REMIS! Obaj macie: {playerHandName}.";
                playerMoney += pot / 2;
                oppMoney += pot / 2;
            }

            pot = 0;
            EndHand();
        }

        int Evaluate7Cards(List<Card> hole, List<Card> board, out string bestName)
        {
            List<Card> all7 = new List<Card>();
            all7.AddRange(hole);
            all7.AddRange(board);

            int bestScore = -1;
            bestName = "";

            for (int i = 0; i < 7; i++)
            {
                for (int j = i + 1; j < 7; j++)
                {
                    List<Card> temp5 = new List<Card>(all7);
                    temp5.RemoveAt(j);
                    temp5.RemoveAt(i);

                    string currentName;
                    int score = GetHandScore(temp5.ToArray(), out currentName);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestName = currentName;
                    }
                }
            }
            return bestScore;
        }

        private string GetRankString(Rank r)
        {
            switch (r)
            {
                case Rank.Walet: return "J";
                case Rank.Dama: return "Q";
                case Rank.Krol: return "K";
                case Rank.As: return "A";
                default: return ((int)r).ToString();
            }
        }

        private int GetHandScore(Card[] handToEval, out string handName)
        {
            var groups = handToEval.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
            bool flush = handToEval.GroupBy(c => c.Suit).Count() == 1;
            var orderedRanks = handToEval.Select(c => (int)c.Rank).Distinct().OrderBy(r => r).ToList();

            bool straight = orderedRanks.Count == 5 && orderedRanks.Last() - orderedRanks.First() == 4;
            bool lowAceStraight = false;

            if (orderedRanks.Count == 5 && orderedRanks.Contains((int)Rank.As) && orderedRanks.Contains((int)Rank.Dwa) && orderedRanks.Contains((int)Rank.Piec))
            {
                if (orderedRanks[0] == 2 && orderedRanks[1] == 3 && orderedRanks[2] == 4 && orderedRanks[3] == 5 && orderedRanks[4] == 14)
                {
                    straight = true;
                    lowAceStraight = true;
                }
            }

            int mainVal = (int)groups[0].Key;
            int secVal = groups.Count > 1 ? (int)groups[1].Key : 0;
            Rank highRank = lowAceStraight ? Rank.Piec : (Rank)orderedRanks.Last();
            int highCard = (int)highRank;

            string mainStr = GetRankString(groups[0].Key);
            string secStr = groups.Count > 1 ? GetRankString(groups[1].Key) : "";
            string highStr = GetRankString(highRank);

            if (straight && flush && orderedRanks.Contains((int)Rank.As) && orderedRanks.Contains((int)Rank.Krol))
            { handName = "Poker Królewski"; return 9000; }
            if (straight && flush)
            { handName = $"Poker (do {highStr})"; return 8000 + highCard; }
            if (groups[0].Count() == 4)
            { handName = $"Kareta z {mainStr}"; return 7000 + mainVal; }
            if (groups[0].Count() == 3 && groups[1].Count() == 2)
            { handName = $"Full ({mainStr} na {secStr})"; return 6000 + (mainVal * 10) + secVal; }
            if (flush)
            { handName = $"Kolor (wysoka {highStr})"; return 5000 + highCard; }
            if (straight)
            { handName = $"Strit (do {highStr})"; return 4000 + highCard; }
            if (groups[0].Count() == 3)
            { handName = $"Trójka z {mainStr}"; return 3000 + mainVal; }
            if (groups[0].Count() == 2 && groups[1].Count() == 2)
            { handName = $"Dwie Pary ({mainStr} i {secStr})"; return 2000 + (mainVal * 10) + secVal; }
            if (groups[0].Count() == 2)
            { handName = $"Para z {mainStr}"; return 1000 + mainVal; }

            handName = $"Wysoka karta ({highStr})";
            return highCard;
        }
    }
}