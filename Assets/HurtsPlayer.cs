using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Player
{
    public class HurtsPlayer : MonoBehaviour
    {

        #region Fields
        [SerializeField] private GameObject _hurtParticleSystem;
        private float _attackCoolDown;
        [SerializeField] private float _attackTime = 2;
        [SerializeField] private float _attackDamage = 5;

        #endregion
        private void Start()
        {
            _hurtParticleSystem.SetActive(false);
        }
        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            _attackCoolDown -= Time.deltaTime;
            if (_attackCoolDown <= 0)
            {
                _hurtParticleSystem.SetActive(true);
                _attackCoolDown = _attackTime;

                GameController.Instance.HealthSystem.TakeDamage(_attackDamage);
                //TODO PARTICULAS DE QUEMARSE
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _attackCoolDown = 0;
            _hurtParticleSystem?.SetActive(false);
        }
        #endregion
    }
}
