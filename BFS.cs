using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    internal class BFS
    {
        Functions functions;
        private char[] direction;
        private int visited;
        private int processed;
        private int deepest;

        public BFS(char[] direction)
        {
            this.direction = direction;
            this.functions = new Functions();
        }

        public int Visited { get => visited; }
        public int Processed { get => processed; }
        public int Deepest { get => deepest; }

        public List<Graph> BfsAlgorithm(Graph root)
        {

            Queue<Graph> search = new Queue<Graph>();
            Queue<Graph> searched = new Queue<Graph>();
            List<Graph> solution = new List<Graph>();

            bool isSolved = false;

            search.Enqueue(root);

            while (search.Count > 0 && !isSolved)
            {
                Graph currentGraph = search.ElementAt(0);
                searched.Enqueue(currentGraph);
                search.Dequeue();

                if (currentGraph.IsGoal())
                {
                    isSolved = true;
                    functions.GetParentList(solution, currentGraph);
                    break;
                }

                currentGraph.CreateChildren(direction);

                for (int i = 0; i < currentGraph.Children.Count; i++)
                {
                    if (!functions.IsInQueue(search, currentGraph.Children[i]) && !functions.IsInQueue(searched, currentGraph.Children[i]))
                    {
                        search.Enqueue(currentGraph.Children[i]);
                    }
                }

            }
            deepest = solution[0].Depth;
            visited = searched.Count + search.Count;
            processed = searched.Count;

            return solution;
        }
    
}
}
