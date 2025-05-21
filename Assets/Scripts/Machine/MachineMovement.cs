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

            //CheckGround();
            //isGrounded();
        }
        private void Update()
        {
                _moveInput = Input.GetAxis("Vertical");
                _rotateInput = Input.GetAxisRaw("Horizontal");
           
                //transform.Rotate(Vector3.up * _speedRotation * Time.deltaTime * _rotateInput);

            if (_inventory.HasResource(RecolectableType.HyperCrystal))
            {

                //energyTimer += Time.deltaTime;
                //if (energyTimer >= energyDecayRate)
                //    _inventory.UseResource(RecolectableType.HyperCrystal);


            }
            else
            {
                GameController.Instance.MachineController.StopMoving();
                Debug.Log("Not enough Crystals");
                OnNoCrystals?.Invoke();

            }

            isGrounded();
            


        }
        bool isGrounded()
        {
            float checkDistance = 5f;
            int terrainLayer = 1 << LayerMask.NameToLayer("Terrain");

            Vector3[] offsets = new Vector3[]
            {
                Vector3.zero,
                Vector3.right * 1f,
                Vector3.left * 1f,
                Vector3.forward * 1f,
                Vector3.back * 1f
            };

            foreach (var offset in offsets)
            {
                Vector3 origin = transform.position + offset;
                Debug.DrawRay(origin, Vector3.down * checkDistance, Color.red);

                if (Physics.Raycast(origin, Vector3.down, checkDistance, terrainLayer))
                    return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
            Vector3 move = flatForward * _moveInput  * _speedForce;
            _rb.velocity = move * _speedForce;

            if (Math.Abs(_rotateInput) > 0.01f)
            {
                float rotationDegrees = _rotateInput * _speedRotation * Time.fixedDeltaTime;
                Quaternion deltaRotation = Quaternion.Euler(0f, rotationDegrees, 0f);
                _rb.MoveRotation(_rb.rotation * deltaRotation);

            }



            //Snap to the ground
            bool grounded = isGrounded();

            if (grounded)
            {
                print("Grounded");
                _isGrounded = true;
            }
            else
            {
                _rb.AddForce(Vector3.down * _forceDown, ForceMode.Acceleration);
            }

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
