using System.Collections;
using System.Collections.Generic;
using Deforestation.Dinosaurus;
using UnityEngine;
namespace Deforestation.Dinosaurus
{


    public class Velociraptor : Dinosaur
    {
        #region Fields
        [SerializeField] private float _distanceDetection = 50;
        [SerializeField] private float _attackDistance = 7;
        private Vector3 _playerPosition => GameController.Instance.HealthSystem.transform.position;

        private bool _chase;
        private bool _attack;
        private bool _dead;

        HealthSystem _healthSystem;
        #endregion

        #region Unity Callbacks
        

       
        void Update()
        {
            //Idle
            if(!_chase && !_attack &&  !_dead && Vector3.Distance(transform.position, _playerPosition) < _distanceDetection)
            {
                Idle();
                return; 
            }
            //Chase
            if (_chase && Vector3.Distance(transform.position, _playerPosition ) < _distanceDetection)
            {
                ChasePlayer();
                return; 
            }
            if (_chase && Vector3.Distance(transform.position, _playerPosition) > _distanceDetection)
            {
                Idle(); 
                return;
            }

            if (_attack && Vector3.Distance(transform.position, _playerPosition) > _attackDistance)
            {
                ChasePlayer();
                return;
            }
            if (_chase && Vector3.Distance(transform.position, _playerPosition) < _attackDistance)
            {
                Attack();
                return;
            }

        }
        #endregion

        #region Private Methods
        private void Idle()
        {
            _anim.SetBool("Run", false);
            _anim.SetBool("Attack", false);
            _agent.isStopped = true;
            _chase = false;
            _attack = false;
            _dead = false;
        }
        private void ChasePlayer()
        {
            _anim.SetBool("Run", true);
            _anim.SetBool("Attack", false); 
            _agent.SetDestination(_playerPosition);
            _chase = true;
            _attack = false;
            _dead = false;
        }

        private void Attack()
        {
            _anim.SetBool("Run", false);
            _anim.SetBool("Attack", true);
            _agent.isStopped = true;
            _chase = false;
            _attack = true;
            _dead = false;
        }

        #endregion
    }
}
