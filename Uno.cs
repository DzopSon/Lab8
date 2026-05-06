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
    public partial class Uno : Form
    {
        enum UnoColor { Czerwony, Zielony, Niebieski, Zolty, Czarny }
        enum UnoValue { Zero, Jeden, Dwa, Trzy, Cztery, Piec, Szesc, Siedem, Osiem, Dziewiec, Blokada, PlusDwa, ZmianaKoloru }

        class Card
        {
            public UnoColor Color { get; set; }
            public UnoValue Value { get; set; }

            public override string ToString()
            {
                if (Value == UnoValue.ZmianaKoloru) return "ZMIANA";
                if (Value == UnoValue.Blokada) return "⊘ STOP";
                if (Value == UnoValue.PlusDwa) return "+2";

                return ((int)Value).ToString();
            }

            public System.Drawing.Color GetCardColor()
            {
                switch (Color)
                {
                    case UnoColor.Czerwony: return System.Drawing.Color.Crimson;
                    case UnoColor.Zielony: return System.Drawing.Color.ForestGreen;
                    case UnoColor.Niebieski: return System.Drawing.Color.RoyalBlue;
                    case UnoColor.Zolty: return System.Drawing.Color.Goldenrod;
                    default: return System.Drawing.Color.FromArgb(30, 30, 30);
                }
            }
        }

        List<Card> deck = new List<Card>();
        List<Card> discardPile = new List<Card>();
        List<Card> playerHand = new List<Card>();
        List<Card> botHand = new List<Card>();

        UnoColor currentColor;

        FlowLayoutPanel flpPlayer;
        FlowLayoutPanel flpBot;
        Button btnDeck;
        Button btnTopCard;
        Label lblStatus;
        Panel pnlColorPicker;
        Button btnRules;

        bool isPlayerTurn = true;
        bool waitingForColorChoice = false;
        string playerName;

        public Uno(Player? currentPlayer = null)
        {
            InitializeComponent();

            playerName = currentPlayer?.Name ?? "Brak";

            SetupUI();
            StartGame();
        }

        void SetupUI()
        {
            this.BackColor = Color.FromArgb(40, 44, 52);
            this.ClientSize = new Size(1000, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gra w UNO vs Bot";

            flpBot = new FlowLayoutPanel
            {
                Location = new Point(50, 20),
                Size = new Size(780, 150),
                BackColor = Color.FromArgb(33, 37, 43),
                AutoScroll = true,
                Padding = new Padding(10)
            };
            this.Controls.Add(flpBot);

            btnRules = new Button
            {
                Location = new Point(850, 20),
                Size = new Size(120, 45),
                Font = new Font("Arial", 11, FontStyle.Bold),
                BackColor = Color.LightSkyBlue,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Text = "Zasady",
                Cursor = Cursors.Hand
            };
            btnRules.FlatAppearance.BorderSize = 0;
            btnRules.Click += BtnRules_Click;
            this.Controls.Add(btnRules);

            flpPlayer = new FlowLayoutPanel
            {
                Location = new Point(50, 550),
                Size = new Size(900, 160),
                BackColor = Color.FromArgb(33, 37, 43),
                AutoScroll = true,
                Padding = new Padding(10)
            };
            this.Controls.Add(flpPlayer);

            btnDeck = new Button
            {
                Location = new Point(330, 280),
                Size = new Size(120, 150),
                BackColor = Color.Firebrick,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Padding = new Padding(0),
                Text = "DOBIERZ",
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDeck.FlatAppearance.BorderColor = Color.White;
            btnDeck.FlatAppearance.BorderSize = 2;
            btnDeck.Click += BtnDeck_Click;
            this.Controls.Add(btnDeck);

            btnTopCard = new Button
            {
                Location = new Point(500, 280),
                Size = new Size(120, 150),
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            btnTopCard.FlatAppearance.BorderColor = Color.White;
            btnTopCard.FlatAppearance.BorderSize = 2;
            this.Controls.Add(btnTopCard);

            lblStatus = new Label
            {
                Location = new Point(50, 480),
                Size = new Size(900, 40),
                Font = new Font("Arial", 18, FontStyle.Bold),
                ForeColor = Color.Gold,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Witaj w UNO!"
            };
            this.Controls.Add(lblStatus);

            pnlColorPicker = new Panel
            {
                Location = new Point(350, 200),
                Size = new Size(250, 60),
                Visible = false
            };

            UnoColor[] colors = { UnoColor.Czerwony, UnoColor.Zielony, UnoColor.Niebieski, UnoColor.Zolty };
            Color[] uiColors = { Color.Crimson, Color.ForestGreen, Color.RoyalBlue, Color.Goldenrod };

            for (int i = 0; i < 4; i++)
            {
                Button btnColor = new Button
                {
                    Size = new Size(50, 50),
                    Location = new Point(10 + (i * 60), 5),
                    BackColor = uiColors[i],
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = colors[i]
                };
                btnColor.FlatAppearance.BorderColor = Color.White;
                btnColor.FlatAppearance.BorderSize = 2;
                btnColor.Click += BtnColorChoice_Click;
                pnlColorPicker.Controls.Add(btnColor);
            }
            this.Controls.Add(pnlColorPicker);
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            string zasady = "ZASADY GRY W UNO:\n\n" +
                            "1. Każdy gracz otrzymuje na start 7 kart.\n" +
                            "2. W swoim ruchu musisz dopasować kartę do tej na stole:\n" +
                            "   - Tym samym KOLOREM,\n" +
                            "   - LUB tą samą CYFRĄ/SYMBOLEM.\n" +
                            "3. Jeśli nie masz pasującej karty, kliknij na 'DOBIERZ', aby pociągnąć nową kartę z talii.\n\n" +
                            "KARTY SPECJALNE:\n" +
                            "• ⊘ STOP - Przeciwnik traci swoją kolejkę.\n" +
                            "• +2 - Przeciwnik musi dobrać 2 karty i traci kolejkę.\n" +
                            "• ZMIANA (Czarna) - Możesz ją zagrać na dowolną kartę. Ty decydujesz, jaki kolor ma obowiązywać od teraz.\n\n" +
                            "Wygrywa ten gracz, który jako pierwszy pozbędzie się wszystkich swoich kart!";

            MessageBox.Show(zasady, "Instrukcja gry", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void GenerateDeck()
        {
            deck.Clear();
            UnoColor[] colors = { UnoColor.Czerwony, UnoColor.Zielony, UnoColor.Niebieski, UnoColor.Zolty };

            foreach (var color in colors)
            {
                deck.Add(new Card { Color = color, Value = UnoValue.Zero });

                for (int i = 1; i <= 9; i++)
                {
                    deck.Add(new Card { Color = color, Value = (UnoValue)i });
                    deck.Add(new Card { Color = color, Value = (UnoValue)i });
                }

                deck.Add(new Card { Color = color, Value = UnoValue.Blokada });
                deck.Add(new Card { Color = color, Value = UnoValue.Blokada });
                deck.Add(new Card { Color = color, Value = UnoValue.PlusDwa });
                deck.Add(new Card { Color = color, Value = UnoValue.PlusDwa });
            }

            for (int i = 0; i < 4; i++)
            {
                deck.Add(new Card { Color = UnoColor.Czarny, Value = UnoValue.ZmianaKoloru });
            }

            Random r = new Random();
            deck = deck.OrderBy(x => r.Next()).ToList();
        }

        Card DrawOneCard()
        {
            if (deck.Count == 0)
            {
                Card top = discardPile.Last();
                discardPile.Remove(top);
                deck = discardPile.ToList();
                discardPile.Clear();
                discardPile.Add(top);

                Random r = new Random();
                deck = deck.OrderBy(x => r.Next()).ToList();
            }

            Card drawn = deck[0];
            deck.RemoveAt(0);
            return drawn;
        }

        void StartGame()
        {
            GenerateDeck();
            playerHand.Clear();
            botHand.Clear();
            discardPile.Clear();

            for (int i = 0; i < 7; i++)
            {
                playerHand.Add(DrawOneCard());
                botHand.Add(DrawOneCard());
            }

            Card firstCard = DrawOneCard();
            while (firstCard.Color == UnoColor.Czarny)
            {
                deck.Add(firstCard);
                firstCard = DrawOneCard();
            }
            discardPile.Add(firstCard);
            currentColor = firstCard.Color;

            isPlayerTurn = true;
            UpdateUI();
        }

        void UpdateUI()
        {
            Card topCard = discardPile.Last();
            btnTopCard.Text = topCard.ToString();
            btnTopCard.BackColor = topCard.Color == UnoColor.Czarny ? GetColorFromEnum(currentColor) : topCard.GetCardColor();

            flpBot.Controls.Clear();
            foreach (var card in botHand)
            {
                Button btnBotCard = new Button
                {
                    Size = new Size(70, 100),
                    BackColor = Color.FromArgb(50, 0, 0),
                    ForeColor = Color.LightGray,
                    Font = new Font("Arial", 9, FontStyle.Bold),
                    Padding = new Padding(0),
                    Text = "UNO",
                    FlatStyle = FlatStyle.Flat
                };
                btnBotCard.FlatAppearance.BorderColor = Color.White;
                btnBotCard.FlatAppearance.BorderSize = 1;
                flpBot.Controls.Add(btnBotCard);
            }

            flpPlayer.Controls.Clear();
            foreach (var card in playerHand)
            {
                Button btnMyCard = new Button
                {
                    Size = new Size(95, 130),
                    BackColor = card.GetCardColor(),
                    ForeColor = Color.White,
                    Font = new Font("Arial", 8.5f, FontStyle.Bold), 
                    Padding = new Padding(0),
                    Text = card.ToString(),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    Tag = card
                };

                btnMyCard.FlatAppearance.BorderColor = Color.White;
                btnMyCard.FlatAppearance.BorderSize = 2;

                btnMyCard.Click += PlayerCard_Click;
                flpPlayer.Controls.Add(btnMyCard);
            }

            CheckWinCondition();
        }

        Color GetColorFromEnum(UnoColor c)
        {
            if (c == UnoColor.Czerwony) return Color.Crimson;
            if (c == UnoColor.Zielony) return Color.ForestGreen;
            if (c == UnoColor.Niebieski) return Color.RoyalBlue;
            if (c == UnoColor.Zolty) return Color.Goldenrod;
            return Color.FromArgb(30, 30, 30);
        }

        bool IsValidMove(Card cardToPlay)
        {
            Card topCard = discardPile.Last();

            if (cardToPlay.Color == UnoColor.Czarny) return true;
            if (cardToPlay.Color == currentColor) return true;
            if (cardToPlay.Value == topCard.Value) return true;

            return false;
        }

        private void PlayerCard_Click(object sender, EventArgs e)
        {
            if (!isPlayerTurn || waitingForColorChoice) return;

            Button clickedBtn = sender as Button;
            Card playedCard = clickedBtn.Tag as Card;

            if (IsValidMove(playedCard))
            {
                playerHand.Remove(playedCard);
                discardPile.Add(playedCard);
                currentColor = playedCard.Color;

                if (playedCard.Color == UnoColor.Czarny)
                {
                    lblStatus.Text = "Wybierz nowy kolor (przyciski powyżej)!";
                    waitingForColorChoice = true;
                    pnlColorPicker.Visible = true;
                    UpdateUI();
                    return;
                }

                UpdateUI();
                ProcessCardEffectAndNextTurn(playedCard);
            }
            else
            {
                MessageBox.Show("Ta karta nie pasuje do koloru ani cyfry na stole!", "Zły ruch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeck_Click(object sender, EventArgs e)
        {
            if (!isPlayerTurn || waitingForColorChoice) return;

            Card drawn = DrawOneCard();
            playerHand.Add(drawn);
            lblStatus.Text = $"Dobierasz kartę. Ruch Bota...";
            isPlayerTurn = false;
            UpdateUI();

            ScheduleBotTurn(1500);
        }

        private void BtnColorChoice_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            currentColor = (UnoColor)btn.Tag;

            pnlColorPicker.Visible = false;
            waitingForColorChoice = false;
            lblStatus.Text = $"Zmieniłeś kolor na {currentColor}. Ruch Bota...";
            UpdateUI();

            ProcessCardEffectAndNextTurn(discardPile.Last());
        }

        void ProcessCardEffectAndNextTurn(Card playedCard)
        {
            if (isPlayerTurn)
            {
                if (playedCard.Value == UnoValue.Blokada)
                {
                    lblStatus.Text = "Zablokowałeś Bota! Znowu Twój ruch.";
                    UpdateUI();
                    return;
                }
                else if (playedCard.Value == UnoValue.PlusDwa)
                {
                    botHand.Add(DrawOneCard());
                    botHand.Add(DrawOneCard());
                    lblStatus.Text = "Bot dostaje +2 karty i traci kolejkę! Twój ruch.";
                    UpdateUI();
                    return;
                }

                isPlayerTurn = false;
                lblStatus.Text = "Ruch Bota...";
                ScheduleBotTurn(1500);
            }
            else
            {
                if (playedCard.Value == UnoValue.Blokada)
                {
                    lblStatus.Text = "Zostałeś zablokowany! Bot gra znowu.";
                    UpdateUI();
                    ScheduleBotTurn(1500);
                    return;
                }
                else if (playedCard.Value == UnoValue.PlusDwa)
                {
                    playerHand.Add(DrawOneCard());
                    playerHand.Add(DrawOneCard());
                    lblStatus.Text = "Dostajesz +2 karty i tracisz kolejkę! Bot gra znowu.";
                    UpdateUI();
                    ScheduleBotTurn(2000);
                    return;
                }

                isPlayerTurn = true;
                lblStatus.Text = "Twój ruch!";
                UpdateUI();
            }
        }

        void BotTurn()
        {
            if (CheckWinCondition()) return;

            Card cardToPlay = botHand.FirstOrDefault(c => IsValidMove(c));

            if (cardToPlay != null)
            {
                botHand.Remove(cardToPlay);
                discardPile.Add(cardToPlay);
                currentColor = cardToPlay.Color;

                string actionText = $"Bot gra: {cardToPlay.ToString().Replace("\n", " ")}.";

                if (cardToPlay.Color == UnoColor.Czarny)
                {
                    var bestColor = botHand.Where(c => c.Color != UnoColor.Czarny)
                                           .GroupBy(c => c.Color)
                                           .OrderByDescending(g => g.Count())
                                           .Select(g => g.Key)
                                           .FirstOrDefault();

                    if (bestColor == UnoColor.Czarny || botHand.Count == 0) bestColor = UnoColor.Czerwony;

                    currentColor = bestColor;
                    actionText += $" Zmienia kolor na: {currentColor}!";
                }

                lblStatus.Text = actionText;
                UpdateUI();
                ProcessCardEffectAndNextTurn(cardToPlay);
            }
            else
            {
                Card drawn = DrawOneCard();
                botHand.Add(drawn);
                lblStatus.Text = "Bot dobiera kartę z talii. Twój ruch!";
                isPlayerTurn = true;
                UpdateUI();
            }
        }

        bool CheckWinCondition()
        {
            if (playerHand.Count == 0)
            {
                lblStatus.Text = "🎉 WYGRAŁEŚ! 🎉";
                GameHistory.AddEntry(new HistoryEntry(playerName, "UNO", "Wygrana", DateTime.Now));

                MessageBox.Show("Gratulacje! Pozbyłeś się wszystkich kart!", "Koniec Gry");
                StartGame();
                return true;
            }
            else if (botHand.Count == 0)
            {
                lblStatus.Text = "💀 PRZEGRAŁEŚ! Bot wygrał. 💀";
                GameHistory.AddEntry(new HistoryEntry(playerName, "UNO", "Przegrana", DateTime.Now));

                MessageBox.Show("Niestety, Bot wygrał tę partię.", "Koniec Gry");
                StartGame();
                return true;
            }
            return false;
        }
        void ScheduleBotTurn(int delayMs)
        {
            Task.Delay(delayMs).ContinueWith(_ =>
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!this.IsDisposed) BotTurn();
                        }));
                    }
                    catch
                    {
   
                    }
                }
            });
        }
    }
}