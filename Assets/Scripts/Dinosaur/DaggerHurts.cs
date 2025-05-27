using System.Collections;
using System.Collections.Generic;
using Deforestation;
using UnityEngine;
using System;

public class DaggerHurts : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private ParticleSystem _bloodParticle;
    private ParticleSystem _stegasaurusBloodParticle;


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
            print("Trigger with Dino");
            HealthSystem stegasaurusHealth = other.gameObject.GetComponentInParent<HealthSystem>();
            HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();

            print("Hit dinosaur");
            _bloodParticle = other.GetComponent<ParticleSystem>();
            _stegasaurusBloodParticle = other.GetComponentInParent<ParticleSystem>();
            
            if (stegasaurusHealth != null)
            {
                print("Hit Stega");
                
                stegasaurusHealth.TakeDamage(5f);
                _stegasaurusBloodParticle?.Play();
            }
            if(healthSystem != null)
            {
                print("Hit Dino");
                healthSystem.TakeDamage(20f);
                _bloodParticle.Play();
            }

        }
    }
}
