using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Deforestation.Tower
{


    public class TowerRepairDialogue : MonoBehaviour
    {
        
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private GameObject _towerUI;
        private TextMeshProUGUI _uiTowerText; // UI para mostrar los mensajes
        private TextMeshProUGUI _text;
        
        
        [SerializeField] private string[] _initialMessages; // Array de mensajes

        [SerializeField] private int _mensajeActual = 0;

        private void Awake()
        {
            
            _text = GetComponent<TextMeshProUGUI>();
            _uiTowerText = _towerUI.GetComponentInChildren<TextMeshProUGUI>();
            _mensajeActual = 0; 
        }
        void Start()
        {
            //_dialoguePanel.SetActive(true);
            _text.text = _initialMessages[_mensajeActual]; // Mostrar primer mensaje
            StartCoroutine(MensajesAutomaticos());
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.E))
            {
                _mensajeActual++;
                InitialMessages();

            }
        }

        public void InitialMessages()
        {
            StartCoroutine(MensajesAutomaticos());
            if (_mensajeActual < _initialMessages.Length)
            {
                _text.text = _initialMessages[_mensajeActual];

            }


            if (_mensajeActual >= _initialMessages.Length)
            {
                _towerUI.SetActive(true);
                _uiTowerText.enabled = true;
                //TODO Camera enfoca a la torre destrozandose, explosion
                //Particulas de explosion
                _dialoguePanel.SetActive(false);
                print("Dialogue panel false");
                Destroy(gameObject);
            }
        }
        IEnumerator MensajesAutomaticos()
        {
            if (_mensajeActual >= _initialMessages.Length)
                yield break;
            Debug.Log("Corrutina Dialogos");
            yield return new WaitForSeconds(4f);
            _mensajeActual++;
            InitialMessages();

        }
    }
}

