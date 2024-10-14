using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elemendid_vormis_TARpv23
{
    public partial class Araarvamismang : Form
    {
        Label timeLabel;
        Label plusLeftLabel;
        Label plusRightLabel;
        NumericUpDown sum;
        Label minusLeftLabel;
        Label minusRightLabel;
        NumericUpDown sumMinus;
        Label timesLeftLabel;
        Label timesRightLabel;
        NumericUpDown sumMultiply;
        Label dividedLeftLabel;
        Label dividedRightLabel;
        NumericUpDown sumDivide;
        Button startButton;
        System.Windows.Forms.Timer timer;
        int timeLeft;
        Random rand = new Random();
        int addend1, addend2;
        int minuend, subtrahend;
        int multiplicand, multiplier;
        int dividend, divisor;
        int score;


        bool additionCorrect = false;
        bool subtractionCorrect = false;
        bool multiplicationCorrect = false;
        bool divisionCorrect = false;

        public Araarvamismang(int w, int h)
        {
            this.Width = w;
            this.Height = h;
            this.Text = "Matemaatiline äraarvamismäng";

            timeLabel = new Label();
            timeLabel.Text = "Time Left: 30 seconds";
            timeLabel.Location = new Point(95, 30);
            timeLabel.Width = 200;
            Controls.Add(timeLabel);


            plusLeftLabel = new Label();
            plusLeftLabel.Text = "0";
            plusLeftLabel.Location = new Point(50, 100);
            Controls.Add(plusLeftLabel);

            plusRightLabel = new Label();
            plusRightLabel.Text = "+ 0 =";
            plusRightLabel.Location = new Point(150, 100);
            Controls.Add(plusRightLabel);

            sum = new NumericUpDown();
            sum.Location = new Point(250, 100);
            sum.Width = 50;
            sum.Value = 0; 
            sum.Enter += answer_Enter;
            Controls.Add(sum);


            minusLeftLabel = new Label();
            minusLeftLabel.Text = "0";
            minusLeftLabel.Location = new Point(50, 150);
            Controls.Add(minusLeftLabel);

            minusRightLabel = new Label();
            minusRightLabel.Text = "- 0 =";
            minusRightLabel.Location = new Point(150, 150);
            Controls.Add(minusRightLabel);

            sumMinus = new NumericUpDown();
            sumMinus.Location = new Point(250, 150);
            sumMinus.Width = 50;
            sumMinus.Value = 0; 
            sumMinus.Enter += answer_Enter;
            Controls.Add(sumMinus);

            timesLeftLabel = new Label();
            timesLeftLabel.Text = "0";
            timesLeftLabel.Location = new Point(50, 200);
            Controls.Add(timesLeftLabel);

            timesRightLabel = new Label();
            timesRightLabel.Text = "× 0 =";
            timesRightLabel.Location = new Point(150, 200);
            Controls.Add(timesRightLabel);

            sumMultiply = new NumericUpDown();
            sumMultiply.Location = new Point(250, 200);
            sumMultiply.Width = 50;
            sumMultiply.Value = 0; 
            sumMultiply.Enter += answer_Enter;
            Controls.Add(sumMultiply);


            dividedLeftLabel = new Label();
            dividedLeftLabel.Text = "0";
            dividedLeftLabel.Location = new Point(50, 250);
            Controls.Add(dividedLeftLabel);

            dividedRightLabel = new Label();
            dividedRightLabel.Text = "÷ 0 =";
            dividedRightLabel.Location = new Point(150, 250);
            Controls.Add(dividedRightLabel);

            sumDivide = new NumericUpDown();
            sumDivide.Location = new Point(250, 250);
            sumDivide.Width = 50;
            sumDivide.Value = 0; 
            sumDivide.Enter += answer_Enter;
            Controls.Add(sumDivide);


            startButton = new Button();
            startButton.Text = "Start the quiz";
            startButton.Location = new Point(100, 300);
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);


            timer = new System.Windows.Forms.Timer(); 
            timer.Interval = 1000; 
            timer.Tick += QuizTimer_Tick;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartQuiz();
            startButton.Enabled = false;
        }

        private void StartQuiz()
        {
            score = 0;
            timeLeft = 30;
            timeLabel.Text = "Time Left: 30 seconds";
            GenerNumbers();
            timer.Start();
        }

        private void GenerNumbers()
        {

            additionCorrect = subtractionCorrect = multiplicationCorrect = divisionCorrect = false;

            addend1 = rand.Next(51);
            addend2 = rand.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = "+ " + addend2.ToString() + " =";
            sum.Value = 0; 

            minuend = rand.Next(1, 101);
            subtrahend = rand.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = "- " + subtrahend.ToString() + " =";
            sumMinus.Value = 0; 

            multiplicand = rand.Next(2, 11);
            multiplier = rand.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = "× " + multiplier.ToString() + " =";
            sumMultiply.Value = 0; 


            do
            {
                divisor = rand.Next(2, 11); 
            }
            while (divisor == 0);

            int tempQuotient = rand.Next(2, 11);
            dividend = divisor * tempQuotient; 
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = "÷ " + divisor.ToString() + " =";
            sumDivide.Value = 0; 
        }

        private void QuizTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = $"Aeg on jäänud: {timeLeft} sekundid";
                CheckAnswer();
            }
            else
            {
                timer.Stop();
                timeLabel.Text = "Aeg on läbi!";
                MessageBox.Show($"Aeg on läbi! Sinu tulemus on {score}.");
                startButton.Enabled = true;
            }
        }

        private void CheckAnswer()
        {

            if (addend1 + addend2 == sum.Value)
            {
                if (!additionCorrect)
                {
                    sum.BackColor = Color.LightGreen;
                    score++; 
                    additionCorrect = true; 
                }
            }
            else
            {
                sum.BackColor = Color.LightCoral;
                additionCorrect = false; 
            }

            if (minuend - subtrahend == sumMinus.Value)
            {
                if (!subtractionCorrect)
                {
                    sumMinus.BackColor = Color.LightGreen;
                    score++;
                    subtractionCorrect = true;
                }
            }
            else
            {
                sumMinus.BackColor = Color.LightCoral;
                subtractionCorrect = false;
            }

            if (multiplicand * multiplier == sumMultiply.Value)
            {
                if (!multiplicationCorrect)
                {
                    sumMultiply.BackColor = Color.LightGreen;
                    score++;
                    multiplicationCorrect = true;
                }
            }
            else
            {
                sumMultiply.BackColor = Color.LightCoral;
                multiplicationCorrect = false;
            }

            if (dividend / divisor == sumDivide.Value)
            {
                if (!divisionCorrect)
                {
                    sumDivide.BackColor = Color.LightGreen;
                    score++;
                    divisionCorrect = true;
                }
            }
            else
            {
                sumDivide.BackColor = Color.LightCoral;
                divisionCorrect = false;
            }


            if (additionCorrect && subtractionCorrect && multiplicationCorrect && divisionCorrect)
            {
                GenerNumbers(); 
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            if (answerBox != null)
            {
                int LenghtAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, LenghtAnswer);
            }
        }
    }
}
