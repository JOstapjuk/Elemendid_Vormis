using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class PildiVaatamine : Form
    {
        string[] pildid = { "esimene.png", "teine.png", "kolmas.png", "game.png" };
        PictureBox pbox;
        Button JargmineBtn;
        Button TagasiBtn;
        CheckBox chkb;
        Button show;
        Button exit;
        Button backgrn;
        Button close;
        ColorDialog colorDialog;

        public PildiVaatamine(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Pildi vaatamise programm";

            pbox = new PictureBox();
            pbox.Size = new Size(400, 400);
            pbox.Location = new Point(50, 50);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(pbox);

            JargmineBtn = new Button();
            JargmineBtn.Text = "Järgmine";
            JargmineBtn.BackColor = Color.LightBlue; 
            JargmineBtn.ForeColor = Color.Black; 
            JargmineBtn.Font = new Font("Arial", 8, FontStyle.Bold); 
            JargmineBtn.FlatStyle = FlatStyle.Flat; 
            JargmineBtn.FlatAppearance.BorderSize = 0;
            JargmineBtn.Size = new Size(65,20);
            JargmineBtn.Location = new Point(50, 550);
            JargmineBtn.Click += JrBtn_Click;
            Controls.Add(JargmineBtn);

            TagasiBtn = new Button();
            TagasiBtn.Text = "Eelmine";
            TagasiBtn.BackColor = Color.LightBlue;
            TagasiBtn.ForeColor = Color.Black;
            TagasiBtn.Font = new Font("Arial", 8, FontStyle.Bold);
            TagasiBtn.FlatStyle = FlatStyle.Flat;
            TagasiBtn.FlatAppearance.BorderSize = 0;
            TagasiBtn.Size = new Size(60, 20);
            TagasiBtn.Location = new Point(115, 550);
            TagasiBtn.Click += TgBtn_Click;
            Controls.Add(TagasiBtn);

            chkb = new CheckBox();
            chkb.Checked = false;
            chkb.Text = "Venitada";
            chkb.ForeColor = Color.Black;
            chkb.Font = new Font("Arial", 8, FontStyle.Bold);
            chkb.FlatStyle = FlatStyle.Flat;
            chkb.FlatAppearance.BorderSize = 0;
            chkb.Size = new Size(150, 60);
            chkb.Location = new Point(50, 580);
            chkb.CheckedChanged += Chk_CheckedChanged;
            Controls.Add(chkb);

            show = new Button();
            show.Text = "Näita";
            show.BackColor = Color.LightBlue;
            show.ForeColor = Color.Black;
            show.Font = new Font("Arial", 8, FontStyle.Bold);
            show.FlatStyle = FlatStyle.Flat;
            show.FlatAppearance.BorderSize = 0;
            show.Size = new Size(60, 20);
            show.Location = new Point(380, 550);
            show.Click += Show_Click;
            Controls.Add(show);

            colorDialog = new ColorDialog();
            backgrn = new Button();
            backgrn.Text = "Muuta taustavärvi";
            backgrn.BackColor = Color.LightBlue;
            backgrn.ForeColor = Color.Black;
            backgrn.Font = new Font("Arial", 8, FontStyle.Bold);
            backgrn.FlatStyle = FlatStyle.Flat;
            backgrn.FlatAppearance.BorderSize = 0;
            backgrn.Size = new Size(60, 40);
            backgrn.Location = new Point(445,550);
            backgrn.Click += backGround_Click;
            Controls.Add(backgrn);

            close = new Button();
            close.Text = "Sulge pilt";
            close.BackColor = Color.LightBlue;
            close.ForeColor = Color.Black;
            close.Font = new Font("Arial", 8, FontStyle.Bold);
            close.FlatStyle = FlatStyle.Flat;
            close.FlatAppearance.BorderSize = 0;
            close.Size = new Size(60, 40);
            close.Location = new Point(510, 550);
            close.Click += Close_Click;
            Controls.Add(close);

            exit = new Button();
            exit.Text = "Väljuda";
            exit.BackColor = Color.LightBlue;
            exit.ForeColor = Color.Black;
            exit.Font = new Font("Arial", 8, FontStyle.Bold);
            exit.FlatStyle = FlatStyle.Flat;
            exit.FlatAppearance.BorderSize = 0;
            exit.Size = new Size(60, 20);
            exit.Location = new Point(575,550);
            exit.Click += Exit_Click;
            Controls.Add(exit);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            pbox.Image = null;
        }

        private void backGround_Click(object sender, EventArgs e)
        {
            // Näita värvidialoogiboksi. Kui kasutaja klõpsab OK, siis muutke 
            // PictureBox juhtelemendi taust selle värviga, mille kasutaja valis.
            if (colorDialog.ShowDialog() == DialogResult.OK)
                pbox.BackColor = colorDialog.Color;
        }

        private void Show_Click(object? sender, EventArgs e)
        {
            pbox.Image = Image.FromFile(@"..\..\..\" + pildid[3]);           
        }

        int tt = 0;
        private void JrBtn_Click(object? sender, EventArgs e)
        {
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 4) 
            { 
                tt = 0; 
            }
        }

        private void TgBtn_Click(object? sender, EventArgs e)
        {
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt--;
            if (tt < 0) 
            { 
                tt = pildid.Length-1; 
            }
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chkb.Checked)
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pbox.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}
