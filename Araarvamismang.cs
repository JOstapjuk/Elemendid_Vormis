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
    public partial class Araarvamismang : Form
    {
        Label TimeText, TimeNum;
        Label plusNum1, plusNum2;
        Label minusNum1, minusNum2;
        Label multiplyNum1, multiplyNum2;
        Label divideNum1, divideNum2;
        Label sum;

        public Araarvamismang(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Matemaatiline äraarvamismäng";

            TimeText = new Label();
            TimeText.Text = "Aega jäänud";
            TimeText.Size = new Size(75,20);
            TimeText.Location = new Point(95,30);
            Controls.Add(TimeText);

            TimeNum = new Label();
            TimeNum.Text = "";
            TimeNum.BorderStyle = BorderStyle.FixedSingle;
            TimeNum.Size = new Size(100,20);
            TimeNum.Location = new Point(180,30);
            Controls.Add(TimeNum);


        }
    }
}
