using System;

namespace search
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int[,] map = new int[,]{
                    {1,0,0,0,0},
                    {1,1,0,0,0},
                    {0,1,1,0,0},
                    {1,0,0,0,1}
                };
                System.Console.WriteLine(Search.CalculateArea(map, new Point(1, 0), Search.BFSCalculateArea));
                return;
            }
            {
                int[,] map = new int[5, 4]{
                    {0,0,1,0},
                    {0,0,0,0},
                    {0,0,1,0},
                    {0,1,0,0},
                    {0,0,0,1}
                };
                Search.FindPath(map, new Point(0, 0), new Point(3, 2), Search.BreadthFirstSearchFindPath);
                return;
            }
        }
    }
}
