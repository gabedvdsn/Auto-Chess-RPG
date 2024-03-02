using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AutoChessRPG
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody rb;

        private bool hasTargetPosition;
        private Vector3 targetPosition;

        private readonly float distanceDampenerRange = .65f;
        private float distanceToTarget;
        
        private Vector3 movementDirection;
        private Vector3 lookDirection;
        
        private float turnSmoothTime;
        private float turnSmoothVelocity;
        
        private float moveSpeed;
        private float rotationSpeed;
        private float allowableRangeForMovement;
        
        private float degreesToTarget;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            lookDirection = transform.forward;
        }

        public void Initialize(float _moveSpeed, float _rotationSpeed, float _allowableRangeForMovement)
        {
            moveSpeed = _moveSpeed;
            rotationSpeed = _rotationSpeed;
            allowableRangeForMovement = _allowableRangeForMovement;
        }

        public void SendTargetPosition(Vector3 _targetPosition)
        {
            Debug.Log($"Moving to {_targetPosition}");
            if (_targetPosition == transform.position) return;

            targetPosition = _targetPosition;
            targetPosition.y = transform.position.y;
            
            lookDirection = (targetPosition - transform.position).normalized;
            lookDirection.y = 0f;
            
            hasTargetPosition = true;
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
            if (!hasTargetPosition) return;
            
            movementDirection = (targetPosition - transform.position).normalized;
            distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget < .1)
            {
                hasTargetPosition = false;
                SetZeroVelocity();
                return;
            }

            if (!(movementDirection.magnitude > 0.1f) || !(degreesToTarget < allowableRangeForMovement))
            {
                SetZeroVelocity();
                return;
            }

            rb.velocity = movementDirection * (moveSpeed * RotationSpeedDampener() * DistanceToTargetSpeedDampener());
        }

        private float RotationSpeedDampener() => Mathf.Clamp01(1 - degreesToTarget / allowableRangeForMovement);

        private float DistanceToTargetSpeedDampener() => distanceToTarget < distanceDampenerRange ? Mathf.Lerp(0, 1, distanceToTarget / distanceDampenerRange) : 1f;

        private void SetZeroVelocity()
        {
            rb.velocity = Vector3.zero;
        }

    }
}
