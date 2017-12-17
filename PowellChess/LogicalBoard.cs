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
    /// The firstmost square in a board representation
    /// array will always correspond to A1 on the chess board. 
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

        /// <summary>
        /// transforms index on 64 square board into piece on 120 square board
        /// </summary>
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
        /// stores which sides turn it is
        /// 0 for white, 1 for black        
        /// </summary>
        private int turn = 0;

        /// <summary>
        /// board index of the currently selected piece
        /// used to save state of whether piece should be
        /// moved or highlighted on click
        /// selectedPieceIndex uses 120 square board indexing
        /// </summary>
        private int selectedPieceIndex = -1;

        /// <summary>
        /// array to remember the possible squares the selected piece could move to
        /// </summary>
        private int[] possibleMoves = new int[] {
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, 0, 0, 0, 0, 0, 0, 0, 0, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
        };

        /// <summary>
        /// arrays of move offsets used when generating possible moves
        /// </summary>
        private int[] kingDirections = { -11, -10, -9, -1, 1, 9, 10, 11 };

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
                -1, 0, 0, 0, 0, 12, 0, 0, 0, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
            };
        }
        
        /// <summary>
        /// Returns the current status of all squares.
        /// </summary>
        /// <returns>Array of the pieces on the board.</returns>
        public int[] GetBoardState()
        {
            int[] ret = new int[64];

            for (int i = 0; i < 64; i++)
            {
                ret[i] = board[boardKey[i]];
            }

            return ret;
        }

        /// <summary>
        /// Returns array of any squares that could be moved to
        /// and need to be highlighted.
        /// </summary>
        /// <returns></returns>
        public int[] GetHighlightedSquares()
        {
            int[] ret = new int[64];

            for (int i = 0; i < 64; i++)
            {
                ret[i] = possibleMoves[boardKey[i]];
            }

            return ret;
        }

        //TODO probably unneeded
        /// <summary>
        /// Return a boolean distinguishing whether the clicked square
        /// has a piece of color.
        /// </summary>
        /// <param name="color">0 for white 1 for black</param>
        /// <returns>bool</returns>
        public bool HasPiece(int color)
        {
            return true;
        }

        /// <summary>
        /// Determines if the clicked square has a piece to be moved
        /// and if it that piece's color's turn. Returns true if new
        /// squares should be highlighted otherwise false.
        /// </summary>
        /// <param name="bi">board index of click</param>
        /// <returns>bool</returns>
        public void ClickedAt(int bi)
        {
            // 10x12 board contents of click position
            int p = board[boardKey[bi]];

            // index on 10x12 board of click
            int index = boardKey[bi];

            // selecting a piece, highlight here
            if (selectedPieceIndex == -1)
            {
                // selecting white piece
                if (turn == 0 && (p <= 10 && p > 0))
                {
                    DiscoverMoves(bi);
                    selectedPieceIndex = index;
                }
                // selecting black piece
                else if (turn == 1 && (p <= 20 && p > 10))
                {
                    DiscoverMoves(bi);
                    selectedPieceIndex = index;
                }
            }
            // attempting to move, check for valid move otherwise deselect
            else
            {
                //if (turn == 0 && (p <= 10 && p > 0))
                //{
                if (possibleMoves[index] == 1)
                {
                    PerformMove(selectedPieceIndex, index);
                    ClearPossibleMoves();
                }
                //}
                //else if (turn == 1 && (p <= 20 && p > 10))
                //{
                //if (possibleMoves[index] == 1)
                //{
                //    PerformMove(selectedPieceIndex, index);
                //    ClearPossibleMoves();
                //}
                //}
            }
        }

        /// <summary>
        /// Discover moves for a given piece.
        /// </summary>
        public void DiscoverMoves(int bi)
        {
            // piece on 120 square board
            int p = board[boardKey[bi]];
            int startingIndex = boardKey[bi];

            // TODO determine whether piece is on the same side
            int side;
            int oppside;

            // Kings moving
            if (p == 2 || p == 12)
            {
                for (int t = 0; t < 8; t++)
                {
                    // TODO opposite side and captures
                    // move to empty square
                    if (board[startingIndex + kingDirections[t]] == 0)
                    {
                        possibleMoves[startingIndex + kingDirections[t]] = 1;
                    }
                }
            }
            //return;
        }

        /// <summary>
        /// Move piece from selected piece index into the new position.
        /// </summary>
        /// <param name="oldPos">from index on 120 square board</param>
        /// <param name="newPos">to index on 120 square board</param>
        private void PerformMove(int oldPos, int newPos)
        {
            int temp = board[oldPos];
            board[newPos] = temp;
            board[oldPos] = 0;
            turn = ((turn == 0) ? 1 : 0);
        }

        /// <summary>
        /// Resets the possible moves board and selected piece.
        /// </summary>
        private void ClearPossibleMoves()
        {
            selectedPieceIndex = -1;

            for (int c = 0; c < 64; c++)
            {
                possibleMoves[boardKey[c]] = 0;
            }
        }

        /// <summary>
        /// Resets all the logical functionality of the board
        /// </summary>
        public void ResetBoard()
        {

        }
    }
}
