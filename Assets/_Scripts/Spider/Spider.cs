using System;
using System.Collections.Generic;
using _Scripts.Spider;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

namespace Spider
{
    public class Spider : MonoBehaviour
    {
        [SerializeField] private float sphereCastRadius;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField] private float velocity;
        [SerializeField] private float threshold;
        private bool _firstMove = true;
        
        [SerializeField] private float minDelay;
        [SerializeField] private float maxDelay;
        private float _timer;

        private Web _web;
        private Vector3 _target = Vector3.zero;
        private Transform _model;

        private Rigidbody _rigidbody;

        private List<Vector3> _randomPoints = new List<Vector3>();
        
        private Vector3 _direction;
        private Vector3 _targetToLookAt;
        


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _model = transform.GetChild(0);
        }

        void Update()
        {
            if(WebMovement()) return;
            FloorState();
        }

        private void FloorState()
        {
            transform.localRotation = Quaternion.Euler(180, 0, 0);
        }

        private bool WebMovement()
        {
            if (_web == null)
            {
                var collidersDetected = Physics.OverlapSphere(transform.position, sphereCastRadius, layerMask);
                if (collidersDetected.Length == 0)
                {
                    SetGravityAndKinematic(true, false);
                    return false;
                }
                
                _web = collidersDetected[0].gameObject.GetComponent<Web>();
                
                if (_web == null)
                {
                    SetGravityAndKinematic(true, false);
                    return false;
                }
                
                transform.up = _web.GetNormal();
                SetGravityAndKinematic(false, true);
            }

            if (_target == Vector3.zero || Vector3.Distance(transform.position, _target) < threshold)
            {
                _target = _web.GetRandomPoint();
                _randomPoints.Add(_target);

                var targetLocalPosition = _model.InverseTransformPoint(_target);
                targetLocalPosition.y = 0;
                _targetToLookAt = _model.TransformPoint(-targetLocalPosition);

                _timer = Random.Range(minDelay, maxDelay);
            }

            _timer -= Time.deltaTime;
            if (_timer >= 0) return true;
            _model.LookAt(_targetToLookAt, transform.up);


            _direction = (_target - transform.position).normalized;
            var currentVel = velocity * (_firstMove ? 2 : 0);
            _firstMove = true;
            transform.Translate(_direction * (currentVel * Time.deltaTime), Space.World);

            return true;
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
