using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Recolectables
{


    public class InteractDagger : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueDagger;
        [SerializeField] private GameObject _daggerPlayer;
        [SerializeField] private GameObject _dialoguePanel;

        public void DaggerActives()
        {
            if (_dialogueDagger != null && _daggerPlayer != null)
            {
                _dialoguePanel.SetActive(true);
                _dialogueDagger.SetActive(true);
                _daggerPlayer.SetActive(true);

            }
        }
    }
}
