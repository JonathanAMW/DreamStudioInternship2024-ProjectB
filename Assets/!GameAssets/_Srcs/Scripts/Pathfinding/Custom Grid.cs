//----------------------------------------------------------------------
// Author   : "InsertYourNameHere"
// Created  : "YYYY/MM/DD"
//----------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Tilemaps;


namespace UnderworldCafe
{
    /// <summary>
    /// Class summary
    /// </summary>
    public class CustomGrid : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Tilemap _playerWalkable;
        [SerializeField] private Tilemap _npcWalkable;
        [SerializeField] private Tilemap _obstacle;

    }
}
