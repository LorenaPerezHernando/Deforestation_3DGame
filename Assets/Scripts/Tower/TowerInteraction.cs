using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Tower
{


    public class TowerControl : MonoBehaviour
    {

        [SerializeField] private GameObject _repairTowerDialogue;
        void Start()
        {
            _repairTowerDialogue.SetActive(false);
        }


        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (_repairTowerDialogue != null)
            {
                _repairTowerDialogue.SetActive(true);
            }

            
            //DIALOGUE FOR PIECES


        }

        
    }
}
