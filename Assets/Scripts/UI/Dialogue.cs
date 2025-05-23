using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Deforestation.Dialogue
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] private GameObject _dialoguePanel;
        [SerializeField] private string[] _initialMessages; // Array de mensajes
        [SerializeField] private TextMeshProUGUI _uiText; // UI para mostrar los mensajes

        [SerializeField] private int _mensajeActual = 0;
        void Start()
        {
            _dialoguePanel.SetActive(true);
            _uiText.text = _initialMessages[_mensajeActual]; // Mostrar primer mensaje
            StartCoroutine(MensajesAutomaticos());

        }

        // Update is called once per frame
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
                _uiText.text = _initialMessages[_mensajeActual];
               
            }


            if (_mensajeActual >= _initialMessages.Length)
            {
                _dialoguePanel.SetActive(false);
                Destroy(gameObject);
            }
        }


        IEnumerator MensajesAutomaticos()
        {
            Debug.Log("Corrutina Dialogos");
            yield return new WaitForSeconds(4f);
            _mensajeActual++;
            InitialMessages();

        }
    }
}

