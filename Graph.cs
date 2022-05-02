using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sise
{
    class Graph
    {
        private int cols;
        private int rows;
        private int size;
        private string toDirection;

        private int depth;
        private int[] puzzle;
        private List<Graph> children = new List<Graph>();
        private Graph parent;
        private int h;

        internal Graph Parent { get => parent; }
        internal List<Graph> Children { get => children; }
        public int[] Puzzle { get => puzzle; }

        public string todirection { get => toDirection; }
        public int Depth { get => depth; }

        public Graph(int[] fromFile)
        {

            this.cols = fromFile[1];
            this.rows = fromFile[0];
            this.size = cols * rows;
            this.depth = 0;
            puzzle = new int[this.size];

            for (int i = 2; i < puzzle.Length + 2; i++)
            {
                puzzle[i - 2] = fromFile[i];
            }
        }

        public Graph(int[] fromFile, int c, int r)
        {

            this.cols = c;
            this.rows = r;
            this.size = cols * rows;

            puzzle = new int[this.size];

            for (int i = 0; i < puzzle.Length; i++)
            {
                puzzle[i] = fromFile[i];
            }
        }

        public void Move(int[] fromFile, int a, int b, string direction)
        {

            int[] tab2 = new int[size];
            CopyBoard(tab2, fromFile);
            int tmp = tab2[a];
            tab2[a] = tab2[b];
            tab2[b] = tmp;

            Graph child = new Graph(tab2, cols, rows);
            child.toDirection = direction;
            child.parent = this;
            child.depth = this.depth + 1;
            children.Add(child);
        }
        public void CopyBoard(int[] a, int[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                a[i] = b[i];
            }
        }

        public void MoveToDirection(int[] fromFile, int i, string letter)
        {

            if (i - cols >= 0 && letter == "U")
            {
                Move(fromFile, i - cols, i, letter);
            }

            else if (i + cols < puzzle.Length && letter == "D")
            {
                Move(fromFile, i + cols, i, letter);
            }

            else if (i % cols > 0 && letter == "L")
            {
                Move(fromFile, i - 1, i, letter);
            }

            else if (i % cols < cols - 1 && letter == "R")
            {
                Move(fromFile, i + 1, i, letter);
            }
        }

        public void CreateChildren(char[] direction)
        {

            int empty = 0;
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (puzzle[i] == 0)
                {
                    empty = i;
                }
            }

            for (int i = 0; i < direction.Length; i++)
            {
                switch (direction[i])
                {

                    case 'L':
                        MoveToDirection(puzzle, empty, "L");
                        break;

                    case 'R':
                        MoveToDirection(puzzle, empty, "R");
                        break;

                    case 'U':
                        MoveToDirection(puzzle, empty, "U");
                        break;

                    case 'D':
                        MoveToDirection(puzzle, empty, "D");
                        break;

                    default:
                        Console.WriteLine("Wrong direction!");
                        break;
                }
            }
        }
        public void PrintPuzzle()
        {

            Console.WriteLine();
            int pom = 0;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(puzzle[pom] + " ");
                    pom++;
                }
                Console.WriteLine();
            }
        }

        public bool IsGoal()
        {

            bool flag = true;
            int tile = puzzle[0];
            for (int i = 1; i < puzzle.Length - 1; i++)
            {
                if (tile > puzzle[i])
                    flag = false;
                tile = puzzle[i];
            }
            return flag;
        }


        public bool IsRepeated(int[] tab)
        {
            bool flag = true;
            for (int i = 0; i < tab.Length; i++)
            {
                if (puzzle[i] != tab[i])
                    flag = false;
            }
            return flag;
        }

        public int HammingHeuristics()
        {
            this.h = 0;
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (puzzle[i] != 0 && puzzle[i] != i + 1)
                {
                    h++;
                }
            }
            h += this.depth;
            return h;
        }

        public int ManhattanHeuristics()
        {
            this.h = 0;
            for (int i = 0; i < puzzle.Length; i++)
            {
                if (puzzle[i] != 0 && puzzle[i] != i + 1)
                {
                    int correctX = (puzzle[i] - 1) % cols;
                    int correctY = (puzzle[i] - 1) / rows;

                    int incorrectX = i % cols;
                    int incorrectY = i / rows;

                    this.h += Math.Abs(correctX - incorrectX) + Math.Abs(correctY - incorrectY);
                }
            }
            h += this.depth;
            return h;
        }
    }


}

