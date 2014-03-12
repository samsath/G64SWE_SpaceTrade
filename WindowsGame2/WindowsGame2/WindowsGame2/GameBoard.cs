using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class GameBoard
    {
        public const int GameBoardWidth = 10;
        public const int GameBoardHeight = 10;
        private BoardLocation[] boardLocations = new BoardLocation[38];
        public GameBoard()
        {
            for (int i = 0; i < 38; i++)
            {
                boardLocations[i] = new BoardLocation("space");
            }
        }

        internal void addMovingShip(int p)
        {
            
        }
    }
}
