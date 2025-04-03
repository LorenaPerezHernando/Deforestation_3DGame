using UnityEngine;
using System;
using Deforestation.Interaction;
using System.Linq;

namespace Deforestation.Recolectables
{
	public enum RecolectableType
	{
		SuperCrystal,
		HyperCrystal,
		MegaCrystal,

		TowerPart,
		Dagger,
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

		#region Public Methods
		public InteractableInfo GetInfo()
		{
			_interactableInfo.Type = Type.ToString();
			
			return _interactableInfo;

			

		}

		public void Interact()
		{
            if (_interactableInfo.Type == "Dagger")
            {
				GetComponent<InteractDagger>().DaggerActives();
            }

		

			
            Destroy(gameObject);
		}
		#endregion

		#region Private Methods
		#endregion
	}
}
