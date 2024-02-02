//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/17"
//----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

using UnderworldCafe.GridSystem;
using UnityEngine.Tilemaps;
using System.Diagnostics;


namespace UnderworldCafe.PathfindingSystem
{
    /// <summary>
    /// Class summary
    /// </summary>
    public class PathRequestManager : DestroyOnLoadSingletonMonoBehaviour<PathRequestManager>
    {
        // // Structure to hold a path request
        struct PathRequest
        {
            public Tilemap pathTilemap;
            public Vector3 pathStart;
            public Vector3 pathEnd;
            public Action<List<Node>, bool> pathCallback;

            public PathRequest(Tilemap tilemap, Vector3 start, Vector3 end, Action<List<Node>, bool> callback)
            {
                pathTilemap = tilemap;
                pathStart = start;
                pathEnd = end;
                pathCallback = callback;
            }
        }

        private GridManager _gridManagerRef => GridManager.Instance;
        private AstarPathfinding _astarPathfindingRef;

        private PathRequest currentPathRequest;
        private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        private bool _isProcessingPath;


        private void Start()
        {
            _astarPathfindingRef = GetComponent<AstarPathfinding>();
        }


        
        public void RequestPath(Tilemap pathTilemap, Vector3 pathStart, Vector3 pathEnd, Action<List<Node>, bool> callback)
        {
            PathRequest newRequest = CreatePathRequest(pathTilemap, pathStart, pathEnd, callback);
            pathRequestQueue.Enqueue(newRequest);
            TryProcessNext(); // Attempt to process the next path request
        }



        private PathRequest CreatePathRequest(Tilemap pathTilemap, Vector3 pathStart, Vector3 pathEnd, Action<List<Node>, bool> callback)
        {
            var convertedPathStart = _gridManagerRef.GetTileCenterFromObjPosition(pathTilemap, pathStart);
            var convertedPathEnd = _gridManagerRef.GetTileCenterFromObjPosition(pathTilemap, pathEnd);

            return new PathRequest(pathTilemap, convertedPathStart, convertedPathEnd, callback);
        }


        // Attempt to process the next path request in the queue
        void TryProcessNext()
        {
            if (!_isProcessingPath && pathRequestQueue.Count > 0)
            { 
                currentPathRequest = pathRequestQueue.Dequeue(); // Dequeue the next path request
                _isProcessingPath = true; // Set flag to indicate processing
                _astarPathfindingRef.StartFindPath(_gridManagerRef.ConvertGridToNodes(currentPathRequest.pathTilemap), 
                                                    _gridManagerRef.GetTilemapBounds(currentPathRequest.pathTilemap), 
                                                    currentPathRequest.pathStart, 
                                                    currentPathRequest.pathEnd); 
            }
        }

        // Method called when a path has finished processing
        public void FinishedProcessingPath(List<Node> path, bool success) 
        {
            currentPathRequest.pathCallback(path, success); // Invoke the callback with the path result
            _isProcessingPath = false; // Reset processing flag
            TryProcessNext(); // Attempt to process the next path request
        }        
    }
}
