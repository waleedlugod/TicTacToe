using System;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Board b = new Board();
            string winner = "Nobody";
            string player, option;
            int box;

            Console.WriteLine(
                "Welcome to my Tic-Tac-Toe game!\n"
                + "Type \"QUIT\" to quit at any time.\n"
                + "To choose a box, type the number of the corresponding box.\n"
                + "Type \"c\" to play with a computer, or any other key to play locally.\n"
            );

            option = Console.ReadLine();
            option = option.ToUpper();

            b.clearBoard();
            b.draw();

            do
            {
                // skip input from player if computer mode and player 2 turn
                if (option != "C" || b.getTurn() != PlayerTurn.Player2)
                {
                    string str;
                    do
                    {
                        player = b.getTurn().ToString();
                        Console.Write(player + " please select a box: ");
                        str = Console.ReadLine();

                        str = str.ToUpper();
                        if (str == "QUIT")
                        {
                            return;
                        }
                    } while (!b.validInput(str));
                    // if computer mode is not chosen then box is rewritten
                    box = int.Parse(str);
                }
                else
                {
                    
                    // random move is decided for computer
                    do
                    {
                        box = r.Next(1, 9);
                    } while(b.getBoxState(box) != BoxState.Empty);
                    Console.WriteLine("Computer chose box " + box);
                }

                b.update(box);
                b.draw();
            } while (b.win() == false && b.isFull() == false);

            // the player who made the previous move wins the game,
            // not the player that will make the current move
            
            if(b.win() == true)
            {
                if(b.getTurn() == PlayerTurn.Player2) 
                {
                    winner = "Player 1";
                }
                else if (b.getTurn() == PlayerTurn.Player1)
                {
                    winner = "Player 2";
                }
            }
            Console.WriteLine(winner + " wins!!!");
            Console.ReadLine();
        }
    }
}
