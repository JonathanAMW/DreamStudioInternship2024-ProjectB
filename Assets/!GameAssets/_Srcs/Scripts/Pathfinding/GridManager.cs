//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/13"
//----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



namespace UnderworldCafe.GridSystem
{
    /// <summary>
    /// Class for managing tilemap specifiq operations
    /// </summary>
    public class GridManager : DestroyOnLoadSingletonMonoBehaviour<GridManager>
    {
        [Header("The Game Tilemap ('Every tiles in this tilemap will be rendered by game')")]
        [SerializeField] private Tilemap _gameTilemap;

        [Header("The Pathing Tilemaps ('These tilemaps will not be rendered by game and will not get calculated if outside of Game Tilemap bound')")]
        [SerializeField] private Tilemap _playerWalkableMap;

        private void OnValidate()
        {
            if(_gameTilemap == null)
            {
                Debug.LogError("No Game Tilemap has been set in: " + gameObject.name);
            }
            if(_playerWalkableMap == null)
            {
                Debug.LogError("No Player Walkable Map has been set in: " + gameObject.name);
            }
        }

        protected override void Awake()
        {
            base.Awake();

            //Compress/resize bound of the tilemaps gameobject by removing unused rows/columns from tilemaps
            _gameTilemap.CompressBounds();
            _playerWalkableMap.CompressBounds();

            //Turn of the renderer of the tilemaps
            // _playerWalkableMap.gameObject.GetComponent<TilemapRenderer>().enabled = false;
        }


        public Vector3Int[,] ConvertGridToNodes(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;

            Vector3Int[,] nodes = new Vector3Int[bounds.size.x, bounds.size.y];

            for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
            {
                for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
                {
                    if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                    {
                        nodes[i, j] = new Vector3Int(x, y, 0);
                    }
                    else
                    {
                        nodes[i, j] = new Vector3Int(x, y, 1);
                    }
                }
            }

            return nodes;
        }


        // public List<Vector3> GetTileCenterPos(List<Vector3Int> pathNodes, Tilemap tilemap)
        // {
        //     var temp = new List<Vector3>();
        //     foreach(Vector3 p in pathNodes)
        //     {
        //         Vector3Int tilePosition = tilemap.WorldToCell(p);
        //         Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition);
        //         temp.Add(tileCenter);
        //     }

        //     return temp;
        // }
        
        public BoundsInt GetTilemapBounds(Tilemap tilemap)
        {
            return tilemap.cellBounds;
        }

        public Vector3 GetTileCenterFromObjPosition(Tilemap tilemap, Vector3 objPosition)
        {
            Vector3Int tilePosition = tilemap.WorldToCell(objPosition);
            Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition);

            return tileCenter;
        } 

    }
}
