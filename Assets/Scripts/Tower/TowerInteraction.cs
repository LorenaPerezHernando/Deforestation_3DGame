using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Tower
{


    public class TowerInteraction : MonoBehaviour
    {

        [SerializeField] private GameObject _prefabRepairTowerDialogue;
        private bool ifInstantiate;
        [SerializeField] private GameObject _dialoguePanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {

                if (_prefabRepairTowerDialogue != null)
                {

                    //if(ifInstantiate == false)
                    //{
                    //    //GameObject Towerdialogue = Instantiate(_prefabRepairTowerDialogue, _dialoguePanel.transform);
                    //    ifInstantiate = true;
                    //} 

                    _dialoguePanel.SetActive(true);
                    _prefabRepairTowerDialogue.SetActive(true);
                    //_prefabRepairTowerDialogue.SetActive(true);
                }            
                //DIALOGUE FOR PIECES
            }

        }        
    }
}
