//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/16"
//----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

using UnderworldCafe.PathfindingSystem;
using UnderworldCafe.GridSystem;
using UnderworldCafe.InputSystem;

namespace UnderworldCafe.Player
{
    /// <summary>
    /// Class that responsible to manage players character
    /// </summary>
    public class PlayerController : DestroyOnLoadSingletonMonoBehaviour<PlayerController>
    {
        private InputManager _inputManagerRef => InputManager.Instance;
        private PathRequestManager _pathRequestManagerRef => PathRequestManager.Instance;
        private GridManager _gridManagerRef => GridManager.Instance;

        [SerializeField] private Tilemap _playerWalkableTilemap;
        [SerializeField] private float _movementSpeed = 10f;

        private List<Vector3> _pathPos; // Array to hold the calculated path
        private int _targetIndex; // Index of the current target waypoint in the path
        private bool _isMoving;

        private void Start()
        {
            _pathPos = new List<Vector3>();
            _playerMovementRequestList = new List<PlayerMovementRequest>();

            transform.position = _gridManagerRef.GetTileCenterFromObjPosition(_playerWalkableTilemap, transform.position);
        }


        #region PlayerMovement Methods
        private void MovePlayerTo(Vector2 targetPos)
        {            
            var pathEnd = (Vector3)targetPos;

            _pathRequestManagerRef.RequestPath(_playerWalkableTilemap, transform.position, pathEnd, OnPathFound);
        }

        // Callback method called when a path is found
        public void OnPathFound(List<Node> newPath, bool pathSuccessful) 
        {
            if (pathSuccessful) 
            {
                // Convert list of Nodes to Vector3Int and reversing it
                var processedPath = new List<Vector3>();
                for(int i = newPath.Count - 1; i >= 0; i--) 
                {
                    var temp = new Vector3(newPath[i].X, newPath[i].Y, newPath[i].Height);
                    processedPath.Add(temp);
                }

                // Get each Vector3 pos of tile center
                _pathPos.Clear();
                foreach(Vector3 p in processedPath)
                {
                    _pathPos.Add(_gridManagerRef.GetTileCenterFromObjPosition(_playerWalkableTilemap, p));
                }
                
                _targetIndex = 0;

                StopCoroutine("FollowPath"); 
                StartCoroutine("FollowPath"); 
                
            }
        }

        // Coroutine to move the unit along the calculated path
        private IEnumerator FollowPath() 
        { 
            Vector3 currentWaypoint = _pathPos[0]; // Get the first waypoint from the path
            while (true) 
            {
                if (_gridManagerRef.GetTileCenterFromObjPosition(_playerWalkableTilemap, transform.position) == currentWaypoint) 
                { 
                    _targetIndex++;

                    if (_targetIndex >= _pathPos.Count) 
                    { 
                        transform.position = _gridManagerRef.GetTileCenterFromObjPosition(_playerWalkableTilemap, transform.position);
                        _isMoving = false;
                        FinishedFollowingPath(true);
                        yield break;
                    }
                    currentWaypoint = _pathPos[_targetIndex];
                }

                
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, _movementSpeed * Time.deltaTime);
                
                // yield return new WaitForSeconds(3); 
                yield return null; 
            }
        }
        #endregion


        #region PlayerCommand Methods
        // Need to create a somewhat cmmand pattern like queue for removeable element inside queue
        // and then to have the index of element inside the queue to be reflected in element properties
        struct PlayerMovementRequest
        {
            public Vector2 TargetPos;
            public Action<bool> OnMoveDoneCallback;
            public Action<bool> OnMoveRequestAcceptedCallback;
            public Action<int> OnUpdateMoveIndexInQueueCallback;
            public IInteractable InteractableRef;

            public PlayerMovementRequest(Vector2 TargetPos, Action<bool> OnMoveDoneCallback, Action<bool> OnMoveRequestAcceptedCallback, Action<int> OnUpdateMoveIndexInQueueCallback, IInteractable InteractableRef)
            {
                this.TargetPos = TargetPos;
                this.OnMoveDoneCallback = OnMoveDoneCallback;
                this.OnMoveRequestAcceptedCallback = OnMoveRequestAcceptedCallback;
                this.OnUpdateMoveIndexInQueueCallback = OnUpdateMoveIndexInQueueCallback;
                this.InteractableRef = InteractableRef;
            }
        }

        private List<PlayerMovementRequest> _playerMovementRequestList;
        [SerializeField] private int _maxQueue = 6;
        private PlayerMovementRequest _currentPlayerMovementRequest;

        public void CreatePlayerMovementRequest(Vector2 targetPos, Action<bool> onMoveDoneCallback, Action<bool> onMoveRequestAcceptedCallback, Action<int> onUpdateMoveIndexInQueueCallback, IInteractable InteractableRef)
        {
            if(_playerMovementRequestList.Count < _maxQueue)
            {
                _playerMovementRequestList.Add(new PlayerMovementRequest(targetPos, onMoveDoneCallback, onMoveRequestAcceptedCallback, onUpdateMoveIndexInQueueCallback, InteractableRef));
                onMoveRequestAcceptedCallback.Invoke(true);
                onUpdateMoveIndexInQueueCallback(_playerMovementRequestList.Count);
                TryProcessNext(); // Attempt to process the next path request
            }
        }

        public void RemovePlayerMovementRequest(IInteractable sender)
        {
            bool hasRemoveable = false;
            
            for(int i = 0; i < _playerMovementRequestList.Count; i++)
            {
                if(ReferenceEquals(_playerMovementRequestList[i].InteractableRef, sender))
                {
                    hasRemoveable = true;

                    //if current movement is what being removed
                    if(i==0)
                    {
                        StopCoroutine("FollowPath");
                        _isMoving = false;
                    }
                    
                    _playerMovementRequestList[i].OnMoveRequestAcceptedCallback(false);
                    _playerMovementRequestList.Remove(_playerMovementRequestList[i]);

                    UpdateObjectIndexInList();
                    
                    break;
                }
            }

            if(!hasRemoveable) return;

            TryProcessNext(); 
        }

        //Update queue index in every object inside playerMovementQueue
        private void UpdateObjectIndexInList()
        {
            for(int i = 0; i < _playerMovementRequestList.Count; i++)
            {
                _playerMovementRequestList[i].OnUpdateMoveIndexInQueueCallback.Invoke(i+1);
            }
        }


        private void TryProcessNext()
        {
            if(!_isMoving && _playerMovementRequestList.Count > 0)
            {
                _currentPlayerMovementRequest = _playerMovementRequestList[0];
                _isMoving = true;
                MovePlayerTo(_currentPlayerMovementRequest.TargetPos);
            }
        }

        // Method called when a path has finished processing
        private void FinishedFollowingPath(bool done) 
        {
            _currentPlayerMovementRequest.OnMoveDoneCallback(done); // Invoke the callback with the path result
            RemovePlayerMovementRequest(_playerMovementRequestList[0].InteractableRef);
            _isMoving = false; 
            TryProcessNext(); 
        }
        #endregion
    }
}
