using UnityEngine;
using System;
using Deforestation.Machine.Weapon;
using System.Collections;

namespace Deforestation.Machine
{
	[RequireComponent (typeof(HealthSystem))]
	[RequireComponent(typeof(Animator))]
	public class MachineController : MonoBehaviour
	{
		#region Properties
		public bool isDriving { get; private set; }
		public Action OnTextSalirMaquina;

		public HealthSystem HealthSystem => _health;
		public WeaponController WeaponController;
		public MachineMovement MachineMovement;
		public Action<bool> OnMachineDriveChange;

		#endregion

		#region Fields
		[SerializeField] private GameObject _machineModel;
        [SerializeField] private Vector3 _machineScale = new Vector3(300, 300, 300);
		private HealthSystem _health;
		private Animator _anim;

		#endregion

		#region Unity Callbacks
		private void Awake()
		{
			_health = GetComponent<HealthSystem>();
			MachineMovement = GetComponent<MachineMovement>();
			_anim = GetComponent<Animator>();

		}
		// Start is called before the first frame update
		void Start()
		{
			MachineMovement.enabled = false;
		}

		
		void Update()
		{
            //TODO Input en MachineMovement
            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.E))
            {
                StopDriving();

            }
			

        }		

		#endregion

		#region Public Methods
		public void StopDriving()
		{
           
            GameController.Instance.MachineMode(false);
			StopMoving();
			OnMachineDriveChange?.Invoke(false);

            StartCoroutine(ScaleMachine());

        }

	

        public void StartDriving(bool machineMode)
		{
			_anim.enabled = true;

            OnTextSalirMaquina?.Invoke();
			enabled = machineMode;
			MachineMovement.enabled = machineMode;
			_anim.SetTrigger("WakeUp");
			_anim.SetBool("Move", machineMode);
			//OnDriveSound?.Invoke();
			OnMachineDriveChange?.Invoke(true);
		}

		public void StopMoving()
		{
			MachineMovement.enabled = false;
			_anim.SetBool("Move", false);

           
         

        }

        private IEnumerator ScaleMachine()
        {
            _anim.enabled = false;
            yield return null; 
            Debug.Log("Scale Maquina");

            _machineModel.transform.localScale = _machineScale;


        }
        #endregion

        #region Private Methods
        #endregion
    }

}