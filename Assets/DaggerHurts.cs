using System.Collections;
using System.Collections.Generic;
using Deforestation;
using UnityEngine;

public class DaggerHurts : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
                _anim.SetTrigger("Attack");
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dinosaur"))
        {
            HealthSystem dinosaurHealth = other.gameObject.GetComponent<HealthSystem>();
            print("Hit dinosaur");
            if (dinosaurHealth != null)
                dinosaurHealth.TakeDamage(20f);

        }
    }
}
