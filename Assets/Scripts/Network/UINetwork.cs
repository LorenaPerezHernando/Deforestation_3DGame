using Deforestation.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Deforestation.Network
{


    public class UINetwork : MonoBehaviour
    {
        #region Fields
        [SerializeField] private GameObject _connectingPanel;
        [SerializeField] private UIGameController _uiGameController;
        //[SerializeField] private Button _exitButton;
        //[SerializeField] private Button _retryButton;

        #endregion

        #region Properties
        public GameObject EndGamePanel;
        #endregion

        #region Unity Callbacks	
        private void Awake()
        {
            //_exitButton.onClick.AddListener(Exit);
            //_retryButton.onClick.AddListener(Retry);
            _uiGameController = GetComponentInChildren<UIGameController>();
        }

        private void Start()
        {
            _uiGameController.enabled = true;
        }

        private void Retry()
        {
            SceneManager.LoadScene(0);
        }

        private void Exit()
        {
            Application.Quit();
        }
        #endregion



        public void LoadingComplete()
        {
            _uiGameController.enabled = true;
            _connectingPanel.SetActive(false);
            
        }
    }
}
