using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quasar
{
    class Game
    {
        public double PlayerCredits { get; set; }
        public int PlayerBet { get; set; }
        public int Number { get; set; }

        public Game()
        {
            Console.SetWindowSize(61, 20);
            Console.SetBufferSize(61, 20);
            Play();
        }

        void DisplayInfo()
        {
            DisplayTitle();
            DisplayStats();
        }

        void DisplayTitle()
        {
            Console.WriteLine("  /$$$$$$");
            Console.WriteLine(" /$$__  $$");
            Console.WriteLine("| $$  \\ $$ /$$   /$$  /$$$$$$   /$$$$$$$  /$$$$$$   /$$$$$$");
            Console.WriteLine("| $$  | $$| $$  | $$ |____  $$ /$$_____/ |____  $$ /$$__  $$");
            Console.WriteLine("| $$  | $$| $$  | $$  /$$$$$$$|  $$$$$$   /$$$$$$$| $$  \\__/");
            Console.WriteLine("| $$/$$ $$| $$  | $$ /$$__  $$ \\____  $$ /$$__  $$| $$");
            Console.WriteLine("|  $$$$$$/|  $$$$$$/|  $$$$$$$ /$$$$$$$/|  $$$$$$$| $$");
            Console.WriteLine(" \\____ $$$ \\______/  \\_______/|_______/  \\_______/|__/");
            Console.WriteLine("      \\__/");
        }

        void DisplayStats()
        {
            Console.WriteLine("\nCredits: {0}", this.PlayerCredits);
            Console.WriteLine("Current bet: {0}", this.PlayerBet);
            Console.WriteLine("Current number: {0}", this.Number);
        }

        void Play()
        {
            Random rng = new Random();
            bool cashOut = false;
            double payout = 0;
            ConsoleKey input;

            while (true)
            {
                while (this.PlayerCredits == 0)
                {
                    DisplayInfo();
                    Console.WriteLine("\nPLEASE INSERT CREDITS.");
                    InsertCredits();
                    Console.Clear();
                }

                while (this.PlayerBet == 0)
                {
                    DisplayInfo();
                    Bet();
                    Console.Clear();
                }

                this.Number += rng.Next(1, 10);

                while (this.Number <= 20 && !cashOut)
                {
                    DisplayInfo();
                    Console.WriteLine("\nWhat's your move?");
                    if (this.Number >= 17)
                    {
                        Console.WriteLine("Press 1 to add 1-8. Press 2 to add 4-7. Press 3 to pay out.");
                    }
                    else
                    {
                        Console.WriteLine("Press 1 to add 1-8. Press 2 to add 4-7.");
                    }
                    bool inputValid = false;
                    do
                    {
                        input = Console.ReadKey(true).Key;
                        if (this.Number >= 17 && (input == ConsoleKey.D1 || input == ConsoleKey.D2 || input == ConsoleKey.NumPad1 || input == ConsoleKey.NumPad2 || input == ConsoleKey.D3 || input ==
                            ConsoleKey.NumPad3))
                        {
                            inputValid = true;
                        }
                        else if (input == ConsoleKey.D1 || input == ConsoleKey.D2 || input == ConsoleKey.NumPad1 || input == ConsoleKey.NumPad2)
                        {
                            inputValid = true;
                        }
                    } while (!inputValid);

                    switch (input)
                    {
                        case ConsoleKey.D1:
                            this.Number += rng.Next(1, 9);
                            break;
                        case ConsoleKey.D2:
                            this.Number += rng.Next(4, 8);
                            break;
                        case ConsoleKey.NumPad1:
                            this.Number += rng.Next(1, 9);
                            break;
                        case ConsoleKey.NumPad2:
                            this.Number += rng.Next(4, 8);
                            break;
                        case ConsoleKey.D3:
                            cashOut = true;
                            break;
                        case ConsoleKey.NumPad3:
                            cashOut = true;
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
                }
                DisplayInfo();
                if (this.Number > 20)
                {
                    Console.WriteLine("\nYOU WENT OVER. Press any key to continue . . .");
                }
                else
                {
                    payout = Payout();
                    Console.WriteLine("\nYou've won {0} credits! Press any key to continue . . .", payout);
                }
                Console.ReadKey();
                this.PlayerCredits += payout;
                this.PlayerBet = 0;
                this.Number = 0;
                payout = 0;
                cashOut = false;
                Console.Clear();
            }
        }

        double Payout()
        {
            double payout = 0;
            switch (this.Number)
            {
                case 17:
                    payout = this.PlayerBet;
                    break;
                case 18:
                    payout = this.PlayerBet * 1.25;
                    break;
                case 19:
                    payout = this.PlayerBet * 1.5;
                    break;
                case 20:
                    payout = this.PlayerBet * 2;
                    break;
                default:
                    break;
            }

            return payout;
        }

        void Bet()
        {
            int validAmount;

            Console.Write("\nHow much do you want to bet? ");
            string credits = Console.ReadLine();

            if (int.TryParse(credits, out validAmount))
            {
                if (validAmount <= this.PlayerCredits)
                {
                    this.PlayerCredits -= validAmount;
                    this.PlayerBet = validAmount;
                }
            }
        }

        void InsertCredits()
        {
            int validAmount;

            Console.Write("How many credits would you like to insert? ");
            string credits = Console.ReadLine();

            if (int.TryParse(credits, out validAmount))
            {
                this.PlayerCredits += validAmount;
            }
        }
    }
}
