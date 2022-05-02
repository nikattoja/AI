using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    internal class AStar
    {
        private int visited;
        private int processed;
        private string heuristics;
        private int deepest;
        Functions functions;
       
        private char[] direction;

        public AStar(string heuristics)
        {
            this.functions = new Functions();
            this.heuristics = heuristics;
            string directiontmp = "LUDR";
            direction = directiontmp.ToCharArray();
    }

        public int Visited { get => visited; }
        public int Processed { get => processed; }
        public int Deepest { get => deepest; }

        public List<Graph> AStarAlgorithm(Graph root) {

            PriorityQueue<Graph> search = new PriorityQueue<Graph>();
            List<Graph> searched = new List<Graph>();
            List<Graph> solution = new List<Graph>();

            int searched_count = 0;
            bool isSolved = false;
            search.Add(-1, root);

            while (search.priorityQueue.Count > 0 && !isSolved) {
                Graph current = search.GetFirst();
                if (current.Depth > deepest) { 
                    deepest = current.Depth;
                }
                while (functions.IsInList(searched, current)) { 
                    current = search.GetFirst();
                }
                if (current.IsGoal()) {
                    isSolved = true;
                    functions.GetParentList(solution, current);
                    break;
                }
                current.CreateChildren(direction);

                for (int i = 0; i < current.Children.Count; i++) {
                    int counted;
                    if (heuristics == "HAMM")
                    {
                        counted = current.Children[i].HammingHeuristics();
                    }
                    else {
                        counted = current.Children[i].ManhattanHeuristics();
                    }
                    search.Add(counted, current.Children[i]);
                    searched_count += 1;
                }
                searched.Add(current);
            }
            visited = searched_count + 1;
            processed = searched.Count;

            return solution;
        }

    }
}
