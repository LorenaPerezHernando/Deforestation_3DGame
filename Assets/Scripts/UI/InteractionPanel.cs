using UnityEngine;
using TMPro;
using System;

namespace Deforestation.UI
{

	public class InteractionPanel : MonoBehaviour
	{
		#region Properties
		#endregion

		#region Fields
		[SerializeField] private TextMeshProUGUI _textPanel;
		#endregion

		#region Unity Callbacks
		// Start is called before the first frame update
		void Start()
		{
			gameObject.SetActive(false);
		}


		#endregion

		#region Public Methods
		public void Show(string message)
		{
			gameObject.SetActive(true);
			_textPanel.text = message;
		}

		internal void Hide()
		{
			gameObject.SetActive(false);

		}
		#endregion

		#region Private Methods
		#endregion
	}
}
