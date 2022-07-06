using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

namespace Spider
{
    public class Spider : MonoBehaviour
    {
        [SerializeField] private float sphereCastRadius;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private float velocity;
        [SerializeField] private float threshold;
        private bool _firstTarget = true;
        
        private Web _web;
        private Vector3 _target = Vector3.zero;

        private Rigidbody _rigidbody;

        private List<Vector3> _randomPoints = new List<Vector3>();
        private Vector3 _direction;
        


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (_web == null)
            {
                var collidersDetected = Physics.OverlapSphere(transform.position, sphereCastRadius, layerMask);
                if (collidersDetected.Length == 0)
                {
                    SetGravityAndKinematic(true, false);
                    return;
                }
                
                _web = collidersDetected[0].gameObject.GetComponent<Web>();
                
                if (_web == null)
                {
                    SetGravityAndKinematic(true, false);
                    return;
                }
                
                transform.up = _web.GetNormal();
                SetGravityAndKinematic(false, true);
            }

            if (_target == Vector3.zero || Vector3.Distance(transform.position, _target) < threshold)
            {
                _target = _web.GetRandomPoint();
                _randomPoints.Add(_target);

                var model = transform.GetChild(0);
                var targetLocalPosition = model.InverseTransformPoint(_target);
                targetLocalPosition.y = 0;
                var targetToLookAt = model.TransformPoint(-targetLocalPosition);
                model.LookAt(targetToLookAt, transform.up);
                
            }

            _direction = (_target - transform.position).normalized;
            var currentVel = velocity * (_firstTarget ? 2 : 0);
            _firstTarget = true;
            transform.Translate(_direction * (currentVel * Time.deltaTime), Space.World);
        }

        private void SetGravityAndKinematic(bool gravity, bool kinematic)
        {
            _rigidbody.useGravity = gravity;
            _rigidbody.isKinematic = kinematic;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, sphereCastRadius);
            Gizmos.DrawSphere(_target, 0.06f);

            foreach (var point in _randomPoints)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point, 0.05f);
            }
            
            Gizmos.DrawRay(transform.position, _direction);
        }
    }
}
