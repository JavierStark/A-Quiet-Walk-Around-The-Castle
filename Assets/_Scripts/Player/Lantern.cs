using System;
using UnityEngine;
using UnityEngine.Serialization;
using Input = InputSystem.Input;
using Random = UnityEngine.Random;

namespace _Scripts.Player
{
    public class Lantern : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Transform _thisTransform;
        private Transform model;
        [SerializeField] private new Light light;
        [SerializeField] private Input input;

        private float _timeTillTurningOff;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        
        [SerializeField] private Transform playerCameraRoot;

        [SerializeField] private int rotationSpeed = 5;
        [SerializeField] private float recuperationSpeed = 0.2f;
        [SerializeField] private LayerMask raycastLayerMask;

        private void Awake()
        {
            _thisTransform = transform;
            model = transform.GetChild(0);
        }

        private void Start()
        {
            _ray = new Ray(_thisTransform.position, _thisTransform.forward);
            ResetTimer();
        }
        private void Update()
        {
            UpdateRay();
            Physics.Raycast(_ray, out _hitInfo, 20, raycastLayerMask);

            transform.rotation = Quaternion.Slerp(transform.rotation, playerCameraRoot.rotation, Time.deltaTime * rotationSpeed);
            RotateModel();
            UpdatePos();
            TurnOff();
            Interact();
            SwapSide();
        }

        private void Interact()
        {
            if (!input.lampInteraction) return;
            
            TurnOn();
        }

        private void SwapSide()
        {
            if (!input.lampSwap) return;
            input.lampSwap = false;

            var modelPosition = model.localPosition;
            model.localPosition = new Vector3(-modelPosition.x, modelPosition.y, modelPosition.z);
        }

        private void UpdateRay()
        {
            _ray.origin = _thisTransform.position;
            _ray.direction = _thisTransform.forward;
        }

        private void UpdatePos()
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCameraRoot.position, Time.deltaTime * recuperationSpeed);
        }

        private void RotateModel()
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(_hitInfo.point - model.position, _thisTransform.up); 
            
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void ResetTimer()
        {
            _timeTillTurningOff = Random.Range(minTime, maxTime);
        }
        private void TurnOn()
        {
            ResetTimer();
            light.enabled = true;
        }
        private void TurnOff()
        {
            if (_timeTillTurningOff <= 0)
            {
                light.enabled = false;
                return;
            }

            _timeTillTurningOff -= Time.deltaTime;
        }
    }
}
