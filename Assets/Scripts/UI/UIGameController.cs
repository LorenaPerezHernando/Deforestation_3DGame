using UnityEngine;
using TMPro;
using Deforestation.Recolectables;
using System;
using Deforestation.Interaction;
using UnityEngine.UI;
using UnityEngine.Audio;
using Deforestation.Tower;
using System.Collections;

namespace Deforestation.UI
{
	public class UIGameController : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		private Inventory _inventory => GameController.Instance.Inventory;		
		private TowerInteraction _towerInteraction => GameController.Instance.TowerInteraction;
		private InteractionSystem _interactionSystem => GameController.Instance.InteractionSystem;
        

		[Header("Settings")]
		[SerializeField] private AudioMixer _mixer;
		[SerializeField] private Button _settingsButton;
		[SerializeField] private GameObject _settingsPanel;
		[SerializeField] private Slider _musicSlider;
		[SerializeField] private Slider _fxSlider;

		[Header("Inventory")]
		[SerializeField] private TextMeshProUGUI _crystal1Text;
		[SerializeField] private TextMeshProUGUI _crystal2Text;
        [SerializeField] private TextMeshProUGUI _crystal3Text;
		[SerializeField] private TextMeshProUGUI _notEnoughCrystals;
		[SerializeField] private TextMeshProUGUI _salirMaquina;
        [Header("Interaction")]
		[SerializeField] private InteractionPanel _interactionPanel;
		[Header("Live")]
		[SerializeField] private Slider _machineSlider;
		[SerializeField] private Slider _playerSlider;

		[Header("Tower")]
		[SerializeField] private int _crystalsToGather = 10;
		[SerializeField] private GameObject _dialoguePanel;
		[SerializeField] private GameObject _fixTowerDialogue;
		[SerializeField] private GameObject _repairedTowerDialogue;
		[SerializeField] private TextMeshProUGUI _towerPartText;
		[Header("Hurt")]
		[SerializeField] private TextMeshProUGUI _hurtDialogue;

		private bool _settingsOn = false;
		private
		#endregion

		#region Unity Callbacks
		void Start()
		{
            GameController.Instance.TowerInteraction.OnRepairTower += RepairedTower;
            _settingsPanel.SetActive(false);
			_fixTowerDialogue.SetActive(false);
			NotEnoughCrystals();

			//My Events
			_inventory.OnInventoryUpdated += UpdateUIInventory;
			_interactionSystem.OnShowInteraction += ShowInteraction;
			_interactionSystem.OnHideInteraction += HideInteraction;
			//Settings events
			_settingsButton.onClick.AddListener(SwitchSettings);
			_musicSlider.onValueChanged.AddListener(MusicVolumeChange);
			_fxSlider.onValueChanged.AddListener(FXVolumeChange);
		}	
		

		private void SwitchSettings()
		{
			_settingsOn = !_settingsOn;
			_settingsPanel.SetActive(_settingsOn);
		}

		internal void UpdateMachineHealth(float value)
		{
			_machineSlider.value = value;
		}

		internal void UpdatePlayerHealth(float value)
		{
			_playerSlider.value = value;
		}

		#endregion

		#region Public Methods
		public void ShowInteraction(string message)
		{
			_interactionPanel.Show(message);
		}
		public void HideInteraction()
		{
			_interactionPanel.Hide();

		}
		public void NotEnoughCrystals()
		{
			StartCoroutine(NotEnoughCrystalsCoroutine());
		}
		public IEnumerator NotEnoughCrystalsCoroutine()
		{
            _notEnoughCrystals.gameObject.SetActive(true);
			StartCoroutine(TextSalirMaquinaCoroutine());
            yield return new WaitForSeconds(1);

            _notEnoughCrystals.gameObject.SetActive(false);
        }
		public void TextSalirMaquina()
		{
			StartCoroutine(TextSalirMaquinaCoroutine());
		}
		private IEnumerator TextSalirMaquinaCoroutine()
		{
            _salirMaquina.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            _salirMaquina.gameObject.SetActive(false);
        }
		public void HurtText()
		{
			StartCoroutine(HurtTextCoroutine());
		}
		private IEnumerator HurtTextCoroutine()
		{
			//_dialoguePanel.gameObject.SetActive(true);
			_hurtDialogue.gameObject.SetActive(true);
			yield return new WaitForSeconds(1);
			//_dialoguePanel.gameObject.SetActive(false);
			_hurtDialogue.gameObject.SetActive(false);

        }


        #endregion

        #region Private Methods
        private void UpdateUIInventory()
		{
			if (_inventory.InventoryStack.ContainsKey(RecolectableType.SuperCrystal))
				_crystal1Text.text = _inventory.InventoryStack[RecolectableType.SuperCrystal].ToString();
			else
				_crystal1Text.text = "0";
			if (_inventory.InventoryStack.ContainsKey(RecolectableType.HyperCrystal))
				_crystal2Text.text = _inventory.InventoryStack[RecolectableType.HyperCrystal].ToString();
			else
				_crystal2Text.text = "0";
			if (_inventory.InventoryStack.ContainsKey(RecolectableType.MegaCrystal))
				_crystal3Text.text = _inventory.InventoryStack[RecolectableType.MegaCrystal].ToString();
			else
				_crystal3Text.text = "0";
			if (_inventory.InventoryStack.ContainsKey(RecolectableType.TowerPart))
				_towerPartText.text = _inventory.InventoryStack[RecolectableType.TowerPart].ToString() + " / " + _crystalsToGather;
			else
				_towerPartText.text = "0/10";
            if (_inventory.InventoryStack.TryGetValue(RecolectableType.TowerPart, out int cantidad) && cantidad >= _crystalsToGather)
            {
                _towerInteraction.isRepaired = true;
                _dialoguePanel.SetActive(true);
                _fixTowerDialogue.SetActive(true);
            }
        }
        

        private void RepairedTower()
		{
			_repairedTowerDialogue.SetActive(true );
		}

		private void FXVolumeChange(float value)
		{
			_mixer.SetFloat("FXVolume", Mathf.Lerp(-60f, 0f, value));
		}

		private void MusicVolumeChange(float value)
		{
			_mixer.SetFloat("MusicVolume", Mathf.Lerp(-60f, 0f, value));

		}
		#endregion
	}

}