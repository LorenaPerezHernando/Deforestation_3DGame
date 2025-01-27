using UnityEngine;
using System;
using Deforestation.Interaction;

namespace Deforestation.Recolectables
{
	public enum RecolectableType
	{
		SuperCrystal,
		HyperCrystal,
	}
	public class Recolectable : MonoBehaviour, IInteractable
	{
		#region Properties
		[field: SerializeField] public int Count { get; set; }
		[field: SerializeField] public RecolectableType Type { get; set; }

		#endregion

		#region Fields
		[SerializeField] private InteractableInfo _interactableInfo;
		#endregion

		#region Unity Callbacks
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
		#endregion

		#region Public Methods
		public InteractableInfo GetInfo()
		{
			_interactableInfo.Type = Type.ToString();
			return _interactableInfo;
		}

		public void Interact()
		{
			Destroy(gameObject);
		}
		#endregion

		#region Private Methods
		#endregion
	}
}
