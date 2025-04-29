using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.UI
{


    public class RespawnPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _respawnPanel;

        private void Awake()
        {
            _respawnPanel = GetComponentInChildren<GameObject>();
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
            _respawnPanel.SetActive(false);
        }
        internal void Died()
        {
            _respawnPanel.SetActive(true);
        }
    }
}
