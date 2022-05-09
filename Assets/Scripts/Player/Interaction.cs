using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;

namespace Player
{
    public class Interaction : MonoBehaviour
    {
        public float maxDistance;
        private Ray _ray;
        private RaycastHit _hitInfo;

        private Transform _cameraRoot;

        private PlayerInput _playerInput;
        private Input _input;

        private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

        private void Start()
        {
            _cameraRoot = transform.GetChild(0);

            _playerInput = GetComponent<PlayerInput>();
            _input = GetComponent<Input>();

            _ray = new Ray();
        }

        void Update()
        {
            _ray.origin = _cameraRoot.position;
            _ray.direction = _cameraRoot.forward;

            if (_input.interact)
            {
                Debug.Log("E key pressed");
                CastInteractionRayForward();

                IInteractable target = _hitInfo.transform?.GetComponent<IInteractable>();
                if (target != null)
                {
                    target.Interact();
                }
            }
            _input.interact = false;
        }

        void CastInteractionRayForward()
        {
            Physics.Raycast(_ray, out _hitInfo, maxDistance);
        }

        private void OnDrawGizmos()
        {
            if(_cameraRoot) Gizmos.DrawRay(_cameraRoot.position, _cameraRoot.forward*maxDistance);
        }
    }
}