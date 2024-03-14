//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/13"
//----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnderworldCafe.PathfindingSystem
{
    /// <summary>
    /// This class provides Astar Algorithms for Pathfinding
    /// </summary>
    public class AstarPathfinding : MonoBehaviour
    {
        PathRequestManager _requestManagerRef => PathRequestManager.Instance;
        private bool IsValidPath(Node start, Node end)
        {
            if(end==null)
            {
                Debug.LogWarning("end is null");
            }
            if(start == null)
            {
                Debug.LogWarning("start is null");
            }
            if(end.Height >= 1)
            {
                Debug.LogWarning("end height is more than 1");
            }

            if (end == null || start == null || end.Height >= 1)
            {
                Debug.LogWarning("Path is not valid");
                return false;
            }
                
            return true;
        }


        public void StartFindPath(Vector3Int[,] grid, BoundsInt tilemapBound, Vector3 start, Vector3 end) 
        {
            Vector3Int startInt = new Vector3Int(Mathf.FloorToInt(start.x), Mathf.FloorToInt(start.y), 0);
            Vector3Int endInt = new Vector3Int(Mathf.FloorToInt(end.x), Mathf.FloorToInt(end.y), 0);
               
            StartCoroutine(FindPath(grid, tilemapBound, startInt, endInt)); 
        }


        private IEnumerator FindPath(Vector3Int[,] grid, BoundsInt tilemapBound, Vector3Int start, Vector3Int end)
        {
            //if (!IsValidPath(grid, start, end))
            //     return null;

            Node End = null;
            Node Start = null;

            Node[,] Nodes = new Node[tilemapBound.size.x, tilemapBound.size.y];

            var columns = Nodes.GetUpperBound(0) + 1;
            var rows = Nodes.GetUpperBound(1) + 1;

            Nodes = new Node[columns, rows];

            //probably should only be called once as path/grid is not changing or dynamic
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Nodes[i, j] = new Node(grid[i, j].x, grid[i, j].y, grid[i, j].z);
                }
            }


            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Nodes[i, j].AddNeighboors(Nodes, i, j);
                    if (Nodes[i, j].X == start.x && Nodes[i, j].Y == start.y)
                    {
                        // Debug.Log("Found start");
                        Start = Nodes[i, j];
                    }
                    // else if (Nodes[i, j].X == end.x && Nodes[i, j].Y == end.y)
                    // {
                    //     Debug.Log("Found End");
                    //     End = Nodes[i, j];
                    // }
                    if (Nodes[i, j].X == end.x && Nodes[i, j].Y == end.y)
                    {
                        // Debug.Log("Found End");
                        End = Nodes[i, j];
                    }
                }
            }


            if (!IsValidPath(Start, End))
            {
                _requestManagerRef.FinishedProcessingPath(null, false);
                yield break;
            }


            List<Node> OpenSet = new List<Node>();
            List<Node> ClosedSet = new List<Node>();


            OpenSet.Add(Start);

            while (OpenSet.Count > 0)
            {
                //Find shortest step distance in the direction of your goal within the open set
                int winner = 0;
                for (int i = 0; i < OpenSet.Count; i++)
                {
                    if (OpenSet[i].F < OpenSet[winner].F)
                    {
                        winner = i;
                    }
                    else if (OpenSet[i].F == OpenSet[winner].F) //tie breaking for faster routing
                    {
                        if (OpenSet[i].H < OpenSet[winner].H)
                        {
                            winner = i;
                        }
                    }
                }

                var current = OpenSet[winner];

                //Found the path, creates, retraces, and returns the path
                if (End != null && OpenSet[winner] == End)
                {
                    List<Node> Path = new List<Node>();
                    var temp = current;
                    Path.Add(temp);
                    
                    while (temp.previous != null)
                    {
                        Path.Add(temp.previous);
                        temp = temp.previous;
                    }
                    
                    _requestManagerRef.FinishedProcessingPath(Path, true);
                    yield break;
                }

                OpenSet.Remove(current);
                ClosedSet.Add(current);

                //Finds the next closest step on the grid
                var neighboors = current.Neighboors;
                for (int i = 0; i < neighboors.Count; i++) //look threw our current spots neighboors (current spot is the shortest F distance in openSet
                {
                    var n = neighboors[i];
                    if (!ClosedSet.Contains(n) && n.Height < 1) //Checks to make sure the neighboor of our current tile is not within closed set, and has a height of less than 1
                    {
                        var tempG = current.G + 1; //gets a temp comparison integer for seeing if a route is shorter than our current path

                        bool newPath = false;
                        if (OpenSet.Contains(n)) //Checks if the neighboor we are checking is within the openset
                        {
                            if (tempG < n.G) //The distance to the end goal from this neighboor is shorter so we need a new path
                            {
                                n.G = tempG;
                                newPath = true;
                            }
                        }
                        else //if its not in openSet or closed set, then it IS a new path and we should add it too openset
                        {
                            n.G = tempG;
                            newPath = true;
                            OpenSet.Add(n);
                        }
                        if (newPath) //if it is a newPath caclulate the H and F and set current to the neighboors previous
                        {
                            n.H = Heuristic(n, End);
                            n.F = n.G + n.H;
                            n.previous = current;
                        }
                    }
                }
            }
        }

        private int Heuristic(Node a, Node b)
        {
            //manhattan
            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            return 1 * (dx + dy);

            #region diagonal
            //diagonal
            // Chebyshev distance
            //var D = 1;
            // var D2 = 1;
            //octile distance
            //var D = 1;
            //var D2 = 1;
            //var dx = Math.Abs(a.X - b.X);
            //var dy = Math.Abs(a.Y - b.Y);
            //var result = (int)(1 * (dx + dy) + (D2 - 2 * D));
            //return result;// *= (1 + (1 / 1000));
            //return (int)Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
            #endregion
        } 
    }
}