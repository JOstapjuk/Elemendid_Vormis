using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Elemendid_vormis_TARpv23
{
    public partial class PiltideLeidmise : Form
    {
        TableLayoutPanel tableLayoutPanel;
        Label firstClicked = null;
        Label secondClicked = null;
        System.Windows.Forms.Timer timer;
        private Color selectedIconColor = Color.Black;
        Random random = new Random();
        List<string> iconsW = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        int tries;

        public PiltideLeidmise(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Sarnaste piltide leidmise mäng";

            MessageBox.Show("Tere tulemast \n\n" +
                            "Instruktsioonid: \n" +
                            "1. Klõpsake kahel ruudul, et paljastada peidetud ikoonid\n" +
                            "2. Proovi sobitada kahte identset ikooni\n" +
                            "3. Kui need ei klapi, siis on nad pärast lühikest viivitust jälle peidus.\n" +
                            "4. Mäng lõpeb, kui kõik ikoonid on edukalt sobitatud\n\n" +
                            "Edu mängule!", "Mängujuhised");

            ChooseIconSet();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80 
            };

            Button closeButton = new Button
            {
                Text = "Sulge mäng",
                Dock = DockStyle.Right,
                Height = 50,
                Width = 200
            };
            closeButton.Click += (sender, e) => this.Close();

            Button restartButton = new Button
            {
                Text = "Taaskäivitada mäng",
                Dock = DockStyle.Left,
                Height = 50,
                Width = 200
            };
            restartButton.Click += (sender, e) => RestartGame();

            Label counterLabel = new Label
            {
                Text = "Proovid: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 14)
            };

            tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 4,
                BackColor = Color.CornflowerBlue
            };


            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            }

            for (int i = 0; i < 16; i++)
            {
                Label label = new Label
                {
                    Text = "c",
                    Font = new Font("Webdings", 48, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    BackColor = Color.RoyalBlue,
                    AutoSize = false,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    BorderStyle = BorderStyle.FixedSingle
                };
                label.Click += Label_Click;
                tableLayoutPanel.Controls.Add(label);
            }

            AssignIconsToSquares();

            buttonPanel.Controls.Add(counterLabel);
            buttonPanel.Controls.Add(closeButton);
            buttonPanel.Controls.Add(restartButton);

            Controls.Add(tableLayoutPanel);
            Controls.Add(buttonPanel);

            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem windowMenu = new ToolStripMenuItem("Värvid");

            ToolStripMenuItem backgroundColorOption = new ToolStripMenuItem("Taustavärvi muutmine", null, new EventHandler(ChangeBackgroundColor_Click));
            ToolStripMenuItem iconColorOption = new ToolStripMenuItem("Akna värvi muutmine", null, new EventHandler(ChangeIconColor_Click));
            ToolStripMenuItem iconSelectedColorOption = new ToolStripMenuItem("Vali ikooni värv", null, new EventHandler(ChangeSelectedColor_Click));

            windowMenu.DropDownItems.Add(iconSelectedColorOption);
            windowMenu.DropDownItems.Add(backgroundColorOption);
            windowMenu.DropDownItems.Add(iconColorOption);

            ms.Items.Add(windowMenu);
            ms.Dock = DockStyle.Top;
            MainMenuStrip = ms;
            Controls.Add(ms);
        }

        private void ChangeSelectedColor_Click(object? sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedIconColor = colorDialog.Color;
            }
        }

        private void ChangeBackgroundColor_Click(object? sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                tableLayoutPanel.BackColor = colorDialog.Color;
            }
        }

        private void ChangeIconColor_Click(object? sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        iconLabel.BackColor = colorDialog.Color;

                        iconLabel.ForeColor = colorDialog.Color;
                    }
                }
            }
        }

        private void UpdateCounterLabel()
        {
            
            Label counterLabel = (Label)((Panel)Controls[1]).Controls[0];
            counterLabel.Text = $"Proovid: {tries}";
        }

        private void RestartGame()
        {
            firstClicked = null;
            secondClicked = null;
            iconsW.Clear();
            ChooseIconSet();
            AssignIconsToSquares();
            tries = 0;
            UpdateCounterLabel();
        }

        private void ChooseIconSet()
        {
            Form selectionForm = new Form
            {
                Text = "Ikoonide valik",
                Width = 300,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false
            };

            RadioButton rbSet1 = new RadioButton { Text = "Webdings 1", Location = new Point(20, 20), Checked = true };
            RadioButton rbSet2 = new RadioButton { Text = "Webdings 2", Location = new Point(20, 50) };
            RadioButton rbSet3 = new RadioButton { Text = "Webdings 3", Location = new Point(20, 80) };

            Button confirmButton = new Button { Text = "OK", Location = new Point(100, 120), DialogResult = DialogResult.OK };
            confirmButton.Click += (sender, e) => selectionForm.Close();

            selectionForm.Controls.Add(rbSet1);
            selectionForm.Controls.Add(rbSet2);
            selectionForm.Controls.Add(rbSet3);
            selectionForm.Controls.Add(confirmButton);

            if (selectionForm.ShowDialog() == DialogResult.OK)
            {
                if (rbSet1.Checked)
                {
                    iconsW = new List<string>
                    {
                        "!", "!", "N", "N", ",", ",", "k", "k",
                        "b", "b", "v", "v", "w", "w", "z", "z"
                    };
                }
                else if (rbSet2.Checked)
                {
                    iconsW = new List<string>
                    {
                        "a", "a", "e", "e", "c", "c", "d", "d",
                        "f", "f", "o", "o", "j", "j", "i", "i"
                    };
                }
                else if (rbSet3.Checked)
                {
                    iconsW = new List<string>
                    {
                        "]", "]", ".", ".", "_", "_", "@", "@",
                        "$", "$", "%", "%", "*", "*", "+", "+"
                    };
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Label_Click(object? sender, EventArgs e)
        {
            if (timer.Enabled) return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == selectedIconColor)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = selectedIconColor;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = selectedIconColor;

                tries++;
                UpdateCounterLabel();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    if (CheckIfGameIsOver())
                    {
                        MessageBox.Show($"Sa oled kõik ikoonid kokku sobitanud! Proovide arv: {tries}", "Mäng Läbi");
                        this.Close();
                    }

                    return;
                }

                timer.Start();
            }
        }



        private void Timer_Tick(object? sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void AssignIconsToSquares()
        {

            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    int randomNumber = random.Next(iconsW.Count);
                    iconLabel.Text = iconsW[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    iconsW.RemoveAt(randomNumber);
                }
            }
        }

        private bool CheckIfGameIsOver()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor != Color.Black)
                {
                    return false; 
                }
            }
            return true; 
        }
    }
}
