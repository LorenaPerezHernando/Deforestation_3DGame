using UnityEngine;
using System;
using Deforestation.Machine.Weapon;

namespace Deforestation.Machine
{
	[RequireComponent (typeof(HealthSystem))]
	public class MachineController : MonoBehaviour
	{
		#region Properties
		public HealthSystem HealthSystem => _health;
		public WeaponController WeaponController;
		public Action<bool> OnMachineDriveChange;

		#endregion

		#region Fields
		private HealthSystem _health;
		private MachineMovement _movement;
		private Animator _anim;

		#endregion

		#region Unity Callbacks
		private void Awake()
		{
			_health = GetComponent<HealthSystem>();
			_movement = GetComponent<MachineMovement>();
			_anim = GetComponent<Animator>();

		}
		// Start is called before the first frame update
		void Start()
		{
			_movement.enabled = false;
		}

		// Update is called once per frame
		void Update()
		{
			//TODO: Mover a Input System
			if (Input.GetKeyUp(KeyCode.Escape))
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

		}

		public void StartDriving(bool machineMode)
		{
			enabled = machineMode;
			_movement.enabled = machineMode;
			_anim.SetTrigger("WakeUp");
			_anim.SetBool("Move", machineMode);
			OnMachineDriveChange?.Invoke(true);
		}

		public void StopMoving()
		{
			_movement.enabled = false;
			_anim.SetBool("Move", false);
		}
		#endregion

		#region Private Methods
		#endregion
	}

}