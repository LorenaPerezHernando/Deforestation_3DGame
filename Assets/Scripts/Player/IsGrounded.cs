using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IsGrounded : MonoBehaviour
{
    public bool Grounded;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Grounded = false;
        }
        else
        {
            Grounded = true;
        }
    }
}

