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
    public class MachineMovement1 : MonoBehaviour
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
                //Movement
                _movementDirection = new Vector3(Input.GetAxis("Vertical"), 0, 0);
                transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * Input.GetAxis("Horizontal"));
                Debug.DrawRay(transform.position, transform.InverseTransformDirection(_movementDirection.normalized) * _speedForce);

                //Energy
                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    energyTimer += Time.deltaTime/2;
                    if (energyTimer >= energyDecayRate)
                        _inventory.UseResource(RecolectableType.HyperCrystal);
                }
            }
            else
            {
                GameController.Instance.MachineController.StopMoving();
            }

            Check();
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(_movementDirection.normalized * _speedForce, ForceMode.Impulse);
        }

        void Check()
        {
            RaycastHit hit;
            float maxDistance = 4f;
            float force = 100000;
            Vector3 direction = -transform.up;

            // Dibuja el rayo en el editor
            Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

            // Calcula la máscara de la capa correctamente
            int layerMask = 1 << LayerMask.NameToLayer("Terrain");

            // Lanza un rayo hacia abajo desde la posición del objeto
            if (!Physics.Raycast(transform.position, direction, out hit, maxDistance, layerMask))
                _rb.AddRelativeForce(direction * force);
        }

			
		

		IEnumerator TextSalirMaquina()
		{
            _textSalirMaquina.SetActive(true);
			yield return new WaitForSeconds(5f);
			_textSalirMaquina.SetActive(false);
        }



        void CheckGround()
		{
           

			// Si no está en el suelo, aplica fuerza hacia abajo
			

			
        }

        private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Tree")
			{
				int index = other.GetComponent<Tree>().Index;
				GameController.Instance.TerrainController.DestroyTree(index, other.transform.position);
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
