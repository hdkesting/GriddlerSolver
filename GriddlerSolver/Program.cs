using System;

namespace GriddlerSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var challenge = GetChallenge(3);

            Console.WriteLine($"{challenge.Name}: Size to solve: {challenge.Height} rows and {challenge.Width} columns.");
            System.Threading.Thread.Sleep(1000);

            var solver = new Solver(challenge);

            foreach (var partial in solver.Solve())
            {
                Console.WriteLine(partial.ToString());
                System.Threading.Thread.Sleep(500);
            }

            Console.Write("Press enter > ");
            Console.ReadLine();
        }

        private static Griddler GetChallenge(int index)
        {
            switch (index)
            {
                case 0:
                    return new Griddler("Apricot", "1,1,7; 2,3,2; 2,2,3,6; 1,1,4,2,3; 1,4,2,2,3;" + "3,5,1,2; 2,1,2,3; 4,1,4,2,1,1; 2,4,2,1,4; 3,2,4,2;" +
                        "2,1,1,4,2; 2,1,1,1,1,5; 3,1,2,4; 3,2,2,2; 2,7",
                        "3,6; 14; 1,5,2; 4,1,1; 2,1,2;" + "1,2,4; 2,2,3,2; 1,2,1,1,2; 1,2,1,1,1,1; 3,1,1,1,1;" +
                        "2,2,1,1,3; 1,3,2,1,2; 1,2,2,3,1,1; 1,2,6; 1,1,7;" + "1,1,4; 2,1,1,1; 3,3,2; 4,3; 6");

                case 1:
                    // no solution (without guessing)
                    return new Griddler("Flower", "2; 1,1; 2,1,1; 1,1,2; 2,1,2;" + "1,2,4; 1,2,4; 2,1; 1,1; 2",
                         "2; 1,1; 2,2; 1,2,1; 1,2,1;" + "2,2; 3,2; 1,1,2; 6; 3");

                case 2:
                    // no solution
                    return new Griddler("Red Squirrel", "2,3,2,3; 1,2,1,1,2,2; 3,1,1,2,6; 1,1,1,2,6; 3,1,1,2,1,1;" + "1,1,1,1,3; 1,2,2,2,1; 1,2,3; 2,1,1,1,5,2; 3,3,2,2;" +
                        "3,3,4; 11,4; 4,3,6,1; 7,1; 7,7,1;" + "9,8; 1,9,7; 1,13,2; 1,4,8; 3,6",
                        "1,1,1,3; 1,1,1,2,1,1; 1,1,2,2,2,1,1; 1,2,4,3,1; 1,4,5;" + "2,2,1,3,5; 3,1,1,2,5; 1,2,3,4; 2,5,5; 2,1,3,6;" +
                        "1,1,2,2,5; 7,1,4,4; 3,1,4,3; 1,1,3,9; 4,14;" + "2,2,9; 1,2,2,5; 1,2,1,3,4; 4,2,2,3; 5,6");

                case 3:
                    return new Griddler("Chicken", "3,1; 5,1,1,5; 4,2,1,1; 2,1,1,2; 3,3,1;" + "4,1,2,1; 3,1; 3,1,2,1; 4,2,4; 4,2,1;" +
                        "2,2,5; 2,3,1,1; 4,1,2; 1,2; 4",
                        "2,2,1,2; 5,5; 6,4,1; 3,5,2; 2,1,3,1,2;" + "1,1,3,1; 3,1,2,1; 1,2,2; 1,1,2,2; 1,1,2,1;" +
                        "1,3,2; 1,2,4; 3,1,1,1; 1,1,1; 1,2,2");
            }

            throw new InvalidOperationException("No challenge at index " + index);
        }
    }
}
