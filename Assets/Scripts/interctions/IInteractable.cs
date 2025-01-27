namespace Deforestation.Interaction
{
	public interface IInteractable
	{
		public void Interact();
		public InteractableInfo GetInfo();
	}

	//MOver a otro archivo
	[System.Serializable]
	public class InteractableInfo
	{
		public string Action;
		public string Type;
	}

}