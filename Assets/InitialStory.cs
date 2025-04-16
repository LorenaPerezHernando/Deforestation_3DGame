using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deforestation.UI
{


    public class InitialStory : MonoBehaviour
    {
        [SerializeField] private Image _firstImage;
        [SerializeField] private Image _secondImage;

        [SerializeField] private GameObject _firstDialogue;
        void Start()
        {
            _firstDialogue.SetActive(true);
            _firstImage.enabled = true;
            FadeAlpha();
        }

        private void FadeAlpha()
        {
            float fadeDuration = 5f;
            float timeElapsed = 0f;

            // Obtén el color actual de la imagen
            Color startColor = _firstImage.color;

            // La imagen debe estar completamente visible al inicio
            while (timeElapsed < fadeDuration)
            {
                timeElapsed += Time.deltaTime;

                // Calcula el alpha en base al tiempo transcurrido
                float alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);

                // Actualiza el color de la imagen con el nuevo alpha
                _firstImage.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            }
        }
    }
}
