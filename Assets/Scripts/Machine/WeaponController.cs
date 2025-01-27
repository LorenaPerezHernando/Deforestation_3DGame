using UnityEngine;
using System;
namespace Deforestation.Machine.Weapon
{

	public class WeaponController : MonoBehaviour
	{
		#region Properties
		public Action OnMachineShoot;
		#endregion

		#region Fields
		[SerializeField] private Transform _towerWeapon;
		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private float _speedRotation = 5f;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private GameObject _smokeShoot1;
		[SerializeField] private GameObject _smokeShoot2;
		#endregion

		#region Unity Callbacks
		private void Awake()
		{
		}

		void Update()
		{
			//Si no estamos conduciendo no controlamos esto. 
			if (!GameController.Instance.MachineModeOn)
				return;

			Ray ray = GameController.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				Vector3 direccion = hit.point - transform.position;
				direccion.y = 0; // Mantener la rotación solo en el eje Y

				Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
				_towerWeapon.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, _speedRotation * Time.deltaTime);
			}

			if (Input.GetMouseButtonUp(0) && GameController.Instance.MachineModeOn && GameController.Instance.Inventory.UseResource(Recolectables.RecolectableType.SuperCrystal))
			{
				Shoot(hit.point);
			}
		}

		public void Shoot(Vector3 lookAtPoint)
		{
			transform.LookAt(lookAtPoint);
			Instantiate(_bulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
			_smokeShoot1.SetActive(true);
			_smokeShoot2.SetActive(true);
			OnMachineShoot?.Invoke();
		}
		#endregion

		#region Public Methods
		#endregion

		#region Private Methods
		#endregion
	}

}