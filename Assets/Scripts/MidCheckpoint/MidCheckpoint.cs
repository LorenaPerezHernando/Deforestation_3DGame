using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Checkpoints
{


    public class MidCheckpoint : MonoBehaviour
    {

        public Action<Vector3, Vector3> OnCheckpoint;
        [SerializeField] GameObject _dialogueCheckpoint;
        [SerializeField] private Transform _playerPos;
        [SerializeField] private Transform _maquinePos;
        void Start()
        {
            Check();
            _dialogueCheckpoint?.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Check();
        }

        internal void Check()
        {
            OnCheckpoint?.Invoke(_playerPos.position, _maquinePos.position);
            _dialogueCheckpoint?.SetActive(true);
            StartCoroutine(CheckFalse());
        }

        IEnumerator CheckFalse()
        {
            yield return new WaitForSeconds(1);
            _dialogueCheckpoint?.SetActive(false);
        }
    }
    
}
