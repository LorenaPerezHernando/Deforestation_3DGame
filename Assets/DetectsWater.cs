using System.Collections;
using System.Collections.Generic;
using Deforestation;
using UnityEngine;

public class DetectsWater : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _prefabfireParticles;
    private bool _fireOn;
    [SerializeField]  private GameObject _playerFireParticle;

    private float _attackCoolDown;
    [SerializeField] private float _attackTime = 2;
    [SerializeField] private float _attackDamage = 5;

    #endregion
    private void Awake()
    {
        
        

    }

    private void Start()
    {
   
      
    }
    #region Private Methods

    private void OnTriggerEnter(Collider other)
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

                _playerFireParticle = GameObject.FindGameObjectWithTag("Player Fire Particle");
                GameController.Instance.HealthSystem.TakeDamage(_attackDamage);

            }
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        _attackCoolDown = 0;
        Destroy(_playerFireParticle);
        _fireOn = false; 
    }
    #endregion
}
