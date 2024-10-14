using Microsoft.VisualBasic;
using System.Data;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Elemendid_vormis_TARpv23
{
    public partial class StartVorm : Form
    {
        List<string> elemendid = new List<string> { "Nupp", "Silt", "Pilt", "Märkeruut", "radioNupp", "Tekstkast", "Loetelu", "Table", "Dialoogaknad","Mängid" };
        List<string> rbtn_list = new List<string> { "Üks", "Kaks", "Kolm" };
        List<string> rbtn_list2 = new List<string> { "Pildi vaatamise programm", "Matemaatiline äraarvamismäng", "Sarnaste piltide leidmise mäng" };
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pbox;
        CheckBox chk1, chk2;
        RadioButton rbtn;
        RadioButton rbtn2;
        TextBox txt;
        ListBox lb;
        DataSet ds;
        DataGridView dg;


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
            lbl.Font = new Font("Arial", 24);
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
                int startY = 250;
                int spacing = 30;

                for (int i = 0; i < rbtn_list.Count; i++)
                {
                    rbtn = new RadioButton();
                    rbtn.Checked = false;
                    rbtn.Text = rbtn_list[i];
                    rbtn.Size = new Size(80, 40);
                    rbtn.Location = new Point(350, startY + (i * spacing));
                    rbtn.CheckedChanged += new EventHandler(Btn_CheckedChanged);

                    this.Controls.Add(rbtn);
                }
            }
            else if (e.Node.Text == "Tekstkast")
            {
                txt = new TextBox();
                txt.Location = new Point(250, 70);
                txt.Font = new Font("Arial", 10);
                txt.Width = 200;
                txt.TextChanged += Txt_TextChanged;

                this.Controls.Add(txt);
            }
            else if (e.Node.Text == "Loetelu")
            {
                lb = new ListBox();
                foreach (string element in rbtn_list)
                {
                    lb.Items.Add(element);
                }
                lb.Location = new Point(460, 70);
                lb.Font = new Font("Arial", 10);
                lb.Width = 200;
                lb.SelectedIndexChanged += Lb_SelectedIndexChanged;

                this.Controls.Add(lb);
            }
            else if (e.Node.Text == "Table")
            {
                ds = new DataSet("XML fail");
                ds.ReadXml(@"..\..\..\plant.xml");
                dg = new DataGridView();
                dg.Location = new Point(150, 400);
                dg.DataSource = ds;
                dg.DataMember = "plant";

                dg.SelectionChanged += Dg_SelectionChanged;

                this.Controls.Add(dg);
            }
            else if (e.Node.Text == "Dialoogaknad")
            {
                var vastus = MessageBox.Show("Sisestame andmeid", "Kas sa tahad andmeti lisada?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (vastus == DialogResult.Yes)
                {

                    string common = Interaction.InputBox("Sisesta tavaline nimi", "Andmete sisestamine");
                    string botanical = Interaction.InputBox("Sisesta botaaniline nimi", "Andmete sisestamine");
                    string zone = Interaction.InputBox("Sisesta tsoon", "Andmete sisestamine");
                    string light = Interaction.InputBox("Sisesta valgus", "Andmete sisestamine");
                    string price = Interaction.InputBox("Sisesta hind", "Andmete sisestamine");
                    string availability = Interaction.InputBox("Sisesta saadavus", "Andmete sisestamine");


                    AddPlantToXml(common, botanical, zone, light, price, availability);
                    UpdateXML();

                    MessageBox.Show("Uus andmed on lisatud!");
                }
            }
            else if (e.Node.Text == "Mängid")
            {
                int startY = 250;
                int vahekaugus = 40;

                for (int i = 0; i < rbtn_list2.Count; i++)
                {
                    rbtn2 = new RadioButton();
                    rbtn2.Checked = false;
                    rbtn2.Text = rbtn_list2[i];
                    rbtn2.Size = new Size(250, 60);
                    rbtn2.Location = new Point(460, startY + (i * vahekaugus));
                    rbtn2.CheckedChanged += new EventHandler(Btn_CheckedMang);

                    this.Controls.Add(rbtn2);
                }
            }
        }

        private void Btn_CheckedMang(object? sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                string selected = rb.Text;

                switch (selected)
                {
                    case "Pildi vaatamise programm":
                        PildiVaatamine pildiVaatamine = new PildiVaatamine(900, 700);
                        pildiVaatamine.Show();
                        break;
                    case "Matemaatiline äraarvamismäng":
                        Araarvamismang araarvamismang = new Araarvamismang(350, 400);
                        araarvamismang.Show();
                        break;
                    case "Sarnaste piltide leidmise mäng":
                        PiltideLeidmise piltideLeidmise = new PiltideLeidmise(550, 550);
                        piltideLeidmise.Show();
                        break;
                    default:
                        Console.WriteLine("Error");
                        break;
                }
            }
        }


        private void AddPlantToXml(string common, string botanical, string zone, string light, string price, string availability)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(@"..\..\..\plant.xml");

            DataTable dt = ds.Tables["plant"];
            DataRow newRow = dt.NewRow();
            newRow["COMMON"] = common;
            newRow["BOTANICAL"] = botanical;
            newRow["ZONE"] = zone;
            newRow["LIGHT"] = light;
            newRow["PRICE"] = price;
            newRow["AVAILABILITY"] = availability;

            dt.Rows.Add(newRow);

            ds.WriteXml(@"..\..\..\plant.xml");
        }

        private void UpdateXML()
        {

            DataSet ds = new DataSet();
            ds.ReadXml(@"..\..\..\plant.xml");

            DataTable dt = ds.Tables["plant"];


            dg.DataSource = dt;
        }

        private void Dg_SelectionChanged(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count > 0)
            {
                DataGridViewSelectedRowCollection selectedRows = dg.SelectedRows;

                foreach (DataGridViewRow row in selectedRows)
                {
                    string cell1Value = row.Cells[0].Value.ToString();
                    string cell2Value = row.Cells[1].Value.ToString();

                    txt.Text = ($"{cell1Value} / {cell2Value}");
                }
            }
        }

        private void Lb_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (lb.SelectedIndex)
            {
                case 0:
                    txt.BackColor = Color.DimGray;
                    tree.BackColor = Color.DimGray;
                    break;
                case 1:
                    txt.BackColor = Color.Bisque;
                    tree.BackColor = Color.Bisque;
                    break;
                case 2:
                    txt.BackColor = Color.Cyan;
                    tree.BackColor = Color.Cyan;
                    break;
            }

        }

        private void Txt_TextChanged(object? sender, EventArgs e)
        {
            lbl.Text = txt.Text;
        }

        private void Btn_CheckedChanged(object? sender, EventArgs e)
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
