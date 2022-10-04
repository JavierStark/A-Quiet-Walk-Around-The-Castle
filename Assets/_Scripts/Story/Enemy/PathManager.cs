using System;
using System.Collections;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;

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
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            model = GetComponentInChildren<MeshRenderer>().gameObject;
            _navMeshAgent.enabled = false;
            model.SetActive(false);
        }
        public void FollowPath(int pathIndex)
        {
            StartCoroutine(nameof(FollowPathCoroutine), pathIndex);
        }
        
        private IEnumerator FollowPathCoroutine(int pathIndex)
        {
            Path currentPath = paths[pathIndex];
            Transform[] currentPathTransform = currentPath.GetPath();
            transform.position = NormalizeHeight(currentPathTransform[0].position);
            model.SetActive(true);
            _navMeshAgent.enabled = true;
            

            _target = currentPathTransform[1];
            _target.position = NormalizeHeight(_target.position);
            _navMeshAgent.SetDestination(_target.position);
            yield return new WaitUntil(() => 
                Vector3.Distance(transform.position, currentPathTransform[1].position) < distanceToTarget);
            
            model.SetActive(false);
            _target = null;
            _navMeshAgent.enabled = false;
            
            currentPath.PathFinished();
        }

        private Vector3 NormalizeHeight(Vector3 v)
        {
            return new Vector3(v.x, transform.position.y, v.z);
        }

        
    }
}
