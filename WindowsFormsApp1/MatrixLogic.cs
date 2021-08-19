using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B21_Ex05;
namespace B21_Ex02
{
    public class MatrixLogic
    {
        public bool HasAWinner(char[,] i_Board)
        {
            bool thereIsAWinner;
            char[,] winnerToCheck = i_Board;
            if (MajorDiagonalChecking(winnerToCheck) ||
                ColSequenceChecking(winnerToCheck) ||
                SecondaryDiagonalChecking(winnerToCheck) ||
                RowSequenceChecking(winnerToCheck))
            {
                thereIsAWinner = true;
            }
            else
            {
                thereIsAWinner = false;
            }

            return thereIsAWinner;
        }

        public bool RowSequenceChecking(char[,] i_Matrix)
        {
            int sizeOfRows = i_Matrix.GetLength(0);
            bool isWinner = !true;
            for (int i = 0; i < sizeOfRows; i++)
            {
                bool isRowOkay = true;
                for (int j = 1; j < sizeOfRows; j++)
                {
                    if (i_Matrix[i, j] == ' ' || i_Matrix[i, j - 1] != i_Matrix[i, j])
                    {
                        isRowOkay = false;
                        j = sizeOfRows;
                    }
                }

                if (isRowOkay)
                {
                    isWinner = true;
                }
            }

            return isWinner;
        }

        public bool ColSequenceChecking(char[,] i_Matrix)
        {
            int sizeOfCols = i_Matrix.GetLength(0);
            bool isWinner = !true;
            for (int i = 0; i < sizeOfCols; i++)
            {
                bool isRowOkay = true;
                for (int j = 1; j < sizeOfCols; j++)
                {
                    if (i_Matrix[j, i] == ' ' || i_Matrix[j - 1, i] != i_Matrix[j, i])
                    {
                        isRowOkay = false;
                        j = sizeOfCols;
                    }
                }

                if (isRowOkay)
                {
                    isWinner = true;
                }
            }

            return isWinner;
        }

        public bool MajorDiagonalChecking(char[,] i_Matrix)
        {
            bool thereIsAWinner = true;
            for (int i = 0; i < i_Matrix.GetLength(0) - 1; i++)
            {
                if (i_Matrix[i, i] != 'O' && i_Matrix[i, i] != 'X')
                {
                    thereIsAWinner = false;
                    break;
                }

                if (i_Matrix[i, i] != i_Matrix[i + 1, i + 1])
                {
                    thereIsAWinner = false;
                    break;
                }
            }

            return thereIsAWinner;
        }

        public bool SecondaryDiagonalChecking(char[,] i_Matrix)
        {
            bool thereIsAWinner = true;
            int tempDiagonalLocation = i_Matrix.GetLength(0) - 1;
            for (int i = 0; i < i_Matrix.GetLength(0) - 1; i++)
            {
                if (i_Matrix[i, tempDiagonalLocation - i] != 'O' && i_Matrix[i, tempDiagonalLocation - i] != 'X')
                {
                    thereIsAWinner = false;
                    break;
                }

                if (i_Matrix[i, tempDiagonalLocation - i] != i_Matrix[i + 1, tempDiagonalLocation - i - 1])
                {
                    thereIsAWinner = false;
                    break;
                }
            }

            return thereIsAWinner;
        }

        public static string findWinner(ref PlayerDetails io_player1, ref PlayerDetails io_player2, string i_symbol)
        {
            if (io_player1.m_PlayerMove == i_symbol)
            {
                io_player2.m_PlayerScore++;
                return io_player2.m_PlayerName;
            }

            io_player1.m_PlayerScore++;
            return io_player1.m_PlayerName;
        }
    }
}
