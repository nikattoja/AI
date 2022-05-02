using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    internal class Functions
    {

        public Functions() { }

        public void GetParentList(List<Graph> graphs, Graph g)
        {
            Graph current = g;
            graphs.Add(current);
            while (current.Parent != null)
            {
                current = current.Parent;
                graphs.Add(current);
            }
        }

        public bool IsInQueue(Queue<Graph> queue, Graph g)
        {
            for (int i = 0; i < queue.Count(); i++)
            {
                if (queue.ElementAt(i).IsRepeated(g.Puzzle))
                    return true;
            }
            return false;
        }

        public bool IsInList(List<Graph> list, Graph g)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].IsRepeated(g.Puzzle))
                    return true;
            }
            return false;
        }
    }
}
