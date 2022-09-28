using System;
using System.Collections;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace _Scripts.Story.Enemy
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private Path[] paths;

        [SerializeField] private float unitsPerSecond;
        [SerializeField] private float distanceToTarget;

        private GameObject model;

        private Transform _target;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            model = GetComponentInChildren<MeshRenderer>().gameObject;
            model.SetActive(false);
        }

        private void Update()
        {
            if (!_target) return;
            Move();
        }

        private void Move()
        {
            Vector3 position = transform.position;
            
            Vector3 direction = _target.position - position;
            direction = direction.normalized;

            float magnitude = Time.deltaTime * unitsPerSecond;

            
            
            _rigidbody.MovePosition(position + direction*magnitude);
        }

        public void FollowPath(int pathIndex)
        {
            StartCoroutine(nameof(FollowPathCoroutine), pathIndex);
        }
        
        private IEnumerator FollowPathCoroutine(int pathIndex)
        {
            Debug.Log("Path: " + pathIndex);
            Transform[] currentPath = paths[pathIndex].GetPath();
            Debug.Log(currentPath.Length);
            transform.position = NormalizeHeight(currentPath[0].position);
            model.SetActive(true);

            for (int i = 1; i < currentPath.Length; i++)
            {
                _target = currentPath[i];
                _target.position = NormalizeHeight(_target.position);
                SetRotation();
                yield return new WaitUntil(() => 
                    Vector3.Distance(transform.position, currentPath[i].position) < distanceToTarget);
            }
            
            model.SetActive(false);
            _target = null;
        }

        private void SetRotation()
        {
            transform.forward = (_target.position - transform.position).normalized;
        }

        private Vector3 NormalizeHeight(Vector3 v)
        {
            return new Vector3(v.x, transform.position.y, v.z);
        }

        
    }
}
