using UnityEngine;
using Deforestation.UI;
using Deforestation.Recolectables;
using System;

namespace Deforestation.Interaction
{
	public class InteractionSystem : MonoBehaviour
	{
		#region Properties
		public event Action<string> OnShowInteraction;
		public event Action OnHideInteraction;
		#endregion

		#region Fields
		[SerializeField] float _widthDetector = 1;
		[SerializeField] float _distanceDetector = 5;
		[SerializeField] Inventory _inventory;
		private bool _interactebleDetected = false;
		private IInteractable _currentInteraction;
		#endregion

		#region Unity Callbacks		
		private void Update()
		{
			if (_interactebleDetected && Input.GetKeyUp(KeyCode.E))
			{
				//Si la interaccion es Recolectable, la guardamos como tal
				if (_currentInteraction is Recolectable recolectable)
				{
					_inventory.AddRecolectable(recolectable.Type, recolectable.Count);
					recolectable.Interact();
				}
				//Mahine Interaction
				if (_currentInteraction is MachineInteraction machineInteraction)
				{
					machineInteraction.Interact();
				}
			}
		}
		void FixedUpdate()
		{
			RaycastHit hit;
			if (Physics.SphereCast(Camera.main.transform.position, 0.5f, Camera.main.transform.forward, out hit, 5))
			{
				//print(hit.collider.name); //Para saber que estamos detectando para depurar si alguna deteccion falla.
				IInteractable interaction = hit.collider.GetComponent<IInteractable>();
				if (interaction != null)
				{
					InteractableInfo info = interaction.GetInfo();
					OnShowInteraction.Invoke("E - To " + info.Action + " " + info.Type);
					_interactebleDetected = true;
					_currentInteraction = interaction;
					return;
				}
			}
			_interactebleDetected = false;
			OnHideInteraction.Invoke();
			
		}
		#endregion

		#region Public Methods
		#endregion

		#region Private Methods
		#endregion

		void OnDrawGizmos()
		{
			if (Camera.main != null)
			{
				Vector3 startPosition = Camera.main.transform.position;
				Vector3 direction = Camera.main.transform.forward;
				float radius = 0.5f;
				float distance = 5f;

				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere(startPosition, radius);
				Gizmos.DrawWireSphere(startPosition + direction * distance, radius);
				Gizmos.DrawLine(startPosition, startPosition + direction * distance);
			}
		}
	}
}
