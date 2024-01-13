//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/01/13"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Tilemaps;



namespace UnderworldCafe.GridSystem
{
    /// <summary>
    /// Class for creating path from grid inside the gameobject this class is attached to
    /// </summary>
    public class GridManager : DestroyOnLoadSingletonMonoBehaviour<GridManager>
    {
        [SerializeField] private Tilemap _playerWalkableMap;
        [SerializeField] private Tilemap _interactableObjectMap;


        // Start is called before the first frame update
        private void Start()
        {
            //Compress/resize bound of the tilemaps gameobject by removing unused rows/columns from tilemaps
            _playerWalkableMap.CompressBounds();
            _interactableObjectMap.CompressBounds();

            //Turn of the renderer of the tilemaps
            _playerWalkableMap.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _interactableObjectMap.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            

            // bounds = tilemap.cellBounds;
            // camera = Camera.main;

            
            // CreateGrid();
            // astar = new Astar(spots, bounds.size.x, bounds.size.y);
        }

        private void CreateGrid()
        {
            // spots = new Vector3Int[bounds.size.x, bounds.size.y];
            // for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
            // {
            //     for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
            //     {
            //         if (tilemap.HasTile(new Vector3Int(x, y, 0)))
            //         {
            //             spots[i, j] = new Vector3Int(x, y, 0);
            //         }
            //         else
            //         {
            //             spots[i, j] = new Vector3Int(x, y, 1);
            //         }
            //     }
            // }
        }

        public Vector3Int[,] GetSpotFromTilemap(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;

            Vector3Int[,] spots = new Vector3Int[bounds.size.x, bounds.size.y];

            for (int x = bounds.xMin, i = 0; i < (bounds.size.x); x++, i++)
            {
                for (int y = bounds.yMin, j = 0; j < (bounds.size.y); y++, j++)
                {
                    if (tilemap.HasTile(new Vector3Int(x, y, 0)))
                    {
                        spots[i, j] = new Vector3Int(x, y, 0);
                    }
                    else
                    {
                        spots[i, j] = new Vector3Int(x, y, 1);
                    }
                }
            }

            return spots;
        }

    }
}
