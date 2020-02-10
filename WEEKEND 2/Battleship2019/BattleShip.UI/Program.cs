using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BattleShip.BLL.Requests;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Splash screen
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(@"______  ___ _____ _____ _      _____ _____ _   _ ___________ ");
            Console.WriteLine(@"| ___ \/ _ \_   _|_   _| |    |  ___/  ___| | | |_   _| ___ \");
            Console.WriteLine(@"| |_/ / /_\ \| |   | | | |    | |__ \ `--.| |_| | | | | |_/ /");
            Console.WriteLine(@"| ___ \  _  || |   | | | |    |  __| `--. \  _  | | | |  __/ ");
            Console.WriteLine(@"| |_/ / | | || |   | | | |____| |___/\__/ / | | |_| |_| |    ");
            Console.WriteLine(@"\____/\_| |_/\_/   \_/ \_____/\____/\____/\_| |_/\___/\_|    ");
            Console.WriteLine(@"                                                             ");
            Console.ResetColor();

            //2. Ask for names
            Console.WriteLine("What is Player 1's name?");
            string p1Name = NameInput();
            Console.WriteLine("What is Player 2's name?");
            string p2Name = NameInput();

            //3. RNG for who goes first.
            Random rng = new Random();
            int currentTurn = rng.Next(2);

            bool keepPlaying = true;
            while (keepPlaying == true)
            {
                Console.Clear();

                //5. Workflow object of two boards, keep track of player's turn, and process turn.
                Board p1Board = new Board();
                Board p2Board = new Board();

                //Keep track of turn while placing ships.
                int placedCount = 0;
                while (placedCount != 2)
                {
                    Console.Clear();
                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                    
                    if (currentTurn == 0)
                    {
                        ShipPlacerPrompt(p1Board);
                    }
                    else if (currentTurn == 1)
                    {
                        ShipPlacerPrompt(p2Board);
                    }
                    placedCount = (placedCount + 1);
                }
                Console.Clear();

                //7. Show shot history, yellow "M" for miss, red "H" for hit. Prompt for coordinate entry.
                //Validate entry. If valid, create coordinate object, convert letter to number, call opponent board's FireShot().
                //Retrieve response from FireShot method and display corrosponding message.
                //Clear screen & loop back.

                currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);

                bool winner = false;

                while (winner == false)
                {
                    Console.WriteLine();
                    if (currentTurn == 0)
                    {
                        bool valid = false;
                        while (valid == false)
                        {
                            BoardPrinter(p2Board);
                            Console.WriteLine();
                            Console.WriteLine("Choose where to fire.");
                            FireShotResponse response = p2Board.FireShot(Coordinator());
                            switch (response.ShotStatus)
                            {
                                default:
                                case ShotStatus.Invalid:
                                    Console.WriteLine("Can't shoot there. Try again.");
                                    break;
                                case ShotStatus.Duplicate:
                                    Console.WriteLine("Can't shoot where you've already shot. Try again.");
                                    break;
                                case ShotStatus.Miss:
                                    Console.WriteLine("Miss...");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.Hit:
                                    Console.WriteLine("It's a hit!");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.HitAndSunk:
                                    Console.WriteLine("You sunk the enemy " + response.ShipImpacted + "!");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.Victory:
                                    Console.WriteLine(p1Name + " wins!");
                                    valid = true;
                                    winner = true;
                                    break;
                            }
                        }
                    }
                    else if (currentTurn == 1)
                    {
                        bool valid = false;
                        while (valid == false)
                        {
                            BoardPrinter(p1Board);
                            Console.WriteLine();
                            Console.WriteLine("Choose where to fire.");
                            FireShotResponse response = p1Board.FireShot(Coordinator());
                            switch (response.ShotStatus)
                            {
                                default:
                                case ShotStatus.Invalid:
                                    Console.WriteLine("Can't shoot there. Try again.");
                                    break;
                                case ShotStatus.Duplicate:
                                    Console.WriteLine("Can't shoot where you've already shot. Try again.");
                                    break;
                                case ShotStatus.Miss:
                                    Console.WriteLine("Miss...");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.Hit:
                                    Console.WriteLine("It's a hit!");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.HitAndSunk:
                                    Console.WriteLine("You sunk the enemy " + response.ShipImpacted + "!");
                                    Console.ReadLine();
                                    valid = true;
                                    Console.Clear();
                                    currentTurn = TurnSwitcher(currentTurn, p1Name, p2Name);
                                    break;
                                case ShotStatus.Victory:
                                    Console.WriteLine(p2Name + " wins!");
                                    valid = true;
                                    winner = true;
                                    break;
                            }
                        }
                    }
                }

                //8. When game is over, ask if want to play again. If yes, set up boards again. If no, quit.
                Console.WriteLine("Would you like to play again?");
                while (true)
                {
                    string playAgain = Console.ReadLine();
                    if (playAgain == "Yes" || playAgain == "yes")
                    {
                        Console.WriteLine("Restarting...");
                        break;
                    }
                    else if (playAgain == "No" || playAgain == "no")
                    {
                        Console.WriteLine("Thanks for playing, " + p1Name + " & " + p2Name + ".");
                        Console.ReadLine();
                        keepPlaying = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid.");
                    }
                }
            }
        }

        //4. Method for prompting, validating & translating user's coordinate input.
        static Coordinate Coordinator()
        {
            int numberX;
            int Y;

            Console.WriteLine("Where on the letter line (A-J)?");

            while (true)
            {
                string letterX = Console.ReadLine();
                if (letterX == "A" || letterX == "a")
                {
                    numberX = 1;
                    break;
                }
                else if (letterX == "B" || letterX == "b")
                {
                    numberX = 2;
                    break;
                }
                else if (letterX == "C" || letterX == "c")
                {
                    numberX = 3;
                    break;
                }
                else if (letterX == "D" || letterX == "d")
                {
                    numberX = 4;
                    break;
                }
                else if (letterX == "E" || letterX == "e")
                {
                    numberX = 5;
                    break;
                }
                else if (letterX == "F" || letterX == "f")
                {
                    numberX = 6;
                    break;
                }
                else if (letterX == "G" || letterX == "g")
                {
                    numberX = 7;
                    break;
                }
                else if (letterX == "H" || letterX == "h")
                {
                    numberX = 8;
                    break;
                }
                else if (letterX == "I" || letterX == "i")
                {
                    numberX = 9;
                    break;
                }
                else if (letterX == "J" || letterX == "j")
                {
                    numberX = 10;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid X.");
                }
            }

            Console.WriteLine("Where on the number line (1-10)?");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out Y) && Y <= 10 && Y >= 1)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Y.");
                }
            }

            Coordinate coordinate = new Coordinate(numberX, Y);
            return coordinate;
        }

        //Prompt types of ships to place. Uses ShipPlacer.
        static Board ShipPlacerPrompt(Board board)
        {
            Console.WriteLine("Where would you like to place your Battleship?");
            board = ShipPlacer(ShipType.Battleship, board);
            Console.WriteLine("Where would you like to place your Carrier?");
            board = ShipPlacer(ShipType.Carrier, board);
            Console.WriteLine("Where would you like to place your Cruiser?");
            board = ShipPlacer(ShipType.Cruiser, board);
            Console.WriteLine("Where would you like to place your Destroyer?");
            board = ShipPlacer(ShipType.Destroyer, board);
            Console.WriteLine("Where would you like to place your Submarine?");
            board = ShipPlacer(ShipType.Submarine, board);
            return board;
        }

        //6. Prompt ship placement on workflow boards using coordinates & direction (Left, right, Up, Down). Clear screen after.
        static Board ShipPlacer(ShipType shipType, Board board)
        {
            PlaceShipRequest request = new PlaceShipRequest();

            request.ShipType = shipType;

            Coordinate coordinate = Coordinator();
            request.Coordinate = coordinate;

            while (true)
            {
                Console.WriteLine("Which direction would you like to place the ship?");
                
                while (true)
                {
                    string shipDirection = Console.ReadLine();
                    if (shipDirection == "Up")
                    {
                        request.Direction = ShipDirection.Up;
                        break;
                    }
                    else if (shipDirection == "Down")
                    {
                        request.Direction = ShipDirection.Down;
                        break;
                    }
                    else if (shipDirection == "Left")
                    {
                        request.Direction = ShipDirection.Left;
                        break;
                    }
                    else if (shipDirection == "Right")
                    {
                        request.Direction = ShipDirection.Right;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid direction.");
                    }
                }
                ShipPlacement result = board.PlaceShip(request);
                if (result == ShipPlacement.Overlap || result == ShipPlacement.NotEnoughSpace)
                {
                    Console.WriteLine("Cannot place there.");
                }
                else
                {
                    break;
                }
            }
            return board;
        }

        //Method for getting valid name
        static string NameInput()
        {
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (name == "")
                {
                    Console.WriteLine("That name is invalid.");
                }
                else
                {
                    break;
                }
            }
            return name;
        }

        //Switches turn
        static int TurnSwitcher(int turn, string p1Name, string p2Name)
        {
            if (turn == 0)
            {
                turn = 1;
                Console.WriteLine("It's " + p2Name + "'s turn.");
            }
            else if (turn == 1)
            {
                turn = 0;
                Console.WriteLine("It's " + p1Name + "'s turn.");
            }
            Console.ReadKey();
            return turn;
        }

        static void BoardPrinter(Board board)
        {
            Console.WriteLine("   | A || B || C || D || E || F || G || H || I || J |");

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("---|-------------------------------------------------");

                if (i < 10)
                {
                    Console.Write(" " + i + " ");
                }
                else
                {
                    Console.Write("10 ");
                }

                for (int j = 1; j <= 10; j++)
                {
                    Coordinate coordinate = new Coordinate(j, i);
                    ShotHistory shot = board.CheckCoordinate(coordinate);

                    Console.Write("[ ");

                    if (shot == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("H");
                        Console.ResetColor();
                    }
                    else if (shot == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("M");
                        Console.ResetColor();
                    }
                    else if (shot == ShotHistory.Unknown)
                    {
                        Console.Write("X");
                    }

                    Console.Write(" ]");
                }
                Console.WriteLine("");
            }
        }
    }
}
