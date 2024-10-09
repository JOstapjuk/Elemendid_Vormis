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
        PictureBox pbox;
        Button JargmineBtn;
        Button TagasiBtn;
        CheckBox chkb;
        Button show;
        Button exit;
        Button backgrn;
        Button close;

        public PildiVaatamine(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Pildi vaatamise programm";

            pbox = new PictureBox();
            pbox.Size = new Size(800, 600);
            pbox.Location = new Point(50, 50);
            Controls.Add(pbox);

            JargmineBtn = new Button();
            JargmineBtn.Text = "Järgmine";
            JargmineBtn.Size = new Size(60,20);
            JargmineBtn.Location = new Point(50, 550);
            JargmineBtn.Click += JrBtn_Click;
            Controls.Add(JargmineBtn);

            TagasiBtn = new Button();
            TagasiBtn.Text = "Eelmine";
            TagasiBtn.Size = new Size(60, 20);
            TagasiBtn.Location = new Point(110, 550);
            TagasiBtn.Click += TgBtn_Click;
            Controls.Add(TagasiBtn);

            chkb = new CheckBox();
            chkb.Checked = false;
            chkb.Text = "Venitada";
            chkb.Size = new Size(150, 60);
            chkb.Location = new Point(50, 580);
            chkb.CheckedChanged += Chk_CheckedChanged;
            Controls.Add(chkb);

            show = new Button();
            show.Text = "Näita";
            show.Size = new Size(60, 20);
            show.Location = new Point(150, 550);
            show.Click += Show_Click;
            Controls.Add(show);
        }

        int t = 0;
        private void Show_Click(object? sender, EventArgs e)
        {
            t++;
            if (t == 1)
            {
                pbox.Image = Image.FromFile(@"..\..\..\game.png");
            }
        }

        int tt = 0;
        private void JrBtn_Click(object? sender, EventArgs e)
        {
            string[] pildid = { "esimene.png", "teine.png", "kolmas.png", "game.png" };
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
            string[] pildid = { "esimene.png", "teine.png", "kolmas.png", "game.png" };
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
