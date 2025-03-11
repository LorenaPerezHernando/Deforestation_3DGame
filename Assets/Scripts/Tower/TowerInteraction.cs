using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Tower
{


    public class TowerControl : MonoBehaviour
    {
        [SerializeField] private bool _isBroken;

        [SerializeField] private GameObject _repairTowerDialogue;
        [SerializeField] private GameObject _dialoguepieces;
        void Start()
        {
            _isBroken = true;
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

            if (_repairTowerDialogue == null)
            {
                _dialoguepieces.SetActive(true);
            }
            //DIALOGUE FOR PIECES


        }

        private void OnTriggerExit(Collider other)
        {
            _dialoguepieces.SetActive(false);
        }
    }
}
