using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class PiltideLeidmise : Form
    {
        TableLayoutPanel tableLayoutPanel;
        Label firstClicked = null;
        Label secondClicked = null;
        System.Windows.Forms.Timer timer;
        Random random = new Random();
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

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

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

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

            Controls.Add(tableLayoutPanel);
        }
        

        private void Label_Click(object sender, EventArgs e)
        {
            if (timer.Enabled) return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    if (CheckIfGameIsOver())
                    {
                        MessageBox.Show("Sa oled kõik ikoonid kokku sobitanud!", "Mäng Läbi");
                        this.Close();
                    }

                    return;
                }

                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
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
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
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
