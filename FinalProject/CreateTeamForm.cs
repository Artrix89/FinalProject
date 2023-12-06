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
        MainWindow returnForm;
        public CreateTeamForm( MainWindow form)
        {
            InitializeComponent();
            returnForm = form;
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
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



            //returnForm.CreateCustomGame(HomeTeamText.Text, HomeBatterNames.Lines, HomeBatterStats.Lines, HomePitcherNames.Lines, HomePitcherStats.Lines,
            //AwayTeamText.Text, AwayBatterNames.Lines, AwayBatterStats.Lines, AwayPitcherNames.Lines, AwayPitcherStats.Lines);

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

    }
}
