using System;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class Lantern : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hitInfo;
        private Transform _thisTransform;
        
        [SerializeField] private Transform playerCameraRoot;

        [SerializeField] private int rotationSpeed = 5;
        [SerializeField] private LayerMask raycastLayerMask;
        
        private void Start()
        {
            _thisTransform = transform;
            _ray = new Ray(_thisTransform.position, _thisTransform.forward);
        }
        private void Update()
        {
            UpdateRay();
            Physics.Raycast(_ray, out _hitInfo, 20, raycastLayerMask);

            transform.rotation = Quaternion.Slerp(transform.rotation, playerCameraRoot.rotation, Time.deltaTime * rotationSpeed);
            RotateModel();
            UpdatePos();
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
    }
}
