using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Deforestation.Dialogue
{
    public class FirstDialogue : MonoBehaviour
    {
        //TODO Icono de un movil sonando
        [SerializeField] private GameObject _movilIcon;
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private GameObject _dialogoRepararTorre;


        [SerializeField] private string[] _initialMessages; // Array de mensajes
        [SerializeField] private TextMeshProUGUI _uiText; // UI para mostrar los mensajes

        [SerializeField] private int _mensajeActual = 0;

        private void Awake()
        {
            _uiText = GetComponent<TextMeshProUGUI>();
        }
        void Start()
        {
            _dialogoRepararTorre.SetActive(false);
            _movilIcon.SetActive(true);
            _dialoguePanel.SetActive(true);
            _uiText.text = _initialMessages[_mensajeActual]; // Mostrar primer mensaje

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                _mensajeActual++;
                InitialMessages();

            }
        }

        private void NextMessage()
        {
            _mensajeActual++;
        }

        public void InitialMessages()
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
                _movilIcon?.SetActive(false);
                _dialogoRepararTorre?.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}

