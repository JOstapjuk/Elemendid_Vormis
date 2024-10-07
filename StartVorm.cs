namespace Elemendid_vormis_TARpv23
{
    public partial class StartVorm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkeruut", "radioNupp", "Tekstkast" };
        List<string> rbtn_list = new List<string> { "Üks", "Kaks", "Kolm" };
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1, chk2;
        RadioButton rbtn;
        TextBox txt;

        public StartVorm()
        {
            this.Height = 700;
            this.Width = 900;
            this.Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid:");
            foreach (var element in elemendid)
            {
                tn.Nodes.Add(new TreeNode(element));
            }

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);

            //nupp-button
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Height = 50;
            btn.Width = 100;
            btn.Location = new Point(150, 70);
            btn.Click += Btn_Click;
            //silt-label
            lbl = new Label();
            lbl.Text = "Aknade elemendid c# abil";
            lbl.Font = new Font("Arial", 24, FontStyle.Underline);
            lbl.Size = new Size(520, 50);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pbox = new PictureBox();
            pbox.Size = new Size(60, 60);
            pbox.Location = new Point(150, 130);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.Image = Image.FromFile(@"..\..\..\game.png");
            pbox.DoubleClick += Pbox_DoubleClick;

        }
        int tt = 0;
        private void Pbox_DoubleClick(object? sender, EventArgs e)
        {
            string[] pildid = { "esimene.png", "teine.png", "kolmas.png", "game.png" };
            string fail = pildid[tt];
            pbox.Image = Image.FromFile(@"..\..\..\" + fail);
            tt++;
            if (tt == 4) { tt = 0; }
        }

        private void Lbl_MouseLeave(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 24);
            lbl.ForeColor = Color.FromArgb(0, 0, 0);
        }
        private void Lbl_MouseHover(object? sender, EventArgs e)
        {
            lbl.Font = new Font("Arial", 24, FontStyle.Underline);
            lbl.ForeColor = Color.FromArgb(127, 201, 169);

        }
        int t = 0;
        private void Btn_Click(object? sender, EventArgs e)
        {
            t++;
            if (t % 2 == 0)
            {
                btn.BackColor = Color.Green;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }
        }
        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp")
            {
                Controls.Add(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pbox);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                //checkbox1
                chk1 = new CheckBox();
                chk1.Checked = false;
                chk1.Text = e.Node.Text;
                chk1.Size = new Size(150, 60);
                chk1.Location = new Point(150, 200);
                chk1.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                //checkbox2
                chk2 = new CheckBox();
                chk2.Checked = false;
                //chk2.Image = Image.FromFile(@"..\..\..\game.png");
                chk2.BackgroundImage = Image.FromFile(@"..\..\..\game.png");
                chk2.BackgroundImageLayout = ImageLayout.Zoom;
                chk2.Size = new Size(100, 100);
                chk2.Location = new Point(150, 270);
                chk2.CheckedChanged += new EventHandler(Chk_CheckedChanged);

                Controls.Add(chk1);
                Controls.Add(chk2);

            }
            else if (e.Node.Text == "radioNupp")
            {
                int startY = 250; // Starting Y position for the first radio button
                int spacing = 30; // Space between each radio button
                00
                for (int i = 0; i < rbtn_list.Count; i++)
                {
                    rbtn = new RadioButton();
                    rbtn.Checked = false;
                    rbtn.Text = rbtn_list[i];
                    rbtn.Size = new Size(100, 40);
                    rbtn.Location = new Point(350, startY + (i * spacing)); // Adjust Y position for each button
                    rbtn.CheckedChanged += new EventHandler(Btn_CheckedChanged);

                    this.Controls.Add(rbtn);
                }
            }
            else if (e.Node.Text == "Tekstkast")
            {
                txt = new TextBox();
                txt.Location = new Point(250, 70);  
                txt.Font = new Font("Arial", 24);
                txt.Width = 200;
                txt.TextChanged += Txt_TextChanged;

                this.Controls.Add(txt);
            }
        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            lbl.Text = txt.Text;
        }

        private void Btn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            lbl.Text = rb.Text;
        }

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chk1.Checked && chk2.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (chk1.Checked)
            {
                lbl.BorderStyle = BorderStyle.Fixed3D;
                pbox.BorderStyle = BorderStyle.None;
            }
            else if (chk2.Checked)
            {
                pbox.BorderStyle = BorderStyle.Fixed3D;
                lbl.BorderStyle = BorderStyle.None;
            }
            else
            {
                lbl.BorderStyle = BorderStyle.None;
                pbox.BorderStyle = BorderStyle.None;
            }

        }
    }
}
