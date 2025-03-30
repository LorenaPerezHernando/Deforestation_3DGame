using System.Collections;
using System.Collections.Generic;
using Deforestation.Dinosaurus;
using UnityEngine;
using UnityEngine.AI;
namespace Deforestation.Dinosaurus
{


    public class Velociraptor : Dinosaur
    {
        #region Fields
        [SerializeField] private float _distanceDetection = 50;
        [SerializeField] private float _attackDistance = 7;
        private Vector3 _playerPosition => GameController.Instance.Inventory.transform.position;

        private bool _chase;
        private bool _attack;

        [SerializeField] private float _attackTime = 2;
        [SerializeField] private float _attackDamage = 5;
        private float _attackColdDown;


        #endregion

        #region Unity Callbacks



        void Update()
        {
           
            //Idle
            if (!_chase && !_attack && Vector3.Distance(transform.position, _playerPosition) < _distanceDetection)
            {
                ChasePlayer();
                return;
            }

            //Chase
            if (_chase)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(_playerPosition, out hit, _attackDistance, 1))
                    _agent.SetDestination(hit.position);
            }

            if (_chase && Vector3.Distance(transform.position, _playerPosition) < _attackDistance)
            {
                Attack();
                return;
            }
            if (_chase && Vector3.Distance(transform.position, _playerPosition) > _distanceDetection)
            {
                Idle();
                return;
            }

            //Attack
            if (_attack)
            {
                //Atack damage to player
                _attackColdDown -= Time.deltaTime;
                if (_attackColdDown <= 0)
                {
                    print("Haciendo daño al player");
                    _attackColdDown = _attackTime;
                    GameController.Instance.HealthSystem.TakeDamage(_attackDamage);
                }
            }
            if (_attack && Vector3.Distance(transform.position, _playerPosition) > _attackDistance)
            {
                ChasePlayer();
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

        }
        private void ChasePlayer()
        {       

            _anim.SetBool("Run", true);
            _anim.SetBool("Attack", false); 
            _agent.SetDestination(_playerPosition);
            
            _chase = true;
            _attack = false;
           
        }

        private void Attack()
        {
            _anim.SetBool("Run", false);
            _anim.SetBool("Attack", true);
            _agent.isStopped = true;
            _chase = false;
            _attack = true;
        
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _distanceDetection);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
        }

        #endregion
    }
}
