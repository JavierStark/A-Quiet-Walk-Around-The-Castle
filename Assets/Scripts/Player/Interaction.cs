using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;
using UnityEngine.UI;
using Input = InputSystem.Input;

namespace Player
{
    public class Interaction : MonoBehaviour
    {
        public float maxDistance;
        private Ray _ray;
        private RaycastHit _hitInfo;

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Image pointerUI;

        private PlayerInput _playerInput;
        private Input _input;

        private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

        private void Start()
        {
            _input = GetComponent<Input>();

            _ray = new Ray();
        }

        void Update()
        {
            Interact();
            _input.interact = false;
        }

        private void Interact()
        {
            _ray.origin = cameraTransform.position;
            _ray.direction = cameraTransform.forward;
            bool hasHit = CastInteractionRayForward();
            
            ChangePointer(hasHit);

            if(!_input.interact || !hasHit) return;

            IInteractable target = _hitInfo.transform?.GetComponent<IInteractable>();
            
            if (target != null)
            {
                target.Interact(this.gameObject);
            }            
        }

        private void ChangePointer(bool interactableDetected)
        {
            if (!interactableDetected)
            {
                pointerUI.color = Color.black;
                return;
            }

            Color color;
            switch (_hitInfo.collider.gameObject.tag)
            {
                case "Pickable":
                {
                    color = Color.red;
                    break;
                }
                case "Door":
                {
                    color = Color.green;
                    break;
                }
                case "Trigger":
                {
                    color = Color.yellow;
                    break;
                }
                default:
                {
                    color = Color.black;
                    break;
                }
            }

            pointerUI.color = color;
        }

        private bool CastInteractionRayForward()
        {
           return Physics.Raycast(_ray, out _hitInfo, maxDistance);
        }

        private void OnDrawGizmos()
        {
            if(cameraTransform) Gizmos.DrawRay(cameraTransform.position, cameraTransform.forward*maxDistance);
        }
    }
}