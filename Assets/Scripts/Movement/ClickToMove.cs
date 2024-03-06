using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AutoChessRPG
{
    public class ClickToMove : MonoBehaviour
    {
        private CharacterMovement characterMovement;
        private Camera cam;

        [SerializeField] private LayerMask layersToHit;

        private void Awake()
        {
            characterMovement = GetComponent<CharacterMovement>();
            cam = FindObjectOfType<Camera>();
        }

        public void OnClickToMove(InputAction.CallbackContext context)
        {
            if (context.started) characterMovement.SendTargetPosition(GetWorldPositionToMove(Mouse.current.position.value));
        }

        private Vector3 GetWorldPositionToMove(Vector3 mouseScreenPosition)
        {
            Ray mouseRay = cam.ScreenPointToRay(mouseScreenPosition);

            return !Physics.Raycast(mouseRay, out RaycastHit hit, 100, layersToHit) ? transform.position : hit.point;

        }

    }
}
