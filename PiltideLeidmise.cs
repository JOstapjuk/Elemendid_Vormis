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

        bool initialRevealPhaseStarted = false;
        System.Windows.Forms.Timer delayTimer;

        public PiltideLeidmise(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Sarnaste piltide leidmise mäng";

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 750;
            timer.Tick += Timer_Tick;

            System.Windows.Forms.Timer delayTimer = new System.Windows.Forms.Timer();
            delayTimer.Interval = 10000;
            delayTimer.Tick += DelayTimer_Tick;

            if (delayTimer != null)
            {
                delayTimer.Start();
            }

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

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is Label clickedLabel)
            {
                if (timer.Enabled == true || !initialRevealPhaseStarted)
                {
                    // During the delay phase or if we're not in the initial reveal phase
                    return;
                }

                if (clickedLabel.ForeColor == Color.Black)
                    return; // Already revealed

                if (firstClicked == null)
                {
                    // First icon clicked
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // Second icon clicked
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Start the timer to check for a match
                timer.Start();

                // Check for a match
                if (firstClicked.Text == secondClicked.Text)
                {
                    // If matched, change border color to red
                    firstClicked.BorderStyle = BorderStyle.None; // Optional: remove the border
                    firstClicked.BackColor = Color.LightGreen; // Optional: change background color
                    firstClicked.ForeColor = Color.Black; // Keep the icon visible

                    secondClicked.BorderStyle = BorderStyle.None; // Optional: remove the border
                    secondClicked.BackColor = Color.LightGreen; // Optional: change background color
                    secondClicked.ForeColor = Color.Black; // Keep the icon visible
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            // Reset the colors if not matched
            if (firstClicked != null && secondClicked != null && firstClicked.Text != secondClicked.Text)
            {
                firstClicked.ForeColor = firstClicked.BackColor; // Hide the icon
                secondClicked.ForeColor = secondClicked.BackColor; // Hide the icon
            }

            firstClicked = null;
            secondClicked = null;

            CheckForWinner(); // Check if the game is won
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return; // Not all matched

            }

            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();

            foreach (Control control in Controls)
            {
                control.Enabled = false; // Disable all controls
            }
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            if (sender != null && sender is System.Windows.Forms.Timer timer)
            {
                timer.Stop();
                initialRevealPhaseStarted = true;
                ResetIcons();
                EnableControls();
            }
        }


        private void ResetIcons()
        {
            foreach (Control control in tableLayoutPanel.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.Text = "c"; // Reset icon text
                    iconLabel.ForeColor = Color.Black; // Reset fore color
                    iconLabel.BackColor = Color.RoyalBlue; // Reset background color
                    iconLabel.BorderStyle = BorderStyle.FixedSingle; // Reset border
                }
            }
        }

        private void EnableControls()
        {
            foreach (Control control in Controls)
            {
                control.Enabled = true; // Enable all controls
            }
        }
    }
}
