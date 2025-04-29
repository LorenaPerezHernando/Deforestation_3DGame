using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deforestation.UI
{


    public class InitialStory : MonoBehaviour
    {
        [SerializeField] private Sprite[] _imagenesIniciales;
        [SerializeField] private Image _imagenActual;
        private int _currentIndex = 0;
        private float _fadeDuration = 1f;


        [SerializeField] private GameObject _firstDialogue;
        void Start()
        {
            _firstDialogue.SetActive(true);

        }
        internal void ShowNextImage()
        {
            ShowImage(_currentIndex );
            _currentIndex++;
            if (_currentIndex >= _imagenesIniciales.Length) _currentIndex = 0;
            _imagenActual.sprite = _imagenesIniciales[_currentIndex];
            FadeImage( _imagenActual );
        }

        private void ShowImage(int index)
        {
            if (index >= 0 && index < _imagenesIniciales.Length)
                _imagenActual.sprite = _imagenesIniciales[index];
        }
        private IEnumerator FadeImage(Image img)
        {
            float timeElapsed = 0f;
            Color startColor = img.color;

            // Asegúrate de que empiece visible
            img.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

            while (timeElapsed < _fadeDuration)
            {
                timeElapsed += Time.deltaTime;

                float alpha = Mathf.Lerp(1f, 0f, timeElapsed / _fadeDuration);

                img.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

                yield return null;
            }

            // Asegura que termine completamente invisible
            img.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        }
    }
}
