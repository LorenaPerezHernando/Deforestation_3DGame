using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Network
{


    public class UINetwork : MonoBehaviour
    {
        [SerializeField] private GameObject _connectingPanel;
        void Start()
        {

        }

      
        public void LoadingComplete()
        {
            _connectingPanel.SetActive(false);
        }
    }
}
