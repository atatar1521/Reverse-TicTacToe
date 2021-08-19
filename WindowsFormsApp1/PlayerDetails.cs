using System;
using System.Collections.Generic;
using System.Text;

namespace B21_Ex05
{
   public class PlayerDetails
    {
        public int m_PlayerScore = 0;        public string m_PlayerName;        public string m_PlayerMove;        public PlayerDetails(string i_PlayerMove, string i_UserName)        {            m_PlayerMove = i_PlayerMove;            m_PlayerName = i_UserName;        }
    }
}
