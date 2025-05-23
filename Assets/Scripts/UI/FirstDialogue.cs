using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static UnityEngine.AudioSettings;
using Unity.VisualScripting;

namespace Deforestation.Dialogue
{
    public class FirstDialogue : MonoBehaviour
    {
        public Action OnBrokenNoise;
        public Action OnTowerJustDestroyed;
        public Action OnNextImage;
        public Action OnFinishImages;
        
        [SerializeField] private GameObject _movilIcon;
        [SerializeField] private GameObject _dialoguePanel;
       


        [SerializeField] private string[] _initialMessages; // Array de mensajes
        [SerializeField] private TextMeshProUGUI _uiText; // UI para mostrar los mensajes

        [SerializeField] private int _mensajeActual = 0;

        private void Awake()
        {
            _uiText = GetComponent<TextMeshProUGUI>();
        }
        void Start()
        {

            _movilIcon.SetActive(true);

            _uiText.text = _initialMessages[_mensajeActual]; // Mostrar primer mensaje

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E))
            {
                _mensajeActual++;

                if(_mensajeActual == 2)
                    OnNextImage?.Invoke();

               if(_mensajeActual == 4)
                    OnBrokenNoise?.Invoke();

                if (_mensajeActual == 5)
                {
                    
                    OnTowerJustDestroyed?.Invoke();
                    _movilIcon?.SetActive(false);
                    
                }

                if(_mensajeActual == 6)
                    OnNextImage?.Invoke();

                InitialMessages();

            }
        }
       

        internal void InitialMessages()
        {
            if (_mensajeActual < _initialMessages.Length)
            {
                Animator movil = _movilIcon.GetComponent<Animator>();
                movil.enabled = false;
                _uiText.text = _initialMessages[_mensajeActual];
              
            }


            if (_mensajeActual >= _initialMessages.Length)
            {
                //TODO Camera enfoca a la torre destrozandose, explosion
                //Particulas de explosion
                //SONIDOS de explosion y de colgar la llamada
                _dialoguePanel.SetActive(false);
                _movilIcon?.SetActive(false);
                OnFinishImages?.Invoke();
                Destroy(gameObject);
            }
        }

       
    }
}

