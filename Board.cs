using System;

namespace TicTacToe
{
    public interface IBoard
    {
        public void draw();
        public void clearBoard();
        public void update(int box);
        public void setBoxState(int box, BoxState state);
        public BoxState getBoxState(int box);
        public bool win();
        public PlayerTurn getTurn();
        public bool validInput (string str);
        public bool isFull();
    }

    public class Board : IBoard
    {
        public BoxState [,] board = new BoxState[3,3];
        public int turn = 0;
        public bool validInput (string str)
        {
            int box;

            if (
                str.Length > 1
                || !int.TryParse(str, out box)
                || box < 1
                || box > 9
            )
            {
                Console.WriteLine("Invalid input. Please try again...");
                return false;
            }

            box = int.Parse(str);
            BoxState state = getBoxState(box);
            if(state != BoxState.Empty)
            {
                Console.WriteLine("This space is taken. Please select a different box...");
                return false;
            }

            return true;
        }
        public void setBoxState(int box, BoxState state)
        {
            int x, y;
            boxToCoord(box, out x, out y);
            board[x,y] = state;
        }
        public bool isFull()
        {
            // checks if board is full
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if(board[x, y] == BoxState.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public bool win()
        {
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    // checks row
                    if(
                        board[x, y] == board[(x + 1) % 3, y]
                        && board[(x + 1) % 3, y] == board[(x + 2) % 3, y]
                    )
                    {
                        if(board[x, y] != BoxState.Empty)
                        {
                            return true;
                        }
                    }
                    // checks column
                    if(
                        board[x, y] == board[x, (y + 1) % 3]
                        && board[x, (y + 1) % 3] == board[x, (y + 2) % 3]
                    )
                    {
                        if(board[x, y] != BoxState.Empty)
                        {
                            return true;
                        }
                    }
                }

                if(board[1, 1] != BoxState.Empty) // only middle box since both diagonals pass it
                {
                    // checks if top right to bottom left diagonal is all equal
                    if(board[2, 0] == board[1, 1] && board [1, 1] == board[0, 2])
                    {
                        return true;
                    }
                    // checks if top left to bottom right diagonal is all equal
                    if(board[0, 0] == board[1, 1] && board [1, 1] == board[2, 2])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public BoxState getBoxState(int box)
        {
            int x, y;
            boxToCoord(box, out x, out y);
            BoxState state = this.board[x, y];
            return state;
        }
        public void clearBoard()
        {
            for(int y = 0; y < board.GetLength(1); y++)
            {
                for(int x = 0; x < board.GetLength(0); x++)
                {
                    board[x, y] = BoxState.Empty;
                }
            }
        }
        public PlayerTurn getTurn()
        {
            if(turn % 2 == 0)
            {
                return PlayerTurn.Player1;
            }
            return PlayerTurn.Player2;
        }
        public void update(int box)
        {
            PlayerTurn turn = getTurn();
            if(turn == PlayerTurn.Player1)
            {
                setBoxState(box, BoxState.X);
            }
            else if (turn == PlayerTurn.Player2)
            {
                setBoxState(box, BoxState.O);
            }

            this.turn++;
        }
        public void draw()
        {
            Console.WriteLine();

            int pos = 7;
            for(int i = 0; i < board.GetLength(1); i++)
            {
                for(int j = 0; j < board.GetLength(0); j++)
                {
                    BoxState boxState = getBoxState(pos);
                    if(boxState == BoxState.Empty)
                    {
                        Console.Write(" {0} ", pos); // bad coding practice but im lazy
                    }
                    else
                    {
                        Console.Write(" {0} ", boxState);
                    }

                    if(j < 2) // prints | after each number except the last
                    {
                        Console.Write('|');
                    }

                    pos++;
                }
                /*
                go to the next row of the numpad
                    7 8 9
                    4 5 6
                    1 2 3
                subtract by 6 instead of 5 because pos incriments 4 times instead of 3
                */
                pos -= 6;

                Console.WriteLine();
                if(i < 2) // prints divider except at the bottom
                {
                    Console.WriteLine("---+---+---");
                }
            }

            Console.WriteLine();
        }
        private void boxToCoord(int box, out int x, out int y)
        {
            switch(box)
            {
                case 1:
                case 4:
                case 7:
                    x = 0;
                    break;
                case 2:
                case 5:
                case 8:
                    x = 1;
                    break;
                case 3:
                case 6:
                case 9:
                    x = 2;
                    break;
                default:
                    x = 0;
                    break;
            }
            switch(box)
            {
                case 1:
                case 2:
                case 3:
                    y = 2;
                    break;
                case 4:
                case 5:
                case 6:
                    y = 1;
                    break;
                case 7:
                case 8:
                case 9:
                    y = 0;
                    break;
                default:
                    y = 0;
                    break;
            }
        }
    }
}