using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace B21_Ex05
{
    public partial class GameSettings : Form
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        public string player1
        {
            get { return textBoxPlayer1.Text; }
        }

        public string player2 { get { return this.textBoxPlayer2.Text; } }

        public int matrixSize { get { return Convert.ToInt32(numericUpDownCols.Value); } }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void textBoxPlayer1_Click(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Value = numericUpDownRows.Value;
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Value = numericUpDownCols.Value;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPlayer1.Text))
            {
                MessageBox.Show("Please enter first player name!!!");
            }
            else if (checkBoxPlayer2.Checked && string.IsNullOrEmpty(textBoxPlayer2.Text))
            {
                MessageBox.Show("Please enter second player name!!!");
            }
            else
            {
                this.Hide();
                PlayerDetails player1 = new PlayerDetails("X", textBoxPlayer1.Text);
                PlayerDetails player2;

                // Single-player mode
                if (checkBoxPlayer2.Checked == false)
                {
                    player2 = new PlayerDetails("O", "Computer");
                }

                // Multi-player mode
                else
                {
                    player2 = new PlayerDetails("O", textBoxPlayer2.Text);
                }

                TicTacToeGame gameWindow = new TicTacToeGame((int)numericUpDownCols.Value, player1, player2);
                gameWindow.ShowDialog();
            }
        }

        private void GameSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
