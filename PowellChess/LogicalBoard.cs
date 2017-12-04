using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowellChess
{
    /// <summary>
    /// Used to represent the logic and enforce
    /// rules of a chess board.
    /// </summary>
    class LogicalBoard
    {
        /// <summary>
        /// represents the pieces on the board
        /// the key for int values to piece is as follows:
        /// 0 => blank square
        /// 2 => white king
        /// -1 => outside of board, used to make move validation simpler
        /// the 21st element is A1
        /// </summary>
        private int[] board;

        private int[] boardKey = new int[64] {
            21, 22, 23, 24, 25, 26, 27, 28,
            31, 32, 33, 34, 35, 36, 37, 38,
            41, 42, 43, 44, 45, 46, 47, 48,
            51, 52, 53, 54, 55, 56, 57, 58,
            61, 62, 63, 64, 65, 66, 67, 68,
            71, 72, 73, 74, 75, 76, 77, 78,
            81, 82, 83, 84, 85, 86, 87, 88,
            91, 92, 93, 94, 95, 96, 97, 98
        };

        /// <summary>
        /// constructor for logical board returns self
        /// with a fresh board setup and white to move
        /// </summary>
        public LogicalBoard()
        {
            board = new int[120] {
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, 0, 0, 0, 0, 2, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 2, 0, 0, 0, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
            };
        }
        
        public int[] RetrieveBoardState()
        {
            int[] ret = new int[64];

            for (int i = 0; i < 64; i++)
            {
                ret[i] = board[boardKey[i]];
            }

            return ret;
        }
        
        public void DiscoverMoves()
        {
            return;
        }
    }
}
