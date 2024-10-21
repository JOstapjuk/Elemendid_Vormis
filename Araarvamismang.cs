using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

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
        Button restartButton; 
        Button closeButton;
        System.Windows.Forms.Timer timer;
        Label highScoreLabel;
        Button hintButton;
        int hintPenalty = 1;
        int timeLeft;
        Random rand = new Random();
        int addend1, addend2;
        int minuend, subtrahend;
        int multiplicand, multiplier;
        int dividend, divisor;
        int score;
        int highScore = 0;

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
            timeLabel.Text = "Aeg on jäänud: 30 sekundit";
            timeLabel.Location = new Point(95, 30);
            timeLabel.Width = 200;
            Controls.Add(timeLabel);

            hintButton = new Button();
            hintButton.Text = "Vihje";
            hintButton.Location = new Point(280, 300); 
            hintButton.Click += HintButton_Click;
            Controls.Add(hintButton);

            highScoreLabel = new Label();
            highScoreLabel.Text = "Kõrgeim skoor: 0";
            highScoreLabel.Location = new Point(150, 60);
            highScoreLabel.Width = 150;
            Controls.Add(highScoreLabel);


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
            sum.Text = "";
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
            sumMinus.Text = "";
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
            sumMultiply.Text = "";
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
            sumDivide.Text = "";
            sumDivide.Enter += answer_Enter;
            Controls.Add(sumDivide);


            startButton = new Button();
            startButton.Text = "Alustage";
            startButton.Location = new Point(10, 300);
            startButton.Click += StartButton_Click;
            Controls.Add(startButton);

            restartButton = new Button();
            restartButton.Text = "Taaskäivita";
            restartButton.Location = new Point(100, 300);
            restartButton.Click += RestartButton_Click;
            Controls.Add(restartButton);

            closeButton = new Button();
            closeButton.Text = "Sulge";
            closeButton.Location = new Point(190, 300);
            closeButton.Click += (sender, e) => this.Close();
            Controls.Add(closeButton);

            timer = new System.Windows.Forms.Timer(); 
            timer.Interval = 1000; 
            timer.Tick += QuizTimer_Tick;
            LoadHighScore();
        }

        private void HintButton_Click(object sender, EventArgs e)
        {
            if (score > 0)
            {
                score -= hintPenalty; 
                ShowHint(); 
            }
            else
            {
                MessageBox.Show("Sa ei saa kasutada vihjet, kuna skoor on juba 0!", "Vihje");
            }
        }

        private void ShowHint()
        {
            if (!additionCorrect)
            {
                sum.Value = addend1 + addend2;
            }
            else if (!subtractionCorrect)
            {
                sumMinus.Value = minuend - subtrahend;
            }
            else if (!multiplicationCorrect)
            {
                sumMultiply.Value = multiplicand * multiplier;
            }
            else if (!divisionCorrect)
            {
                sumDivide.Value = dividend / divisor;
            }
        }

        //https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
        //https://learn.microsoft.com/en-us/answers/questions/670543/how-can-i-save-data-information-to-text-file-and-r

        private void LoadHighScore()
        {
            if (File.Exists("Score.txt"))
            {
                string highScoreText = File.ReadAllText("Score.txt");
                if (int.TryParse(highScoreText, out int savedHighScore))
                {
                    highScore = savedHighScore;
                }
            }
            highScoreLabel.Text = "Kõrgeim skoor: " + highScore;
        }

        private void SaveHighScore()
        {
            File.WriteAllText("Score.txt", highScore.ToString());
        }      

        private void RestartButton_Click(object? sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            score = 0;
            timeLeft = 30;
            timeLabel.Text = "Aeg on jäänud: 30 sekundit";
            startButton.Enabled = true;
            timer.Stop();
            ResetControls();
        }

        private void ResetControls()
        {
            sum.Value = 0;
            sumMinus.Value = 0;
            sumMultiply.Value = 0;
            sumDivide.Value = 0;

            plusLeftLabel.Text = "0";
            plusRightLabel.Text = "+ 0 =";

            minusLeftLabel.Text = "0";
            minusRightLabel.Text = "- 0 =";

            timesLeftLabel.Text = "0";
            timesRightLabel.Text = "× 0 =";

            dividedLeftLabel.Text = "0";
            dividedRightLabel.Text = "÷ 0 =";

            additionCorrect = false;
            subtractionCorrect = false;
            multiplicationCorrect = false;
            divisionCorrect = false;
        }


        private void StartButton_Click(object? sender, EventArgs e)
        {
            StartQuiz();
            startButton.Enabled = false;
        }

        private void StartQuiz()
        {
            score = 0;
            timeLeft = 30;
            timeLabel.Text = "Aeg on jäänud: 30 sekundit";
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

            divisor = rand.Next(2, 11);

            int tempQuotient = rand.Next(2, 11);
            dividend = divisor * tempQuotient; 
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = "÷ " + divisor.ToString() + " =";
            sumDivide.Value = 0; 
        }

        private void QuizTimer_Tick(object? sender, EventArgs e)
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

                if (score > highScore)
                {
                    highScore = score;
                    highScoreLabel.Text = "Kõrgeim skoor: " + highScore;
                    SaveHighScore();
                }

                MessageBox.Show($"Aeg on läbi! Sinu tulemus on {score}.", "Mäng läbi");

                startButton.Enabled = true;
            }
        }

        //private void QuizTimer_Tick(object? sender, EventArgs e)
        //{
        //    if (timeLeft > 0)
        //    {
        //        timeLeft--;
        //        timeLabel.Text = $"Aeg on jäänud: {timeLeft} sekundid";
        //        CheckAnswer();
        //    }
        //    else
        //    {
        //        timer.Stop();
        //        timeLabel.Text = "Aeg on läbi!";

        //        MessageBox.Show($"Aeg on läbi! Sinu tulemus on {score}.", "Mäng läbi");

        //        startButton.Enabled = true;
        //    }
        //}

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

        private void answer_Enter(object? sender, EventArgs e)
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
