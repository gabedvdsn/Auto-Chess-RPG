using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AutoChessRPG
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController controller;
        
        private Vector3 targetPosition;
        
        private Vector3 movementDirection;
        [SerializeField] private Vector3 lookDirection;
        
        private float turnSmoothTime;
        private float turnSmoothVelocity;

        private float lookCoef;
        
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] [Range(0, 90)] private float allowableRangeForMovement;
        [SerializeField] private float degreesToTarget;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();

            lookDirection = transform.forward;
        }

        public void SendTargetPosition(Vector3 _targetPosition)
        {
            if (_targetPosition == transform.position) return;

            targetPosition = _targetPosition;
            
            lookDirection = (targetPosition - transform.position).normalized;
            lookDirection.y = 0f;
        }

        public float GetDegreesToTarget() => degreesToTarget;

        public void DoCharacterMovement()
        {
            Move();
            LookAt();
        }
        
        private void LookAt()
        {
            degreesToTarget = Vector3.Angle(transform.forward, lookDirection);
            
            Quaternion rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotationSpeed * Time.deltaTime);
            rotation.x = 0;
            rotation.z = 0;

            transform.rotation = rotation;
        }
        
        private void Move()
        {
            movementDirection = (targetPosition - transform.position).normalized;

            if (!(movementDirection.magnitude > 0.1f)) return;

            if (!(degreesToTarget < allowableRangeForMovement)) return;
            
            // float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg - 90f;
            // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(movementDirection * (moveSpeed * Time.deltaTime * Mathf.Clamp01(1 - degreesToTarget / allowableRangeForMovement)));
        }

    }
}
