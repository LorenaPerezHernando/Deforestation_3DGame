using Deforestation.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Network
{


    public class UINetwork : MonoBehaviour
    {
        [SerializeField] private GameObject _connectingPanel;
        [SerializeField] private UIGameController _uiGameController;

        private void Awake()
        {
            _uiGameController = GetComponent<UIGameController>();
        }
        void Start()
        {

        }

      
        public void LoadingComplete()
        {
            _connectingPanel.SetActive(false);
            _uiGameController.enabled = true;
        }
    }
}
