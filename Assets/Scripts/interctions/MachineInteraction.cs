using UnityEngine;
using System;
using System.Collections;
namespace Deforestation.Interaction
{
	public enum MachineInteractionType
	{
		Door,
		Stairs,
		Machine
	}
	public class MachineInteraction : MonoBehaviour, IInteractable
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] protected MachineInteractionType _type;
		[SerializeField] protected Transform _target;

		[SerializeField] protected InteractableInfo _interactableInfo;

		[SerializeField] protected Transform _initialPosDoor;


        #endregion

        

        #region Public Methods
        public InteractableInfo GetInfo()
		{
			_interactableInfo.Type = _type.ToString();
			return _interactableInfo;
		}

		public virtual void Interact()
		{
			if (_type == MachineInteractionType.Door)
			{
				
				//Move Door
				
				
				transform.position = _target.position;
				StartCoroutine(DoorToInitialPos());
			}
			if (_type == MachineInteractionType.Stairs)
			{
				//Teleport Player
				GameController.Instance.TeleportPlayer(_target.position);
			}
			if (_type == MachineInteractionType.Machine)
			{
				GameController.Instance.MachineMode(true);
			}
		}

		IEnumerator DoorToInitialPos()
		{
			
			_initialPosDoor.GetComponent<Collider>().enabled = false;
			yield return new WaitForSeconds(5);
			transform.position = _initialPosDoor.position;
            _initialPosDoor.GetComponent<Collider>().enabled = true;
          ;
        }

		#endregion
	}

	

}