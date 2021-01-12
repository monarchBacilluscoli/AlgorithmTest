using System;

namespace search
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int[,] map = new int[5, 4]{
                    {0,0,1,0},
                    {0,0,0,0},
                    {0,0,1,0},
                    {0,1,0,0},
                    {0,0,0,1}
                };
                Search.FindPath(map, new Point(0, 0), new Point(3, 2), Search.BreadthFirstSearch);
            }
        }
    }
}
