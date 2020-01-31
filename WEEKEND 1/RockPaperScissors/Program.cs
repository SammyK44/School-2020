using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            bool keepPlaying = true;

            while (keepPlaying == true)
            {
                Console.WriteLine("Welcome to Rock Paper Scissors. How many rounds would you like to play?");
                if (int.TryParse(Console.ReadLine(), out int numberOfRounds) && (numberOfRounds >= 1) && (numberOfRounds <= 10))
                {
                    int roundsPlayed = 0;
                    int winCount = 0;
                    int lossCount = 0;
                    int tieCount = 0;

                    Random rng = new Random();

                    while (roundsPlayed < numberOfRounds)
                    {
                        int playerInput;

                        Console.WriteLine();
                        Console.WriteLine("Rock, paper, scissors!");
                        while (true)
                        {
                            Console.WriteLine("(1 for rock, 2 For paper, 3 for scissors.)");
                            if (int.TryParse(Console.ReadLine(), out playerInput) && (playerInput >= 1) && (playerInput <= 3))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Try again.");
                            }
                        }

                        int computerInput = rng.Next(1, 4);

                        if ((playerInput == 1 && computerInput == 2) || (playerInput == 3 && computerInput == 1) || (playerInput == 2 && computerInput == 3))
                        {
                            lossCount = lossCount + 1;
                            Console.WriteLine("You lost that round...");
                        }
                        else if ((playerInput == 1 && computerInput == 3) || (playerInput == 2 && computerInput == 1) || (playerInput == 3 && computerInput == 2))
                        {
                            winCount = winCount + 1;
                            Console.WriteLine("You won that round!");
                        }
                        else if (playerInput == computerInput)
                        {
                            tieCount = tieCount + 1;
                            Console.WriteLine("Tie round.");
                        }

                        roundsPlayed = roundsPlayed + 1;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Game over!");
                    if (winCount > lossCount)
                    {
                        Console.WriteLine("You won the game!");
                    }
                    else if (lossCount < winCount)
                    {
                        Console.WriteLine("You lost the game...");
                    }
                    else if (lossCount == winCount)
                    {
                        Console.WriteLine("Draw game!");
                    }

                    Console.WriteLine("You won " + winCount + " time(s), lost " + lossCount + " time(s), and had " + tieCount + " tie(s).");

                    Console.WriteLine("Would you like to play again?");
                    while (true)
                    {
                        string yesOrNo = Console.ReadLine();
                        if (yesOrNo == "yes")
                        {
                            Console.WriteLine("Alrighty, let's keep going!");
                            break;
                        }
                        else if (yesOrNo == "no")
                        {
                            Console.WriteLine("Aw, alright...goodbye!");
                            keepPlaying = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid. Try again.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("FATAL ERROR: Invalid round count.");
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
