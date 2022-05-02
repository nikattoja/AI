using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Sise
{
    class Program
    {

        static Stopwatch stopwatch;
        static void Main(string[] args)
        {


            string Path = args[2];
            string lines = File.ReadAllText(Path);
            string[] stringList = lines.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
            int[] intList = stringList.Select(arg => int.Parse(arg)).ToArray();
            Graph graph = new Graph(intList);

            string algorithm = args[0];
            string option = args[1];
            algorithm = algorithm.ToUpper();
            option = option.ToUpper();

            List<Graph> solution = new List<Graph>();

            char[] order = option.ToCharArray();

            stopwatch = Stopwatch.StartNew();
            int visited = 0;
            int processed = 0;
            int deepest = 0;
            switch (algorithm)
            {
                case "BFS":
                    BFS bfs = new BFS(order);
                    solution = bfs.BfsAlgorithm(graph);
                    processed = bfs.Processed;
                    visited = bfs.Visited;
                    deepest = bfs.Deepest;
                    break;

                case "DFS":
                    DFS dfs = new DFS(order);
                    solution = dfs.DfsAlgorithm(graph);
                    processed = dfs.Processed;
                    visited = dfs.Visited;
                    deepest = dfs.Deepest;
                    break;
                case "ASTR":
                    AStar aStar = new AStar(option);
                    solution = aStar.AStarAlgorithm(graph);
                    processed = aStar.Processed;
                    visited = aStar.Visited;
                    deepest = aStar.Deepest;
                    break;
                default:
                    Console.WriteLine("Wrong arguments! ");
                    break;

            }
            stopwatch.Stop();

            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            if (solution.Count == 0)
            {
                list.Add("-1");
                list2.Add("-1");
            }
            else
            {

                list.Add((solution.Count - 1).ToString());
                list2.Add((solution.Count - 1).ToString());
                string tmp = "";
                for (int i = solution.Count - 2; i >= 0; i--)
                {
                    tmp += solution[i].todirection;
                }
                list.Add(tmp);
            }
            list2.Add(processed.ToString());
            list2.Add(visited.ToString());
            list2.Add(deepest.ToString());
            list2.Add(Math.Round(stopwatch.Elapsed.TotalMilliseconds, 3).ToString());
            File.WriteAllLines(args[3], list);
            File.WriteAllLines(args[4], list2);


        }
    }
}
