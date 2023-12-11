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
    public partial class CreateTeamForm : Form
    {
        #region variables
        MainWindow returnForm;
        #endregion

        public CreateTeamForm( MainWindow form)
        {
            InitializeComponent();
            returnForm = form;
        }

        #region methods
        private void CreateTeamForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            returnForm.TeamFormClose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (returnForm.IsGameRunning())
            {
                MessageBox.Show("Game created in main window, closing form");
                Close();
                return;
            }
            if (ValidateTextBoxes())
                return;

            double[] hBatterStats = new double[9];
            double[] hPitcherStats = new double[4];
            double[] aBatterStats = new double[9];
            double[] aPitcherStats = new double[4];
            double tmp;
            bool success;

            // Change Lines for stats into actual numbers
            for (int i = 0; i < 9; i++)
            {
                success = double.TryParse(HomeBatterStats.Lines[i], out tmp);
                if (success && tmp >= .1 && tmp <= .5)
                { hBatterStats[i] = tmp; }
                else
                { 
                    System.Windows.MessageBox.Show("Please enter a valid stat number for the batters!");
                    return;
                }
                success = double.TryParse(AwayBatterStats.Lines[i], out tmp);
                if (success && tmp >= .1 && tmp <= .5)
                { aBatterStats[i] = tmp; }
                else
                { 
                    System.Windows.MessageBox.Show("Please enter a valid stat number for the batters!");
                    return;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                success = double.TryParse(HomePitcherStats.Lines[i], out tmp);
                if (success && tmp >= 1 && tmp <= 7)
                { hPitcherStats[i] = tmp; }
                else
                { 
                    System.Windows.MessageBox.Show("Please enter a valid stat number for the pitchers!");
                    return;
                }
                success = double.TryParse(AwayPitcherStats.Lines[i], out tmp);
                if (success && tmp >= 1 && tmp <= 7)
                { aPitcherStats[i] = tmp; }
                else
                { 
                    System.Windows.MessageBox.Show("Please enter a valid stat number for the pitchers!");
                    return;
                }
            }

            returnForm.CreateCustomGame(HomeTeamText.Text, HomeBatterNames.Lines, hBatterStats, HomePitcherNames.Lines, hPitcherStats,
                AwayTeamText.Text, AwayBatterNames.Lines, aBatterStats, AwayPitcherNames.Lines, aPitcherStats);

            Close();
        }

        private bool ValidateTextBoxes()
        {
            //Check if team text is empty
            if (string.IsNullOrWhiteSpace(HomeTeamText.Text) || string.IsNullOrWhiteSpace(AwayTeamText.Text)) 
            {
                MessageBox.Show("Team Text may not be empty");
                return true;
            }

            try
            {
                for (int i = 0; i < 9; i++) //Check if batter name/stats are empty
                {
                    if (string.IsNullOrWhiteSpace(HomeBatterNames.Lines[i]) || string.IsNullOrWhiteSpace(AwayBatterNames.Lines[i]))
                    {
                        MessageBox.Show("Batter Name may not be empty");
                        return true;
                    }
                    if (string.IsNullOrWhiteSpace(HomeBatterStats.Lines[i]) || string.IsNullOrWhiteSpace(AwayBatterStats.Lines[i]))
                    {
                        MessageBox.Show("Batter Stats may not be empty");
                        return true;
                    }
                }
            }
            catch { 
                MessageBox.Show("You must have 9 batters");
                return true;
            }

            //Check if there are more than 9 batters
            if (HomeBatterNames.Lines.Length > 9 || AwayBatterNames.Lines.Length > 9)
            {
                MessageBox.Show("There can only be 9 batters/stats");
                return true;
            }
            if (HomeBatterStats.Lines.Length > 9 || AwayBatterStats.Lines.Length > 9)
            {
                MessageBox.Show("There can only be 9 batters/stats");
                return true;
            }

            try
            {
                for (int i = 0; i < 4; i++) //Check if pitcher name/stats are empty
                {
                    if (string.IsNullOrWhiteSpace(HomePitcherNames.Lines[i]) || string.IsNullOrWhiteSpace(AwayPitcherNames.Lines[i]))
                    {
                        MessageBox.Show("Batter Name may not be empty");
                        return true;
                    }
                    if (string.IsNullOrWhiteSpace(HomePitcherStats.Lines[i]) || string.IsNullOrWhiteSpace(AwayPitcherStats.Lines[i]))
                    {
                        MessageBox.Show("Batter Stats may not be empty");
                        return true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("You must have 4 pitchers");
                return true;
            }

            //Check if there are more than 4 pitchers
            if (HomePitcherNames.Lines.Length > 4 || AwayPitcherNames.Lines.Length > 4)
            {
                MessageBox.Show("There can only be 4 pitchers/stats");
                return true;
            }
            if (HomePitcherStats.Lines.Length > 4 || AwayPitcherStats.Lines.Length > 4)
            {
                MessageBox.Show("There can only be 4 pitchers/stats");
                return true;
            }

            return false;
        }
        #endregion
    }
}
