using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            string winner = "Nobody";
            string str, player;
            int box;

            Console.WriteLine(
                "Welcome to my Tic-Tac-Toe game!\n"
                + "Type \"QUIT\" to quit at any time.\n"
                + "To choose a box, type the number of the corresponding box.\n"
            );

            b.clearBoard();
            b.draw();

            do
            {
                do
                {
                    player = b.getTurn().ToString();
                    Console.Write(player + " please select a box: ");
                    str = Console.ReadLine();

                    str = str.ToUpper();
                    if(str == "QUIT")
                    {
                        return;
                    }
                } while(!b.validInput(str));
                box = int.Parse(str);
                b.update(box);
                b.draw();
            } while (!b.win());

            // the player who made the previous move wins the game,
            // not the player that will make the current move
            if(b.getTurn() == PlayerTurn.Player2) 
            {
                winner = "Player 1";
            }
            else if (b.getTurn() == PlayerTurn.Player1)
            {
                winner = "Player 2";
            }

            Console.WriteLine(winner + " wins!!!");
            Console.ReadLine();
        }
    }
}
