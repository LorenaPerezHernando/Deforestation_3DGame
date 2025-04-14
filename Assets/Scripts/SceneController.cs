using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Deforestation
{


    public class SceneController : MonoBehaviour
    {
        [SerializeField] private int _currentScene;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {

                if (_currentScene == 0)
                    Application.Quit();

                //TODO ACTIVAR EN LA BUILD 2a parte 
                //if (_currentScene == 1) Desactivado pq sino no puedo darle a esc para ver los componentes de la escena 1
                //{
                //    SceneManager.LoadScene(0);
                //    Cursor.visible = true;
                //}
            }

                
            if (_currentScene == 0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }


        }

        public void SceneToGame()
        {
            SceneManager.LoadScene(1);
            Cursor.visible = false;
            _currentScene = 1;
        }

        public void SceneToSettings()
        {
            SceneManager.LoadScene(2);
            _currentScene = 2;
        }
        public void SceneToMainMenu()
        {
            Cursor.visible = true;
            SceneManager.LoadScene(0);
            _currentScene = 0;
        }
    }
}
