using System.Collections;
using System.Collections.Generic;
using Deforestation;
using UnityEngine;

namespace Deforestation.Player
{
    public class MachineDetectsWater : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _prefabfireParticles;
        private bool _fireOn;
        [SerializeField] private GameObject[] _machineFireParticle;

        private float _attackCoolDown;
        [SerializeField] private float _attackTime = 2;
        [SerializeField] private float _attackDamage = 5;

        #endregion

        #region Private Methods

        private void Start()
        {
            _fireOn = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "CollisionWater")
            {
                print("Trigger Con agua");
                _attackCoolDown -= Time.deltaTime;
                if (_attackCoolDown <= 0)
                {
                    _prefabfireParticles.SetActive(true);
                    _attackCoolDown = _attackTime;


                    if (_fireOn == false)
                    {
                        GameObject fire = Instantiate(_prefabfireParticles, transform.position, Quaternion.identity, transform);

                        _fireOn = true;
                    }


                    _machineFireParticle = GameObject.FindGameObjectsWithTag("Machine Fire Particle");
                    GameController.Instance.MachineController.HealthSystem.TakeDamage(_attackDamage);

                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            _attackCoolDown = 0;
            foreach (GameObject fireParticle in _machineFireParticle)
            {
                Destroy(fireParticle);
            }
            _fireOn = false;
        }
        #endregion
    }
}

