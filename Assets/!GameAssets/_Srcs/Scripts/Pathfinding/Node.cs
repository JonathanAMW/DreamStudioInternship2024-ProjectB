//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/15"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnderworldCafe.PathfindingSystem
{
    /// <summary>
    /// This class provides node creation for pathfinding
    /// </summary>
    public class Node
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public int Height = 0;
        public List<Node> Neighboors;
        public Node previous = null;

        public Node(int x, int y, int height)
        {
            X = x;
            Y = y;
            F = 0;
            G = 0;
            H = 0;
            Neighboors = new List<Node>();
            Height = height;
        }

        public void AddNeighboors(Node[,] grid, int x, int y)
        {
            if (x < grid.GetUpperBound(0))
                Neighboors.Add(grid[x + 1, y]);
            if (x > 0)
                Neighboors.Add(grid[x - 1, y]);
            if (y < grid.GetUpperBound(1))
                Neighboors.Add(grid[x, y + 1]);
            if (y > 0)
                Neighboors.Add(grid[x, y - 1]);
            #region diagonal
            //if (X > 0 && Y > 0)
            //    Neighboors.Add(grid[X - 1, Y - 1]);
            //if (X < Utils.Columns - 1 && Y > 0)
            //    Neighboors.Add(grid[X + 1, Y - 1]);
            //if (X > 0 && Y < Utils.Rows - 1)
            //    Neighboors.Add(grid[X - 1, Y + 1]);
            //if (X < Utils.Columns - 1 && Y < Utils.Rows - 1)
            //    Neighboors.Add(grid[X + 1, Y + 1]);
            #endregion
        }
    }
}
