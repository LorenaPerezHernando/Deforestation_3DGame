using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Deforestation.Tower
{


    public class TowerInteraction : MonoBehaviour
    {
        public Action OnRepairTower;
        
        [Header("Tower Repaired")]
        public bool isRepaired;
        [SerializeField] private GameObject _goodTower;
        [SerializeField] private GameObject _badTower;
        [SerializeField] private GameObject _panelMisionCompleted;

        [Header("Tower NOT Repaired")]
        [SerializeField] private GameObject _prefabRepairTowerDialogue;      
        [SerializeField] private GameObject _dialoguePanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Machine"))
            {

                if (_prefabRepairTowerDialogue != null)
                { 
                    _dialoguePanel.SetActive(true);
                    _prefabRepairTowerDialogue.SetActive(true);
                }

                if (isRepaired)
                {
                    StartCoroutine(FixTheTower());
                    
                }
                //DIALOGUE FOR PIECES
            }

        } 


        
        IEnumerator FixTheTower()
        {
            OnRepairTower?.Invoke(); //Audio, Dialogo
            //TODO Particulas
            yield return new WaitForSeconds(1);
            _goodTower.SetActive(true );
            _badTower.SetActive(false );
            yield return new WaitForSeconds(1);
            _dialoguePanel?.SetActive(false);
            _panelMisionCompleted.SetActive(true );   
            //Destroy(_badTower); Destroy(this);
        }
        
    }
}
