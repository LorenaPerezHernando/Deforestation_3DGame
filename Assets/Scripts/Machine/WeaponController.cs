using UnityEngine;
using System;
using Deforestation.Dinosaurus;
using static UnityEngine.UI.GridLayoutGroup;
namespace Deforestation.Machine.Weapon
{

	public class WeaponController : MonoBehaviour
	{
		#region Properties
		public Action OnMachineShoot;
		public Action OnNoCrystals;
		#endregion

		#region Fields
		[SerializeField] private Recolectables.RecolectableType crystalNeeded;
		[SerializeField] private Transform _towerWeapon;
		[SerializeField] private Transform _spawnPoint;
		[SerializeField] private float _speedRotation = 5f;
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private Bullet _blackBulletPrefab;
		[SerializeField] private GameObject _smokeShoot1;
		[SerializeField] private GameObject _smokeShoot2;
		[SerializeField] private GameObject _machine; //Para que no me de la bala
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

			if (Physics.Raycast(ray, out hit)) //LayerMask
			{
				Vector3 direccion = hit.point - transform.position;
				direccion.y = 0; // Mantener la rotación solo en el eje Y

				Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
				_towerWeapon.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, _speedRotation * Time.deltaTime);
			}

			//Forma simple si solo voy a poner 1 tipo de roca
			//if (Input.GetMouseButtonUp(0) && GameController.Instance.MachineModeOn && GameController.Instance.Inventory.UseResource(Recolectables.RecolectableType.SuperCrystal))
			//{
			//	Shoot(hit.point);
			//}
			if (Input.GetMouseButtonUp(0) && GameController.Instance.MachineModeOn)
			{
				if (Physics.Raycast(ray, out hit))
				{
					Rock rock = hit.collider.GetComponent<Rock>();
					if (rock != null)
					{

						crystalNeeded = rock.requiredCrystal;
						// Solo dispara si tienes ese cristal
						if (GameController.Instance.Inventory.UseResource(crystalNeeded))
						{
							Shoot(hit.point);
						}
						else
						{
							Debug.Log("Not enough Crystals to shoot Rock ");
							OnNoCrystals?.Invoke();
						}
					}
					if (hit.collider.CompareTag("Dinosaur"))
					{
						HealthSystem health = hit.collider.GetComponentInParent<HealthSystem>();
						if (health != null && GameController.Instance.Inventory.UseResource(Recolectables.RecolectableType.SuperCrystal))
						{
							Shoot(hit.point);
						}
						else
						{
							Debug.Log("No Crystals to shoot Dino");
							OnNoCrystals?.Invoke();
						}
						
					}
					


				}
			}



		}

		public void Shoot(Vector3 lookAtPoint)
		{
			transform.LookAt(lookAtPoint);

			////Cristales == Diferente particula
			if (crystalNeeded == Recolectables.RecolectableType.MegaCrystal)
				Instantiate(_blackBulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
			else
				Instantiate(_bulletPrefab, _spawnPoint.transform.position, _spawnPoint.transform.rotation);
           

            _smokeShoot1.SetActive(true);
			_smokeShoot2.SetActive(true);


			OnMachineShoot?.Invoke();
		}
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        public void AvoidMyColliders(GameObject machine)
        {
            _machine = machine;
            Collider bulletCollider = GetComponent<Collider>();
            foreach (Collider ownerCollider in _machine.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(bulletCollider, ownerCollider);
            }
        }

    }
    #endregion
}

	
