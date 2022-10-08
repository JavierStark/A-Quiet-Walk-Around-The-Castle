using System;
using UnityEngine;
using Input = InputSystem.Input;
using Random = UnityEngine.Random;

namespace _Scripts.Player
{
    public class Lantern : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Transform _thisTransform;
        private Light _light;
        [SerializeField] private Input input;

        private float _timeTillTurningOff;
        [SerializeField] private float minTime;
        [SerializeField] private float maxTime;
        
        [SerializeField] private Transform playerCameraRoot;

        [SerializeField] private int rotationSpeed = 5;
        [SerializeField] private LayerMask raycastLayerMask;

        private void Awake()
        {
            _thisTransform = transform;
            _light = GetComponent<Light>();
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
            //RotateModel();
            UpdatePos();
            TurnOff();
            Interact();
        }

        private void Interact()
        {
            if (!input.lanternInteraction) return;
            
            TurnOn();
        }

        private void UpdateRay()
        {
            _ray.origin = _thisTransform.position;
            _ray.direction = _thisTransform.forward;
        }

        private void UpdatePos()
        {
            transform.position = playerCameraRoot.position;
        }

        private void RotateModel()
        {
            var model = transform.GetChild(0);
            Quaternion targetRotation = Quaternion.LookRotation(_hitInfo.point - model.position, _thisTransform.up);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void ResetTimer()
        {
            _timeTillTurningOff = Random.Range(minTime, maxTime);
        }
        private void TurnOn()
        {
            ResetTimer();
            _light.enabled = true;
        }
        private void TurnOff()
        {
            if (_timeTillTurningOff <= 0)
            {
                _light.enabled = false;
                return;
            }

            _timeTillTurningOff -= Time.deltaTime;
        }
    }
}
