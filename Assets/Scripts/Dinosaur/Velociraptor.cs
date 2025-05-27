using System;
using System.Collections;
using System.Collections.Generic;
using Deforestation.Dinosaurus;
using UnityEngine;
using UnityEngine.AI;
namespace Deforestation.Dinosaurus
{


    public class Velociraptor : Dinosaur
    {
        public Action OnHurt;
        #region Fields
        [Header("Hurt Dialogue")]
        [Header("Tower NOT Repaired")]
        [SerializeField] private GameObject _hurtDialogue;
        [Header("Attack")]
        [SerializeField] private float _distanceDetection = 50;
        [SerializeField] private float _attackDistance = 7;
        //private Vector3 _playerPos => GameController.Instance.Inventory.transform.position;

        [SerializeField] private Transform _playerPos;
        
        private bool _chase;
        private bool _attack;

        [SerializeField] private float _attackTime = 2;
        [SerializeField] private float _attackDamage = 5;
        private float _attackColdDown;


        #endregion

        #region Unity Callbacks

        private void Awake()
        {

            _hurtDialogue = GameObject.FindGameObjectWithTag("HurtDialogue");
            _anim = GetComponent<Animator>();
           
        }
        private void Start()
        {
            _playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _agent = GetComponent<NavMeshAgent>();
            _hurtDialogue.SetActive(false);
            if (_playerPos != null)
            {
                _playerPos = _playerPos.transform;
            }
            else
            {
                Debug.LogError(" No se encontró el jugador con el tag 'Player'");
            }
        }


        void Update()
        {


            //Idle
            if (!_chase && !_attack && Vector3.Distance(transform.position, _playerPos.position) < _distanceDetection)
            {
                print("Chase");
                ChasePlayer();
                return;
            }

            //Chase
            if (_chase)
            {

                NavMeshHit hit;
                if (NavMesh.SamplePosition(_playerPos.position, out hit, _attackDistance, 1))
                    _agent.SetDestination(hit.position);

                if (_agent.isStopped)
                    _agent.isStopped = false;

            }

            if (_chase && Vector3.Distance(transform.position, _playerPos.position) < _attackDistance)
            {
                print("Atacar");
                Attack();
                return;
            }
            if (_chase && Vector3.Distance(transform.position, _playerPos.position) > _distanceDetection)
            {
                print("Idle");
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
            if (_attack && Vector3.Distance(transform.position, _playerPos.position) > _attackDistance)
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
            _agent.SetDestination(_playerPos.position);
            _anim.SetBool("Run", true);
            _anim.SetBool("Attack", false); 
            
            _chase = true;
            _attack = false;
           
        }

        private void Attack()
        {
            OnHurt?.Invoke();
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
