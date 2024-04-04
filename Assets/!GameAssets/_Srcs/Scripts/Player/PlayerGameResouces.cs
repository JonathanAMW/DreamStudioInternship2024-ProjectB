//----------------------------------------------------------------------
// Author   : "Ananta Miyoru Wijaya"
// Created  : "2024/02/02"
//----------------------------------------------------------------------

using UnityEngine;


namespace UnderworldCafe
{
    /// <summary>
    /// Class is responsible for managing player resources
    /// </summary>
    public class PlayerGameResouces : MonoBehaviour
    {
        private int _money=100;
        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }


        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

       
    }
}
