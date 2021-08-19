using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B21_Ex02;
namespace B21_Ex05
{
    public partial class TicTacToeGame : Form
    {
        private static int s_CurrentTurn;        private int m_MatrixSize;        private Button[,] m_GameTable;        private Label m_Player1;        private Label m_Player2;        private PlayerDetails m_PlayerDetails1;        private PlayerDetails m_PlayerDetails2;
        private Random rnd = new Random();

        public TicTacToeGame(int i_MatrixSize, PlayerDetails i_Player1, PlayerDetails i_Player2)
        {
            m_MatrixSize = i_MatrixSize;            s_CurrentTurn = 0;            m_PlayerDetails1 = i_Player1;            m_PlayerDetails2 = i_Player2;            InitializeComponent();            this.m_GameTable = new Button[m_MatrixSize, m_MatrixSize];            TicTacToeGame_CreateTable();

            // adjust the window to fit the buttons
            this.ClientSize = this.ClientSize = new System.Drawing.Size(18 + (i_MatrixSize * 55), 50 + (i_MatrixSize * 50));            TicTacToeGame_createLabels(i_Player1, i_Player2);
        }

        public void EnterComputerMove(char[,] i_Board, PlayerDetails i_Player1, PlayerDetails i_Player2)
        {
            int row = rnd.Next(i_Board.GetLength(0));
            int col = rnd.Next(i_Board.GetLength(0));
            while (i_Board[row, col] != ' ')
            {
                row = rnd.Next(i_Board.GetLength(0));
                col = rnd.Next(i_Board.GetLength(0));
            }

            i_Board[row, col] = Convert.ToChar(i_Player1.m_PlayerMove);
        }

        public static bool TryInsert(char i_symbol, ref char[,] io_table, int i_rowInput, int i_colInput)
        {
            bool isChanged = false;

            if (io_table[i_rowInput, i_colInput] == ' ')
            {
                io_table[i_rowInput, i_colInput] = i_symbol;
                isChanged = true;
            }

            return isChanged;
        }

        public void TicTacToeGame_CreateTable()        {            for (int i = 0; i < m_GameTable.GetLength(0); i++)            {                for (int j = 0; j < m_GameTable.GetLength(0); j++)                {                    m_GameTable[i, j] = new Button();                    m_GameTable[i, j].Location = new System.Drawing.Point(9 + (i * 55), 9 + (j * 50));                    m_GameTable[i, j].Margin = new Padding(2, 2, 2, 2);                    m_GameTable[i, j].Name = "button" + (i + j);                    m_GameTable[i, j].Size = new System.Drawing.Size(55, 50);                    m_GameTable[i, j].TabStop = false;                    m_GameTable[i, j].Text = " ";                    m_GameTable[i, j].UseVisualStyleBackColor = true;                    m_GameTable[i, j].Show();                    Controls.Add(this.m_GameTable[i, j]);                    m_GameTable[i, j].Click += new System.EventHandler(this.button_Click);                }            }        }

        private void TicTacToeGame_createLabels(PlayerDetails i_Player1, PlayerDetails i_Player2)
        {
            // player1 label
            m_Player1 = new Label();
            m_Player1.AutoSize = true;
            m_Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            m_Player1.Margin = new Padding(2, 0, 2, 0);
            m_Player1.Name = "labelPlayer1";
            m_Player1.Size = new System.Drawing.Size(64, 17);
            m_Player1.TabStop = false;
            m_Player1.Text = string.Format("{0}: {1}", i_Player1.m_PlayerName, i_Player1.m_PlayerScore);
            m_Player1.Location = new System.Drawing.Point(10, ClientSize.Height - 43);

            // player2 label
            m_Player2 = new Label();
            m_Player2.AutoSize = true;
            m_Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            m_Player2.Margin = new Padding(2, 0, 2, 0);
            m_Player2.Name = "labelPlayer2";
            m_Player2.Size = new System.Drawing.Size(64, 17);
            m_Player2.TabStop = false;
            m_Player2.Text = string.Format("{0}: {1}", i_Player2.m_PlayerName, i_Player2.m_PlayerScore);
            m_Player2.Location = new System.Drawing.Point(10, ClientSize.Height - 25);

            // adding
            Controls.Add(m_Player1);
            Controls.Add(m_Player2);
        }

        public void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            UpdateButtonAndLable(button);
            if (isWon())
            {
                DoWhenThereIsAWin();
            }
            else
            {
                button.Enabled = false;
                s_CurrentTurn++;
                if (isTie())
                {
                    DoWhenIsATie();
                }
            }
        }

        public void UpdateButtonAndLable(Button i_Button)
        {
            if (s_CurrentTurn % 2 == 0)
            {
                i_Button.Text = m_PlayerDetails1.m_PlayerMove;
                i_Button.BackColor = Color.GreenYellow;
                m_Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                m_Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            }
            else
            {
                i_Button.Text = m_PlayerDetails2.m_PlayerMove;
                i_Button.BackColor = Color.Gray;
                m_Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
                m_Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            }

            if (m_PlayerDetails2.m_PlayerName == "Computer" && !isWon() && !isTie())
            {
                char[,] symbols = toChars();
                this.EnterComputerMove(symbols, m_PlayerDetails2, m_PlayerDetails1);
                locateNewSymbolInButton(symbols);
                s_CurrentTurn++;
            }
        }

        public void UpdatePlayerWinnings(string i_WinnerName, PlayerDetails i_PlayerDetails, Label i_PlayerLabel)
        {
            i_PlayerLabel.Text = string.Format("{0}: {1}", i_PlayerDetails.m_PlayerName, i_PlayerDetails.m_PlayerScore);
        }

        public void WinnerOrTieMessageBox(string i_output, string i_Title)
        {
            if (MessageBox.Show(i_output, i_Title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clearTable();
            }
            else
            {
                this.Close();
            }
        }

        public void DoWhenThereIsAWin()
        {
            string currentSymbol = (Convert.ToChar(s_CurrentTurn % 2) == 1) ? "O" : "X";
            string winner = MatrixLogic.findWinner(ref m_PlayerDetails1, ref m_PlayerDetails2, currentSymbol);
            string output = string.Format(
                @"
        The winner is {0},
        would you like to play another round?", winner);
            if (winner == m_PlayerDetails1.m_PlayerName)
            {
                UpdatePlayerWinnings(winner, m_PlayerDetails1, m_Player1);
                s_CurrentTurn--;
            }
            else
            {
                UpdatePlayerWinnings(winner, m_PlayerDetails2, m_Player2);
            }

            WinnerOrTieMessageBox(output, "A Win");
        }

        public void DoWhenIsATie()
        {
            string output = string.Format(@"
        Its a tie,
        would you like to play another round?");
            if (Convert.ToChar(s_CurrentTurn % 2) == 1)
            {
                s_CurrentTurn--;
            }

            WinnerOrTieMessageBox(output, "A Tie");
        }

        // Finds out if the table is full.
        private bool isTie()
        {
            bool isContainingEmptyButton = true;

            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    if (m_GameTable[i, j].Text == " ")
                    {
                        isContainingEmptyButton = false;
                        break;
                    }
                }
            }

            return isContainingEmptyButton;
        }

        // Finds out if there is a sequence.
        private bool isWon()
        {
            MatrixLogic logicCheck = new MatrixLogic();
            char currentSymbol = Convert.ToChar(s_CurrentTurn % 2);
            char[,] symbols = toChars();

            return logicCheck.HasAWinner(symbols);
        }

        // Makes an array of chars for the buttons symbols.
        private char[,] toChars()
        {
            char[,] symbols = new char[m_MatrixSize, m_MatrixSize];

            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    symbols[i, j] = m_GameTable[i, j].Text[0];
                }
            }

            return symbols;
        }

        // Takes a chars array and make it identical to the texts in the buttons.
        private void locateNewSymbolInButton(char[,] i_symbols)
        {
            m_Player1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            m_Player2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    if (m_GameTable[i, j].Text[0] != i_symbols[i, j])
                    {
                        m_GameTable[i, j].Text = i_symbols[i, j].ToString();
                        m_GameTable[i, j].Enabled = false;
                        m_GameTable[i, j].BackColor = Color.Gray;
                        break;
                    }
                }
            }
        }

        // Clearing talbe for the next game.
        private void clearTable()        {            for (int i = 0; i < m_MatrixSize; i++)            {                for (int j = 0; j < m_MatrixSize; j++)                {
                    m_GameTable[i, j].Text = " ";                }            }            for (int i = 0; i < m_GameTable.GetLength(0); i++)            {                for (int j = 0; j < m_GameTable.GetLength(0); j++)                {
                    m_GameTable[i, j].Text = " ";
                    m_GameTable[i, j].UseVisualStyleBackColor = true;
                    m_GameTable[i, j].Enabled = true;
                }
            }
        }

        private void TicTacToeGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void TicTacToeGame_Load(object sender, EventArgs e)
        {
        }
    }
}