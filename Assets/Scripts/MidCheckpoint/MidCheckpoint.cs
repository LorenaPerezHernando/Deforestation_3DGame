using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Checkpoints
{


    public class MidCheckpoint : MonoBehaviour
    {

        public Action OnCheckpoint;
        [SerializeField] GameObject _dialogueCheckpoint;

        private void Awake()
        {

        }
        void Start()
        {

            Check();
            //_dialogueCheckpoint?.SetActive(false);
        }

        

        private void OnTriggerEnter(Collider other)
        {
            Check();
        }

        internal void Check()
        {

            OnCheckpoint?.Invoke(); //Guarda la pos del Player y Maquina en el GameController
            //_dialogueCheckpoint?.SetActive(true);
            StartCoroutine(CheckFalse());
            Debug.Log("Partida guardada");
            return;
        }


        IEnumerator CheckFalse()
        {
            yield return new WaitForSeconds(1);
            _dialogueCheckpoint?.SetActive(false);
        }
    }
    
}
