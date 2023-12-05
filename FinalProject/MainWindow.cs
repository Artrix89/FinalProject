using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Ink;

namespace FinalProject
{
    public partial class MainWindow : Form
    {
        private Match currentMatch;
        private int step;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI updating
        private void InitializeUIElements()
        {
            homeTeamText.Text = currentMatch.homeTeam;
            awayTeamText.Text = currentMatch.awayTeam;

            Batter tmpBatter;
            for(int i = 0; i < 9; i++)
            {
                tmpBatter = currentMatch.homeBatter;
                changeLine(homeBatterText, i, tmpBatter.name);
                changeLine(homeBatterStats, i, tmpBatter.battingPercentage.ToString("#.###" + " OPS"));
                tmpBatter = currentMatch.awayBatter;
                changeLine(awayBatterText, i, tmpBatter.name);
                changeLine(awayBatterStats, i, tmpBatter.battingPercentage.ToString("#.###" + " OPS"));
            }
            Pitcher tmpPitcher;
            for (int i = 0; i < 4; i++)
            {
                tmpPitcher = currentMatch.homePitcher;
                changeLine(homePitcherText, i, tmpPitcher.name);
                changeLine(homePitcherStats, i, tmpPitcher.earnedRunAverage.ToString("#.##" + " ERA"));
                tmpPitcher = currentMatch.awayPitcher;
                changeLine(awayPitcherText, i, tmpPitcher.name);
                changeLine(awayPitcherStats, i, tmpPitcher.earnedRunAverage.ToString("#.##" + " ERA"));
            }
        }

        private void changeLine(RichTextBox RTB, int line, string text)
        {
            int s1 = RTB.GetFirstCharIndexFromLine(line);
            int s2 = line < RTB.Lines.Count() - 1 ?
                      RTB.GetFirstCharIndexFromLine(line + 1) - 1 :
                      RTB.Text.Length;
            RTB.Select(s1, s2 - s1);
            RTB.SelectedText = text;
        }

        public void WriteToLog(string text)
        {
            gameLog.Invoke((MethodInvoker)(() => { 
                gameLog.Text = text;
                stepLabel.Text = "Step " + Convert.ToString(step);
                step++;
            }));
        }

        public async void WriteToScore(string text) 
        {
            scoreText.Invoke((MethodInvoker)(() => {
                scoreText.Visible = true;
                scoreText.Text = scoreText.Text + text;
            }));
            await Task.Delay(2000);
            scoreText.Invoke((MethodInvoker)(() => {
                scoreText.Text = "";
                scoreText.Visible = false; 
            }));

        }

        public void UpdateScore( int homePoints, int awayPoints )
        {
            homeScore.Invoke((MethodInvoker)(() => {
                homeScore.Text = Convert.ToString(homePoints);
                awayScore.Text = Convert.ToString(awayPoints);
            }));
        }

        public void ChangeHalf( string inning, bool isBottom )
        {
            if ( isBottom )
            {
                inningText.Invoke((MethodInvoker)(() => { 
                    inningText.Text = "BOTTOM " + inning;
                    batterText.BackColor = Color.DodgerBlue;
                    pitcherText.BackColor = Color.PaleVioletRed;
                }));

            }
            else
            {
                inningText.Invoke((MethodInvoker)(() => { 
                    inningText.Text = "TOP " + inning;
                    pitcherText.BackColor = Color.DodgerBlue;
                    batterText.BackColor = Color.PaleVioletRed;
                }));
            }
        }

        public void UpdateCurrentPlayers( string batter, string pitcher )
        {
            batterText.Invoke((MethodInvoker)(() => {
                batterText.Text = "Batting: " + batter;
                pitcherText.Text = "Pitching: " + pitcher;
            }));
        }

        public void UpdateStrikes( int strikes )
        {
            strikeOne.Invoke((MethodInvoker)(() => {
                if ( strikes == 0 )
                {
                    strikeOne.Visible = false;
                    strikeTwo.Visible = false;
                }
                else if ( strikes == 1 )
                {
                    strikeOne.Visible = true;
                    strikeTwo.Visible = false;
                }
                else if ( strikes >= 2 )
                {
                    strikeOne.Visible = true;
                    strikeTwo.Visible = true;
                }
            }));
        }

        public void UpdateBalls( int balls)
        {
            ballOne.Invoke((MethodInvoker)(() => {
                if (balls == 0)
                {
                    ballOne.Visible = false;
                    ballTwo.Visible = false;
                    ballThree.Visible = false;
                }
                else if (balls == 1)
                {
                    ballOne.Visible = true;
                    ballTwo.Visible = false;
                    ballThree.Visible = false;
                }
                else if (balls == 2)
                {
                    ballOne.Visible = true;
                    ballTwo.Visible = true;
                    ballThree.Visible = false;
                }
                else if (balls >= 3 )
                {
                    ballOne.Visible = true;
                    ballTwo.Visible = true;
                    ballThree.Visible = true;
                }
            }));
        }

        public void UpdateOuts( int outs )
        {
            outOne.Invoke((MethodInvoker)(() => {
                if (outs == 0)
                {
                    outOne.Visible = false;
                    outTwo.Visible = false;
                }
                else if (outs == 1)
                {
                    outOne.Visible = true;
                    outTwo.Visible = false;
                }
                else if (outs >= 2)
                {
                    outOne.Visible = true;
                    outTwo.Visible = true;
                }
            }));
        }

        public void UpdateBases( int code )
        { 

            baseOne.Invoke((MethodInvoker)(() => {
            if ( code % 2 == 1 ) 
            {
                baseOne.Visible = true;
                code = code - 1;
            }
            else 
                baseOne.Visible = false;

            if ( code % 4 == 2)
            {
                baseTwo.Visible = true;
                code = code - 2;
            }
            else 
                baseTwo.Visible = false;

            if ( code % 8 == 4)
            {
                baseThree.Visible = true;
                code = code - 3;
            }
            else
                baseThree.Visible = false;

            }));
        }

        public void EndGame()
        {
            currentMatch = null;
            seedText.ReadOnly = false;
        }

        #endregion

        private void startNewButton_Click(object sender, EventArgs e)
        {
            if (currentMatch != null)
            {
                System.Windows.MessageBox.Show("Match already exists!");
                return;
            }
            step = 1;
            currentMatch = new Match( this );
            seedText.ReadOnly = true;
            InitializeUIElements();
        }

        private void seededNewButton_Click(object sender, EventArgs e)
        {
            if (currentMatch != null)
            {
                System.Windows.MessageBox.Show("Match already exists!");
                return;
            }

            int seed;
            bool success = int.TryParse( seedText.Text, out seed );
            if ( success )
            {
                currentMatch = new Match(this);
                seedText.ReadOnly = true;
                InitializeUIElements();
                step = 1;
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter a valid seed number!");
            }
        }
    }
}
