using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    internal class PriorityQueue<T>
    {
        public SortedDictionary<int, Queue<Graph>> priorityQueue = new SortedDictionary<int, Queue<Graph>>();

        public void Add(int key, Graph g) {
            if (!priorityQueue.ContainsKey(key)) {
                priorityQueue.Add(key, new Queue<Graph>());
            }
            priorityQueue[key].Enqueue(g);
        }

        public Graph GetFirst() { 
            int min_key = priorityQueue.Keys.Min();
            Graph g = priorityQueue[min_key].Dequeue();
            if (priorityQueue[min_key].Count == 0) { 
                priorityQueue.Remove(min_key);
            }
            return g;
        }
    }
}
