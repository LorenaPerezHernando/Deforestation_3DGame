using UnityEngine;
using Deforestation.Machine;
using Deforestation.UI;
using Deforestation.Recolectables;
using Deforestation.Interaction;
using Cinemachine;
using System;
using Deforestation.Dialogue;
using Deforestation.Tower;
using Deforestation.Checkpoints;

namespace Deforestation
{
	public class GameController : Singleton<GameController>
	{
		#region Properties
		public MidCheckpoint MidCheckpoint => _midCheckpoint;
		public TowerInteraction TowerInteraction => _towerInteraction;
		public FirstDialogue FirstDialogue => _firstDialogue;
		public InitialStory InitialStory => _initialStory;
		public MachineController MachineController => _machine;
		public HealthSystem HealthSystem => _playerHealth;
		public Inventory Inventory => _inventory;
		public InteractionSystem InteractionSystem => _interactionSystem;
		public TreeTerrainController TerrainController => _terrainController;
		public Camera MainCamera;
		public DaggerHurts DaggerHurts => _dagger;

		//Events
		public Action<bool> OnMachineModeChange;

		public bool MachineModeOn
		{
			get
			{
				return _machineModeOn;
			}
			private set
			{
				_machineModeOn = value;
				OnMachineModeChange?.Invoke(_machineModeOn);
			}
		}
		#endregion

		#region Fields
		[Header("Checkpoint")]
		[SerializeField] protected MidCheckpoint _midCheckpoint;
		private Vector3 _savedPlayerPos;
		private Vector3 _savedMachinePos;
		[Header("Player")]
		private GameObject _thePlayer;
		[SerializeField] protected CharacterController _player;
		[SerializeField] protected HealthSystem _playerHealth;
		[SerializeField] protected Inventory _inventory;
		[SerializeField] protected InteractionSystem _interactionSystem;

		[SerializeField] protected TowerInteraction _towerInteraction;

		[Header("Camera")]
		[SerializeField] protected CinemachineVirtualCamera _virtualCamera;
		[SerializeField] protected Transform _playerFollow;
		[SerializeField] protected Transform _machineFollow;

		[Header("Machine")]
		private GameObject _theMachine;
		[SerializeField] protected MachineController _machine;
		[Header("UI")]
		[SerializeField] protected FirstDialogue _firstDialogue;
		[SerializeField] protected UIGameController _uiController;
		[SerializeField] protected InitialStory _initialStory;
		[Header("Trees Terrain")]
		[SerializeField] protected TreeTerrainController _terrainController;
		[Header("Dinosaurs")]
		[SerializeField] protected DaggerHurts _dagger;

		private bool _machineModeOn;
		private Quaternion _originalPlayerRotation;
        #endregion

        #region Unity Callbacks


        void Start()
		{
			//UI Update
			_playerHealth.OnHealthChanged += _uiController.UpdatePlayerHealth;
			_machine.HealthSystem.OnHealthChanged += _uiController.UpdateMachineHealth;
			MachineModeOn = false;
			_originalPlayerRotation =_player.transform.rotation;
		}
		void Update()
		{
		}
		#endregion

		#region Public Methods
		public void NextPicInitial()
		{
			_firstDialogue.OnNextPic += _initialStory.ShowNextImage;
		}
		public void SaveCheckpoint(Vector3 _savedplayerPos, Vector3 _savedMachinePos)
		{
			_player.transform.position = _savedPlayerPos;
			_machine.transform.position = _savedMachinePos;	

		}
		public void RespawnAtCheckpoint()
		{
			
			_playerHealth.OnDeath += _inventory.RestartCrystals;
			_playerHealth.OnDeath += _midCheckpoint.Check;
            _playerHealth.OnDeath += () => TeleportPlayer(_savedPlayerPos);
        }
		public void TeleportPlayer(Vector3 target)
		{
			_player.enabled = false;
			_player.transform.position = target;
			_player.enabled = true;
		}

		internal void MachineMode(bool machineMode)
		{
			MachineModeOn = machineMode;
			//Player
			_player.gameObject.SetActive(!machineMode);
			
			_player.enabled = !machineMode;

			//Cursor + UI
			if (machineMode)
			{
				//Start Driving
				if (Inventory.HasResource(RecolectableType.HyperCrystal))
					_machine.StartDriving(machineMode);

				_player.transform.parent = _machineFollow;
				_uiController.HideInteraction();
				Cursor.lockState = CursorLockMode.None;
				//Camera
				_virtualCamera.Follow = _machineFollow;

				_machine.enabled = true;
				_machine.WeaponController.enabled = true;
				_machine.GetComponent<MachineMovement>().enabled = true;

			}
			else
			{
				_machine.enabled = false;
				_machine.WeaponController.enabled = false;
				_machine.GetComponent<MachineMovement>().enabled = false;
				_player.transform.rotation = _originalPlayerRotation;
				_player.transform.parent = null;

				//Camera
				_virtualCamera.Follow = _playerFollow;
				Cursor.lockState = CursorLockMode.Locked;
			}
			Cursor.visible = machineMode;
		}
		#endregion

		#region Private Methods
		#endregion
	}

}