using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    internal class DFS
    {
        Functions functions;
        private char[] direction;
        private int visited;
        private int processed;
        private int deepest;
        private Graph solved;
        private int maxDepth = 21;
        bool isSolved = false;
        private Graph v;

        public DFS(char[] direction)
        {
            this.direction = direction;
            this.functions = new Functions();
        }

        public int Visited { get => visited; }
        public int Processed { get => processed; }
        public int Deepest { get => deepest; }

        public List<Graph> DfsAlgorithm(Graph root)
        {

            Stack<Graph> search = new Stack<Graph>();
            Dictionary<Graph, Graph> searched = new Dictionary<Graph, Graph>();
            List<Graph> solution = new List<Graph>();

            bool isSolved = false;
            search.Push(root);

            while (search.Count > 0 && !isSolved)
            {
                v = search.Pop();
                if (v.Depth > deepest)
                {
                    deepest = v.Depth;
                }
                if (v.IsGoal())
                {
                    isSolved = true;
                    functions.GetParentList(solution, v);
                    visited = searched.Count + search.Count;
                    processed = searched.Count;
                    return solution;
                }
                if (v.Depth > maxDepth)
                {
                    continue;
                }
                if (searched.ContainsKey(v))
                {
                    if (v.Depth >= searched[v].Depth)
                    {
                        continue;
                    }
                    else
                    {
                        searched.Remove(v);
                    }
                }
                searched.Add(v, v);
                v.CreateChildren(direction);
                List<Graph> nextStates = v.Children;
                nextStates.Reverse();
                foreach (Graph nextState in nextStates)
                {
                    search.Push(nextState);
                }
            }
            visited = searched.Count + search.Count;
            processed = searched.Count;

            return solution;
        }

    }
}
