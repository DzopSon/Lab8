using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab8
{
    public partial class BlackJack : Form
    {
        private Deck _deck = null!;
        private readonly List<Card> _playerHand = new();
        private readonly List<Card> _dealerHand = new();
        private bool _playerTurn;
        private readonly Player? _currentPlayer;
        private readonly Random _rng = new(); // used for 50% chance
        private bool _dealerCardRevealed; // controls whether dealer's second card stays visible this round

        // Designer requires parameterless ctor; forward to main ctor
        public BlackJack() : this(null) { }

        public BlackJack(Player? currentPlayer)
        {
            InitializeComponent();
            _currentPlayer = currentPlayer;

            // wire the "Podejrzyj kartę" button click
            button3.Click += button3_Click;

            StartNewRound();
        }

        private void StartNewRound()
        {
            _playerHand.Clear();
            _dealerHand.Clear();
            dealerPanel.Controls.Clear();
            playerPanel.Controls.Clear();
            dealerTotalLabel.Text = "Dealer: 0";

            // reset peek state for new round
            _dealerCardRevealed = false;

            // show player name if available
            playerTotalLabel.Text = _currentPlayer is not null
                ? $"Gracz: {_currentPlayer.Name} (0 kart)"
                : "Gracz: 0 (0)";

            _deck = new Deck();
            _deck.Shuffle();

            // initial deal - two each
            _playerHand.Add(_deck.Draw());
            _dealerHand.Add(_deck.Draw());
            _playerHand.Add(_deck.Draw());
            _dealerHand.Add(_deck.Draw());

            _playerTurn = true;
            button1.Enabled = true; // Hit
            button2.Enabled = true; // Stand
            button3.Enabled = true; // allow peek each round

            RenderHands(initialHideDealerSecond: true);
            UpdateTotalsLabel();
        }

        private void RenderHands(bool initialHideDealerSecond)
        {
            dealerPanel.Controls.Clear();
            playerPanel.Controls.Clear();

            const int cardSpacing = 90;
            int x = 10;

            // dealer cards - if initialHideDealerSecond then second card is face-down placeholder
            for (int i = 0; i < _dealerHand.Count; i++)
            {
                var card = _dealerHand[i];
                Control ctrl;
                // If dealer card was revealed by a successful peek we never hide it this round
                if (i == 1 && initialHideDealerSecond && _playerTurn && !_dealerCardRevealed)
                {
                    ctrl = CreateCardBackControl();
                }
                else
                {
                    ctrl = CreateCardControl(card);
                }

                ctrl.Location = new Point(x, 10);
                dealerPanel.Controls.Add(ctrl);
                x += cardSpacing;
            }

            // player cards
            x = 10;
            for (int i = 0; i < _playerHand.Count; i++)
            {
                var card = _playerHand[i];
                var ctrl = CreateCardControl(card);
                ctrl.Location = new Point(x, 10);
                playerPanel.Controls.Add(ctrl);
                x += cardSpacing;
            }
        }

        private Control CreateCardBackControl()
        {
            var p = new Panel
            {
                Size = new Size(80, 110),
                BackColor = Color.FromArgb(50, 50, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            var lbl = new Label
            {
                Text = "",
                ForeColor = Color.White,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font(SystemFonts.DefaultFont.FontFamily, 12, FontStyle.Bold)
            };
            p.Controls.Add(lbl);
            return p;
        }

        private Control CreateCardControl(Card card)
        {
            var p = new Panel
            {
                Size = new Size(80, 110),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            var suitCenter = new Label
            {
                Text = card.Suit,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI Symbol", 22, FontStyle.Regular),
                ForeColor = card.IsRedSuit ? Color.FromArgb(180, Color.Red) : Color.FromArgb(80, Color.Black),
                BackColor = Color.Transparent
            };

            var center = new Label
            {
                Text = card.DisplayRank,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = card.IsRedSuit ? Color.DarkRed : Color.Black,
                BackColor = Color.Transparent
            };

            var topRight = new Label
            {
                Text = card.Suit,
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = card.IsRedSuit ? Color.Red : Color.Black,
                BackColor = Color.Transparent
            };

            // add controls so rank is on top, then bring to front explicitly to ensure visibility
            p.Controls.Add(suitCenter);
            p.Controls.Add(center);
            p.Controls.Add(topRight);

            // ensure rank is above the suit glyph and small suit above both
            center.BringToFront();
            topRight.BringToFront();

            // position top-right after adding (panel has its size)
            topRight.Location = new Point(p.Width - topRight.Width - 6, 4);

            return p;
        }

        private void UpdateTotalsLabel(bool revealDealer = false)
        {
            // if dealer card was revealed, always show dealer totals
            if (_dealerCardRevealed) revealDealer = true;

            var playerVal = HandValue(_playerHand);
            if (_currentPlayer is not null)
            {
                playerTotalLabel.Text = $"Gracz: {_currentPlayer.Name} — {playerVal} ({_playerHand.Count} kart)";
            }
            else
            {
                playerTotalLabel.Text = $"Gracz: {playerVal} ({_playerHand.Count} kart)";
            }

            if (revealDealer)
            {
                var dealerVal = HandValue(_dealerHand);
                dealerTotalLabel.Text = $"Dealer: {dealerVal}";
            }
            else
            {
                // show only first dealer card value (approx)
                var first = _dealerHand.FirstOrDefault();
                if (first != null)
                {
                    var firstVal = first.ValueForTotals();
                    dealerTotalLabel.Text = $"Dealer: {firstVal} + ?";
                }
                else
                {
                    dealerTotalLabel.Text = "Dealer: 0";
                }
            }
        }

        private static int HandValue(IReadOnlyList<Card> hand)
        {
            // sum aces as 1, then upgrade some aces to 11 where possible
            int sum = 0;
            int aces = 0;
            foreach (var c in hand)
            {
                if (c.IsAce)
                {
                    aces++;
                    sum += 1;
                }
                else
                {
                    sum += c.ValueForTotals();
                }
            }

            while (aces > 0 && sum + 10 <= 21)
            {
                sum += 10;
                aces--;
            }

            return sum;
        }

        private void button1_Click(object? sender, EventArgs e)
        {
            if (!_playerTurn) return;
            _playerHand.Add(_deck.Draw());
            // pass same initialHideDealerSecond: true — RenderHands will keep the dealer card visible
            // if _dealerCardRevealed is true
            RenderHands(initialHideDealerSecond: true);
            var playerVal = HandValue(_playerHand);
            UpdateTotalsLabel();
            if (playerVal > 21)
            {
                EndRound("Przegrałeś!");
            }
        }

        private void button2_Click(object? sender, EventArgs e)
        {
            if (!_playerTurn) return;
            _playerTurn = false;
            RenderHands(initialHideDealerSecond: false);
            while (HandValue(_dealerHand) < 17)
            {
                _dealerHand.Add(_deck.Draw());
                RenderHands(initialHideDealerSecond: false);
                Application.DoEvents();
                System.Threading.Thread.Sleep(350);
            }

            var dealerVal = HandValue(_dealerHand);
            var playerVal = HandValue(_playerHand);
            UpdateTotalsLabel(revealDealer: true);

            if (dealerVal > 21)
            {
                EndRound("Wygrałeś!");
                return;
            }

            if (playerVal > dealerVal)
            {
                EndRound("Wygrałeś!");
            }
            else if (playerVal < dealerVal)
            {
                EndRound("Przegrałeś!");
            }
            else
            {
                EndRound("Remis!");
            }
        }

        // "Podejrzyj kartę" — 50% success reveal, 50% dealer notices and player loses
        private void button3_Click(object? sender, EventArgs e)
        {
            if (!_playerTurn) return;

            // need at least two dealer cards (one hidden)
            if (_dealerHand.Count < 2)
            {
                MessageBox.Show("Brak ukrytej karty do podejrzenia.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // disable button to prevent repeated peeks in same round
            button3.Enabled = false;

            var success = _rng.NextDouble() < 0.5;

            if (success)
            {
                // reveal dealer card visually for the rest of the round (no message, continue game)
                _dealerCardRevealed = true;
                RenderHands(initialHideDealerSecond: false);
                UpdateTotalsLabel(revealDealer: true);
                // no MessageBox here — play continues
            }
            else
            {
                // failure -> dealer notices and player immediately loses
                EndRound("Dealer zauważył — przegrałeś!");
            }
        }

        private void EndRound(string message)
        {
            RenderHands(initialHideDealerSecond: false);
            UpdateTotalsLabel(revealDealer: true);
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            // save to history with score detail
            var playerVal = HandValue(_playerHand);
            var dealerVal = HandValue(_dealerHand);
            var resultText = $"{message} ({playerVal} - {dealerVal})";
            var playerName = _currentPlayer?.Name ?? "Brak";
            GameHistory.AddEntry(new HistoryEntry(playerName, "Blackjack", resultText, DateTime.Now));

            MessageBox.Show(message, "Wynik", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void newRoundButton_Click(object? sender, EventArgs e)
        {
            StartNewRound();
        }

        private class Card
        {
            public string Rank { get; }
            public string Suit { get; } // one of ♠ ♥ ♦ ♣
            public bool IsAce => Rank == "A";
            public bool IsFace => Rank == "J" || Rank == "Q" || Rank == "K";
            public bool IsRedSuit => Suit == "♥" || Suit == "♦";

            public Card(string rank, string suit)
            {
                Rank = rank;
                Suit = suit;
            }

            public string DisplayRank => Rank;

            public int ValueForTotals()
            {
                if (IsAce) return 1;
                if (IsFace) return 10;
                if (int.TryParse(Rank, out var v)) return v;
                return 0;
            }

            public override string ToString() => $"{Rank}{Suit}";
        }

        private class Deck
        {
            private readonly List<Card> _cards;
            private readonly Random _rng = new();

            private static readonly string[] Ranks = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            private static readonly string[] Suits = new[] { "♠", "♥", "♦", "♣" };

            public Deck()
            {
                _cards = new List<Card>(52);
                foreach (var s in Suits)
                {
                    foreach (var r in Ranks)
                    {
                        _cards.Add(new Card(r, s));
                    }
                }
            }

            public void Shuffle()
            {
                int n = _cards.Count;
                for (int i = n - 1; i > 0; i--)
                {
                    int j = _rng.Next(i + 1);
                    var tmp = _cards[i];
                    _cards[i] = _cards[j];
                    _cards[j] = tmp;
                }
            }

            public Card Draw()
            {
                if (_cards.Count == 0) throw new InvalidOperationException("Deck is empty");
                var c = _cards[0];
                _cards.RemoveAt(0);
                return c;
            }
        }
    }
}
