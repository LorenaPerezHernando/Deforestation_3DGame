using UnityEngine;
using System;
using System.Collections.Generic;

namespace Deforestation.Recolectables
{
	public class Inventory : MonoBehaviour
	{
		#region Properties

		public Dictionary<RecolectableType, int> InventoryStack = new Dictionary<RecolectableType, int>();
		public Action OnInventoryUpdated;
		#endregion

		#region Public Methods
		public void AddRecolectable(RecolectableType type, int count)
		{
			if (InventoryStack.ContainsKey(type))
				InventoryStack[type] += count;
			else
				InventoryStack.Add(type, count);
			OnInventoryUpdated?.Invoke();			

		}
		public bool UseResource (RecolectableType type, int count = 1)
		{
			if (HasResource(type, count))
			{
				InventoryStack[type] -= count;
				OnInventoryUpdated?.Invoke();
				return true;
			}

			return false;
		}

		public bool HasResource(RecolectableType type, int count = 1)
		{
			if (InventoryStack.ContainsKey(type) && InventoryStack[type] >= count)
				return true;

			return false;
		}
		public bool NoResource(RecolectableType type)
		{
			if(InventoryStack.ContainsKey(type) && InventoryStack[type] == 0)
                return true;

            return false;
        }
        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider collision)
        {
			if (collision.gameObject.tag.Equals("Crystal"))
			{
				Recolectable item = collision.gameObject.GetComponent<Recolectable>();
				AddRecolectable(item.Type, item.Count);
				Destroy(collision.gameObject);
				Debug.Log("Recolected");
			}

			if (collision.gameObject.tag.Equals("TowerPart"))
			{
                Recolectable itemPart = collision.gameObject.GetComponent<Recolectable>();
                AddRecolectable(itemPart.Type, itemPart.Count);
                Destroy(collision.gameObject);
                Debug.Log("Recolected");

				
            }
        }

		internal void RestartCrystals()
		{
            SetToZeroIfExists(RecolectableType.SuperCrystal);
            SetToZeroIfExists(RecolectableType.HyperCrystal);
            SetToZeroIfExists(RecolectableType.MegaCrystal);
            SetToZeroIfExists(RecolectableType.TowerPart);

            OnInventoryUpdated?.Invoke();
		}

		private void SetToZeroIfExists(RecolectableType type) //Por si ya estan a 0
		{
			if(!InventoryStack.ContainsKey(type))
				InventoryStack[type] = 0;
		}

		
        #endregion
    }
}
