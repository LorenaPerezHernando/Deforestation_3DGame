using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.UI
{


    public class RespawnPanel : MonoBehaviour
    {
        public Action OnRevive;
        private GameObject _deathPanel;

        private void Awake()
        {
            _deathPanel = this.gameObject;
            _deathPanel.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                Revive();
            }
        }
        private void Revive()
        {
            OnRevive?.Invoke();
            _deathPanel.SetActive(false);
            Debug.Log("Revive");
        }
        internal void Died()
        {
            _deathPanel.SetActive(true);
        }
    }
}
