using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame2
{
    class Board
    {
        public const int GameBoardWidth = 10;
        public const int GameBoardHeight = 10;
        private Tile[] boardLocations = new Tile[38];
        public Board()
        {
            for (int i = 0; i < 38; i++)
            {
                boardLocations[i] = new Tile("space");
            }
        }

        internal void addMovingShip(int p)
        {
            
        }
    }
}
