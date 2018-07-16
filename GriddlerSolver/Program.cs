using System;

namespace GriddlerSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var apricot = new Griddler("1,1,7; 2,3,2; 2,2,3,6; 1,1,4,2,3; 1,4,2,2,3;" + "3,5,1,2; 2,1,2,3; 4,1,4,2,1,1; 2,4,2,1,4; 3,2,4,2;" +
                "2,1,1,4,2; 2,1,1,1,1,5; 3,1,2,4; 3,2,2,2; 2,7", // 157
                "3,6; 14; 1,5,2; 4,1,1; 2,1,2;" + "1,2,4; 2,2,3,2; 1,2,1,1,2; 1,2,1,1,1,1; 3,1,1,1,1;" +
                "2,2,1,1,3; 1,3,2,1,2; 1,2,2,3,1,1; 1,2,6; 1,1,7;" + "1,1,4; 2,1,1,1; 3,3,2; 4,3; 6"); // 157

            // no solution
            var flower = new Griddler("2; 1,1; 2,1,1; 1,1,2; 2,1,2;" + "1,2,4; 1,2,4; 2,1; 1,1; 2",
                "2; 1,1; 2,2; 1,2,1; 1,2,1;" + "2,2; 3,2; 1,1,2; 6; 3");

            var solver = new Solver(flower);

            foreach (var partial in solver.Solve())
            {
                Console.WriteLine(partial.ToString());
                System.Threading.Thread.Sleep(500);
            }

            Console.Write("Press enter > ");
            Console.ReadLine();
        }
    }
}
