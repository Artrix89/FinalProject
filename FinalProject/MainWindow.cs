using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainWindow : Form
    {
        Match currentMatch;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region UI updating
        private void UpdateUIElements()
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

        void changeLine(RichTextBox RTB, int line, string text)
        {
            int s1 = RTB.GetFirstCharIndexFromLine(line);
            int s2 = line < RTB.Lines.Count() - 1 ?
                      RTB.GetFirstCharIndexFromLine(line + 1) - 1 :
                      RTB.Text.Length;
            RTB.Select(s1, s2 - s1);
            RTB.SelectedText = text;
        }
        #endregion

        private void startNewButton_Click(object sender, EventArgs e)
        {
            currentMatch = new Match();
            UpdateUIElements();
        }

        private void seededNewButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
