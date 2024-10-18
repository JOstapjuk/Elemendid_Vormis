using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Elemendid_vormis_TARpv23
{
    public partial class PildiVaatamine : Form
    {
        string[] pildid = { "esimene.png", "teine.png", "kolmas.png", "game.png" };
        PictureBox pbox;
        PictureBox zoomPbox;
        Button JargmineBtn;
        Button TagasiBtn;
        CheckBox chkb;
        Button show;
        Button exit;
        Button backgrn;
        Button close;
        ColorDialog colorDialog;
        Image currentImage;
        MenuStrip ms;
        Button uploadBtn;
        OpenFileDialog openFileDialog;
        private Image _OriginalImage;
        private int _ZoomFactor = 1; 

        public PildiVaatamine(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Pildi vaatamise programm";

            pbox = new PictureBox();
            pbox.Size = new Size(800, 500);
            pbox.Location = new Point(30, 30);
            pbox.SizeMode = PictureBoxSizeMode.Zoom;
            pbox.MouseMove += Pbox_MouseMove;
            Controls.Add(pbox);


            zoomPbox = new PictureBox();
            zoomPbox.Size = new Size(150, 150); 
            zoomPbox.Location = new Point(850, 30);
            zoomPbox.BorderStyle = BorderStyle.FixedSingle;
            zoomPbox.BackColor = Color.White;
            Controls.Add(zoomPbox);

            JargmineBtn = new Button();
            JargmineBtn.Text = "Järgmine";
            JargmineBtn.BackColor = Color.LightBlue;
            JargmineBtn.ForeColor = Color.Black;
            JargmineBtn.Font = new Font("Arial", 8, FontStyle.Bold);
            JargmineBtn.FlatStyle = FlatStyle.Flat;
            JargmineBtn.FlatAppearance.BorderSize = 0;
            JargmineBtn.Size = new Size(65, 20);
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
            backgrn.Location = new Point(445, 550);
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
            exit.Location = new Point(575, 550);
            exit.Click += Exit_Click;
            Controls.Add(exit);

            openFileDialog = new OpenFileDialog(); 
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; 

            uploadBtn = new Button();
            uploadBtn.Text = "Lisa Pilt";
            uploadBtn.BackColor = Color.LightBlue;
            uploadBtn.ForeColor = Color.Black;
            uploadBtn.Font = new Font("Arial", 8, FontStyle.Bold);
            uploadBtn.FlatStyle = FlatStyle.Flat;
            uploadBtn.FlatAppearance.BorderSize = 0;
            uploadBtn.Size = new Size(80, 20);
            uploadBtn.Location = new Point(720, 550);
            uploadBtn.Click += UploadBtn_Click; 
            Controls.Add(uploadBtn);

            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem windowMenu = new ToolStripMenuItem("Edit");
            ToolStripMenuItem rotate = new ToolStripMenuItem("Pööra 90°", null, new EventHandler(windowTurnMenu_Click));
            ToolStripMenuItem grayscale = new ToolStripMenuItem("Grayscale", null, new EventHandler(GrayscaleMenu_Click));
            windowMenu.DropDownItems.Add(grayscale);
            windowMenu.DropDownItems.Add(rotate);
            ms.Items.Add(windowMenu);
            ms.Dock = DockStyle.Top;
            MainMenuStrip = ms;
            Controls.Add(ms);
        }

        //https://www.codeproject.com/Articles/21097/PictureBox-Zoom

        private void UpdateImage()
        {
            // If _OriginalImage is null, then return. This situation can occur

            // when a new backcolor is selected without an image loaded.
            if (_OriginalImage == null) return;


            // sourceWidth and sourceHeight store
            // the original image's width and height

            // targetWidth and targetHeight are calculated
            // to fit into the picImage picturebox.
            int sourceWidth = _OriginalImage.Width;
            int sourceHeight = _OriginalImage.Height;
            int targetWidth, targetHeight;
            double ratio;

            // Calculate targetWidth and targetHeight, so that the image will fit into

            // the picImage picturebox without changing the proportions of the image.

            if (sourceWidth > sourceHeight)
            {
                // Set the new width

                targetWidth = pbox.Width;
                // Calculate the ratio of the new width against the original width
                ratio = (double)targetWidth / sourceWidth;
                // Calculate a new height that is in proportion with the original image
                targetHeight = (int)(ratio * sourceHeight);
            }
            else if (sourceWidth < sourceHeight)
            {
                // Set the new height

                targetHeight = pbox.Height;
                // Calculate the ratio of the new height against the original height
                ratio = (double)targetHeight / sourceHeight;
                // Calculate a new width that is in proportion with the original image
                targetWidth = (int)(ratio * sourceWidth);
            }
            else
            {
                // In this case, the image is square and resizing is easy

                targetWidth = pbox.Width;
                targetHeight = pbox.Height;
            }

            // Calculate the targetTop and targetLeft values, to center the image

            // horizontally or vertically if needed

            int targetTop = (pbox.Height - targetHeight) / 2;
            int targetLeft = (pbox.Width - targetWidth) / 2;

            // Create a new temporary bitmap to resize the original image

            // The size of this bitmap is the size of the picImage picturebox
            Bitmap tempBitmap = new Bitmap(pbox.Width, pbox.Height);
            // Set the resolution of the bitmap to match the original resolution.
            Graphics bmGraphics = Graphics.FromImage(tempBitmap);
            // Create a Graphics object to further edit the temporary bitmap
            bmGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            // Draw the original image on the temporary bitmap, resizing it using

            // the calculated values of targetWidth and targetHeight.
            bmGraphics.DrawImage(_OriginalImage, new Rectangle(targetLeft, targetTop, targetWidth, targetHeight), new Rectangle(0, 0, sourceWidth, sourceHeight), GraphicsUnit.Pixel);

            // Dispose of the bmGraphics object
            bmGraphics.Dispose();
            // Set the image of the picImage picturebox to the temporary bitmap
            pbox.Image = tempBitmap;
        }
        //
        private void UpdateZoomedImage(MouseEventArgs e)
        {
            if (_OriginalImage == null) return;

            // Calculate the width and height of the portion of the image we want

            // to show in the picZoom picturebox. This value changes when the zoom

            // factor is changed.
            int zoomWidth = zoomPbox.Width;
            int zoomHeight = zoomPbox.Height;
            // Calculate the horizontal and vertical midpoints for the crosshair

            // cursor and correct centering of the new image
            int halfWidth = zoomWidth / (2 * _ZoomFactor);
            int halfHeight = zoomHeight / (2 * _ZoomFactor);

            // Ensure the mouse doesn't go out of the bounds of the image
            int zoomX = Math.Max(0, Math.Min(e.X - halfWidth, _OriginalImage.Width - zoomWidth / _ZoomFactor));
            int zoomY = Math.Max(0, Math.Min(e.Y - halfHeight, _OriginalImage.Height - zoomHeight / _ZoomFactor));

            // Create a new temporary bitmap to fit inside the picZoom picturebox
            Bitmap zoomedImage = new Bitmap(zoomWidth, zoomHeight);
            // Draw the portion of the main image onto the bitmap

            // The target rectangle is already known now.

            // Here the mouse position of the cursor on the main image is used to

            // cut out a portion of the main image.
            using (Graphics g = Graphics.FromImage(zoomedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(_OriginalImage, new Rectangle(0, 0, zoomWidth, zoomHeight),
                            new Rectangle(zoomX, zoomY, zoomWidth / _ZoomFactor, zoomHeight / _ZoomFactor), GraphicsUnit.Pixel);
            }

            // Draw the bitmap on the picZoom picturebox
            zoomPbox.Image = zoomedImage;
            // Refresh the picZoom picturebox to reflect the changes
            zoomPbox.Refresh();
        }

        private void UploadBtn_Click(object? sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                _OriginalImage = Image.FromFile(filePath);
                pbox.Image = _OriginalImage;
            }
        }

        private void Pbox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_OriginalImage != null)
            {
                UpdateZoomedImage(e);
            }
        }      

        private void GrayscaleMenu_Click(object? sender, EventArgs e)
        {
            if (pbox.Image != null)
            {
                Bitmap bmp = new Bitmap(pbox.Image);
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color originalColor = bmp.GetPixel(x, y);
                        int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                        Color grayColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        bmp.SetPixel(x, y, grayColor);
                    }
                }
                pbox.Image = bmp;
                pbox.Refresh();
            }
            else
            {
                MessageBox.Show("Pilti ei ole", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        int tt = 0;
        private void JrBtn_Click(object? sender, EventArgs e)
        {
            string fail = pildid[tt];
            _OriginalImage = Image.FromFile(@"..\..\..\" + fail); // Update _OriginalImage
            pbox.Image = _OriginalImage;
            tt++;
            if (tt == 4)
            {
                tt = 0;
            }
        }

        private void TgBtn_Click(object? sender, EventArgs e)
        {
            string fail = pildid[tt];
            _OriginalImage = Image.FromFile(@"..\..\..\" + fail); // Update _OriginalImage
            pbox.Image = _OriginalImage;
            tt--;
            if (tt < 0)
            {
                tt = pildid.Length - 1;
            }
        }


        private void windowTurnMenu_Click(object? sender, EventArgs e)
        {
            if (pbox.Image != null)
            {
                currentImage = pbox.Image;
                currentImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pbox.Image = currentImage;
                pbox.Refresh();
            }
            else
            {
                MessageBox.Show("Pilti ei ole", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object? sender, EventArgs e)
        {
            pbox.Image = null;
        }

        private void backGround_Click(object? sender, EventArgs e)
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

        private void Chk_CheckedChanged(object? sender, EventArgs e)
        {
            if (chkb.Checked)
                pbox.SizeMode = PictureBoxSizeMode.StretchImage;
            else
                pbox.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}
