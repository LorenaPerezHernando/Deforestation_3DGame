using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Recolectables
{


    public class InteractDagger : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueDagger;
        [SerializeField] private GameObject _daggerPlayer;

        public void DaggerActives()
        {
            if (_dialogueDagger != null && _daggerPlayer != null)
            {
                _dialogueDagger.SetActive(true);
                _daggerPlayer.SetActive(true);
            }
        }
    }
}
