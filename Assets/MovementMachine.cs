using Deforestation.Recolectables;
using Deforestation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deforestation.Machine
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementMachine : MonoBehaviour
    {


        [Header("Movimiento")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float jumpForce = 7f;

        [Header("Ground Check")]
        [SerializeField] private float groundCheckDistance = 1.5f;
        [SerializeField] private LayerMask groundLayer;
        private bool isGrounded;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            CheckGround();

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void FixedUpdate()
        {
            if (!isGrounded) return;

            float moveInput = Input.GetAxis("Vertical");     
            float rotateInput = Input.GetAxis("Horizontal"); 

            // Movimiento hacia adelante o atrás
            Vector3 move = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + move);

            // Rotación en Y
            Quaternion turn = Quaternion.Euler(0f, rotateInput * rotationSpeed * Time.fixedDeltaTime, 0f);
            _rb.MoveRotation(_rb.rotation * turn);
        }

        private void CheckGround()
        {
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);
            Debug.DrawRay(origin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
        }
    }
}
