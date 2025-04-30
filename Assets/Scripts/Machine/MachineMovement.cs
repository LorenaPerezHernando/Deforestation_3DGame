using System.Collections;
using Deforestation.Dinosaurus;
using Deforestation.Recolectables;
using Unity.VisualScripting;
using UnityEngine;
namespace Deforestation.Machine
{
	[RequireComponent(typeof(Rigidbody))]
    public class MachineMovement : MonoBehaviour
	{
		#region Fields
		[SerializeField] private float jumpForce = 500000;
		[SerializeField] private float groundCheckDistance = 20;
		[SerializeField] private bool isGrounded;

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
		#endregion

		#region Unity Callbacks	
		private void Awake()
		{
			
			_rb = GetComponent<Rigidbody>();
			_machineController = GetComponent<MachineController>();
		}

        private void Start()
        {
			StartCoroutine(TextSalirMaquina());
			
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
				if (isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
				{
                    
                    energyTimer += Time.deltaTime;
					if (energyTimer >= energyDecayRate)
					{
						_inventory.UseResource(RecolectableType.HyperCrystal);
						energyTimer = 0f; //Puede (reiniciar timer)
					}
				}

				
				//Salto 
				if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
				{
					_rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
					
				}
			}
			else
			{
				GameController.Instance.MachineController.StopMoving();
				Debug.Log("Not enough Cristals");
			}

			CheckGround();
		}

		IEnumerator TextSalirMaquina()
		{
            _textSalirMaquina.SetActive(true);
			yield return new WaitForSeconds(10f);
			_textSalirMaquina.SetActive(false);
        }

        private void FixedUpdate()
        {
            _rb.AddRelativeForce(_movementDirection.normalized * _speedForce, ForceMode.Impulse);
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");

            //Movimiento solo en XZ
            Vector3 flatForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
            Vector3 forwardForce = flatForward * moveInput * _speedForce;
            Debug.DrawRay(transform.position, flatForward * 5f, Color.green);
            //Rotar en Y
            Quaternion turnRotation = Quaternion.Euler(0f, turnInput * _speedRotation * Time.fixedDeltaTime, 0f);
            _rb.MoveRotation(_rb.rotation * turnRotation);
        }
       
        private bool RaycastGround(Vector3 offset)
        {
            int layerMask = 1 << LayerMask.NameToLayer("Terrain"); // O usa ~0 para todo
            Vector3 origin = transform.position + offset;
            Debug.DrawRay(origin, Vector3.down * groundCheckDistance, Color.red); // Para depurar
            return Physics.Raycast(origin, Vector3.down, groundCheckDistance, layerMask);
        }
        void CheckGround()
		{
            bool grounded =
			RaycastGround(Vector3.zero) || // Centro
			RaycastGround(Vector3.right * 1f) ||
			RaycastGround(Vector3.left * 1f) ||
			RaycastGround(Vector3.forward * 1f) ||
			RaycastGround(Vector3.back * 1f);

			
            isGrounded = grounded;

			// Si no está en el suelo, aplica fuerza hacia abajo
			if (!isGrounded)
			{
				Debug.Log("Not grounded");
				_rb.AddRelativeForce(Vector3.down * 100000f);
				Debug.Log("Should be grounded");
			}

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
