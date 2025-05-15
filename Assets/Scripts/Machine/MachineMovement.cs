using System.Collections;
using Deforestation.Dinosaurus;
using Deforestation.Recolectables;
using Unity.VisualScripting;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.StandaloneInputModule;
namespace Deforestation.Machine
{
	[RequireComponent(typeof(Rigidbody))]
    public class MachineMovement : MonoBehaviour
	{
		#region Fields
		public Action OnNoCrystals;
        private bool _jumpPressed;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _jumpForce = 500000;
		[SerializeField] private float _groundCheckDistance = 20;
		[SerializeField] private bool _isGrounded;
		[SerializeField] private float _forceDown;

		[SerializeField] private float _speedForce = 50;
		[SerializeField] private float _speedRotation = 15;
		private Rigidbody _rb;
		private Vector3 _movementDirection;
		private MachineController _machineController;
		private Inventory _inventory => GameController.Instance.Inventory;
		[Header("UI")]
		[SerializeField] GameObject _textSalirMaquina;
		[Header("Energy")]
		[SerializeField] private float energyDecayRate = 20f;
		private float energyTimer = 0f;
        #endregion

        #region Properties
        private float _moveInput;
        private float _rotateInput;
        #endregion

        #region Unity Callbacks	
        private void Awake()
		{			
			_rb = GetComponent<Rigidbody>();
			_machineController = GetComponent<MachineController>();
		}

        private void Start()
        {
			//StartCoroutine(TextSalirMaquina());
			CheckGround();
        }
        private void Update()
        {
            if (_inventory.HasResource(RecolectableType.HyperCrystal))
            {
                _moveInput = Input.GetAxis("Vertical");
                ////Movement
                //transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * Input.GetAxis("Horizontal"));



                //Energy
                //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                //{
                //    energyTimer += Time.deltaTime;
                //    if (energyTimer >= energyDecayRate)
                //        _inventory.UseResource(RecolectableType.HyperCrystal);
                //}
            }
            else
            {
                GameController.Instance.MachineController.StopMoving();
                Debug.Log("Not enough Crystals");
                OnNoCrystals?.Invoke();

            }


        }

        private void FixedUpdate()
        {
            //CheckGround();
            Vector3 flatForward = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
            Vector3 move = flatForward * _moveInput ;
            _rb.velocity = move * _speedForce;

            // Rotación en Y
            _rotateInput = Input.GetAxis("Horizontal");
            float rotationDegrees = _rotateInput * _speedRotation;
            float rotationRadians = rotationDegrees * Mathf.Deg2Rad;
            _rb.angularVelocity = new Vector3(0f, rotationRadians, 0f);

        }

        void CheckGround()
        {
            RaycastHit hit;
            float maxDistance = 3f;
            float force = 100000;
            Vector3 direction = -transform.up;
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            //Lanza un rayo hacia abajo desde la posición del objeto
            //if (!Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
            //    _rb.AddRelativeForce(direction * force);


        }


        private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Tree")
			{
				int index = other.GetComponent<Tree>().Index;
				GameController.Instance.TerrainController.DestroyTree(index, other.transform.position);
                Destroy(other.gameObject);
			}          

        }
		private void OnCollisionEnter(Collision collision)
		{
			//Hacemos daño por contacto a los Stegasaurus
			HealthSystem target = collision.gameObject.GetComponent<HealthSystem>();
			if (target != null && target.tag == "Dinosaur") 
			{
				target.TakeDamage(10);
			}

			 
		}

		

		#endregion


	}
	
}
