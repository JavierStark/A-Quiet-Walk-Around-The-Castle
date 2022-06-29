using System;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Player
{
    public class Lamp : MonoBehaviour
    {
        private Ray _ray;
        private RaycastHit _hitInfo;

        private Transform _parent;

        [SerializeField] private int rotationSpeed = 5;
        
        private void Start()
        {
            _parent = transform.parent;
            _ray = new Ray(_parent.position, _parent.forward);
        }

        private void Update()
        {   
            UpdateRay();
            Physics.Raycast(_ray, out _hitInfo);

            Quaternion targetRotation = Quaternion.LookRotation(_hitInfo.point - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        private void UpdateRay()
        {
            _ray.origin = _parent.position;
            _ray.direction = _parent.forward;
        }
    }
}
