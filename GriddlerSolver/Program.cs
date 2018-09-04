using System;

namespace GriddlerSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var challenge = GetChallenge(6);

            Console.WriteLine($"{challenge.Name}: Size to solve: {challenge.Height} rows and {challenge.Width} columns.");
            System.Threading.Thread.Sleep(1000);
            var orgbg = Console.BackgroundColor;
            var orgfg = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            var solver = new Solver(challenge);

            int iterations = 0;
            foreach (var partial in solver.Solve())
            {
                iterations++;
                Console.WriteLine(partial.ToString());
                System.Threading.Thread.Sleep(200);
            }

            Console.BackgroundColor = orgbg;
            Console.ForegroundColor = orgfg;

            Console.WriteLine($"'{challenge.Name}', {solver.Completeness * 100}% complete in {iterations} iterations.");
            Console.Write("Press enter > ");
            Console.ReadLine();
        }

        private static Griddler GetChallenge(int index)
        {
            Griddler g;
            switch (index)
            {
                case 0: // 
                    g = new Griddler("Apricot", 15, 20);
                    g.SetColumnClues("1,1,7; 2,3,2; 2,2,3,6; 1,1,4,2,3; 1,4,2,2,3;" + "3,5,1,2; 2,1,2,3; 4,1,4,2,1,1; 2,4,2,1,4; 3,2,4,2;" +
                        "2,1,1,4,2; 2,1,1,1,1,5; 3,1,2,4; 3,2,2,2; 2,7");
                    g.SetRowClues("3,6; 14; 1,5,2; 4,1,1; 2,1,2;" + "1,2,4; 2,2,3,2; 1,2,1,1,2; 1,2,1,1,1,1; 3,1,1,1,1;" +
                        "2,2,1,1,3; 1,3,2,1,2; 1,2,2,3,1,1; 1,2,6; 1,1,7;" + "1,1,4; 2,1,1,1; 3,3,2; 4,3; 6");
                    g.SanityCheck();
                    return g;

                case 1:
                    // no solution (without guessing)
                    g = new Griddler("Flower", 10, 10);
                    g.SetColumnClues("2; 1,1; 2,1,1; 1,1,2; 2,1,2;" + "1,2,4; 1,2,4; 2,1; 1,1; 2");
                    g.SetRowClues("2; 1,1; 2,2; 1,2,1; 1,2,1;" + "2,2; 3,2; 1,1,2; 6; 3");
                    g.SanityCheck();
                    return g;

                case 2:
                    // no solution (85%)
                    g = new Griddler("Red Squirrel", 20, 20);
                    g.SetColumnClues("2,3,2,3; 1,2,1,1,2,2; 3,1,1,2,6; 1,1,1,2,6; 3,1,1,2,1,1;" + "1,1,1,1,3; 1,2,2,2,1; 1,2,3; 2,1,1,1,5,2; 3,3,2,2;" +
                        "3,3,4; 11,4; 4,3,6,1; 7,1; 7,7,1;" + "9,8; 1,9,7; 1,13,2; 1,4,8; 3,6");
                    g.SetRowClues("1,1,1,3; 1,1,1,2,1,1; 1,1,2,2,2,1,1; 1,2,4,3,1; 1,4,5;" + "2,2,1,3,5; 3,1,1,2,5; 1,2,3,4; 2,5,5; 2,1,3,6;" +
                        "1,1,2,2,5; 7,1,4,4; 3,1,4,3; 1,1,3,9; 4,14;" + "2,2,9; 1,2,2,5; 1,2,1,3,4; 4,2,2,3; 5,6");
                    g.SanityCheck();
                    return g;

                case 3:
                    g = new Griddler("Chicken", 15, 15);
                    g.SetColumnClues("3,1; 5,1,1,5; 4,2,1,1; 2,1,1,2; 3,3,1;" + "4,1,2,1; 3,1; 3,1,2,1; 4,2,4; 4,2,1;" +
                        "2,2,5; 2,3,1,1; 4,1,2; 1,2; 4");
                    g.SetRowClues("2,2,1,2; 5,5; 6,4,1; 3,5,2; 2,1,3,1,2;" + "1,1,3,1; 3,1,2,1; 1,2,2; 1,1,2,2; 1,1,2,1;" +
                    "1,3,2; 1,2,4; 3,1,1,1; 1,1,1; 1,2,2");
                    g.SanityCheck();
                    return g;

                case 4: // 80% complete
                    g = new Griddler("Ice Cream", 15, 15);
                    g.SetColumnClues("1; 3,4; 3,4,1; 7,1; 7,1,1;" + "1,5,1,1,1; 1,1,4,2,1; 2,1,3,3; 1,1,1,1,3; 2,1,1,2,1;" +
                        "4,1,1,1,1; 3,2,1,1; 1,1,1; 1,1,1; 4");
                    g.SetRowClues("1; 3,5; 3,1,1,3; 3,1,1,2; 3,1,4;" + "5,1,1; 7,1; 7,1; 14; 1,1,1,1;" +
                      "1,1,1,1; 2,1,1,2; 6; 2; 8");
                    g.SanityCheck();
                    return g;

                case 5: // 25x30
                    g = new Griddler("Pelican", 25, 30);
                    g.SetColumnClues("8; 9; 7,3; 8,2; 8,2;" + "10,2,1,1; 9,5,2; 9,10; 9,6,2; 8,11;" +
                        "6,2,4,7,2; 9,1,3,7,2; 13,9,1; 5,17,1; 1,4,14,1;" + "2,4,12,1; 1,4,8; 1,5,4; 1,5; 1,5;" +
                        "1,4; 1,4; 1,3; 1,2; 1,1");
                    g.SetRowClues("5; 4,10; 5; 15; 14;" + "3,9; 4,7; 3,3; 4; 4;" + "4,4; 12; 5,5; 8,4; 9,5;" +
                     "16; 9,6; 8,7; 8,7; 6,7;" + "6,9; 4,9; 2,10; 13; 4,5;" + "3,5; 2,1,1; 1,1,1; 6; 11");
                    g.SanityCheck();
                    return g;

                case 6:
                    g = new Griddler("Desert", 28, 22);
                    g.SetColumnClues("8; 8; 5,10; 7,3,6; 9,1,7;" + "9,11; 9,10; 9,10; 8,9; 7,9;" + "3,8; 8; 9; 8; 10;" +
                        "4,6; 1,7; 1,8; 5,10; 6,10;" + "1,10; 1,8; 22; 22; 1,9;" + "4,8; 4,9; 8");
                    g.SetRowClues("5,2; 7,2; 8,2; 9,2; 9,2,2;" + "9,2,2; 8,2,2; 7,2,2,2; 4,2,2,2; 5,2;" +
                        "5; 3,3,2; 2,3,2,3,2; 2,5,1,2,3,3,1; 3,11,11;" + "3,11,12; 28; 28; 28; 28;" + "28; 28");
                    g.SanityCheck();

                    return g;
            }

            throw new InvalidOperationException("No challenge at index " + index);
        }
    }
}
