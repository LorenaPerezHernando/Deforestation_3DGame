using System;
using UnityEngine;
using UnityEngine.AI;

namespace Deforestation.Dinosaurus
{

	public class Dinosaur : MonoBehaviour
	{
		#region Fields
		protected Animator _anim;
		protected NavMeshAgent _agent;
		protected HealthSystem _health;
		
		#endregion

		#region Properties
		public HealthSystem Health => _health;
		#endregion

		#region Unity Callbacks	
		private void Awake()
		{
			_health = GetComponent<HealthSystem>();
			_anim = GetComponent<Animator>();
			_agent = GetComponent<NavMeshAgent>();

			_health.OnDeath += Die;
		}

		private void Die()
		{
			_anim.SetTrigger("Die");
			Destroy(_agent);
			Destroy(this);
		}
		#endregion

		#region Private Methods
		#endregion

		#region Public Methods
		#endregion

	}

}