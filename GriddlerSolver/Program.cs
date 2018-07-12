using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GriddlerSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Griddler("1; 3; 3; 10; 1,1; 1,1; 1,1,1; 1,1,3; 1,1,3; 10",
                    "7; 1,1; 7; 1,1; 1,1; 1,1; 1,1; 3,3; 4,4; 3,3");

            var solver = new Solver(game);

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
