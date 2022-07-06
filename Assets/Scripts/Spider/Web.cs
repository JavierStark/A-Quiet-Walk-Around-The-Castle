using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Spider
{
    public class Web : MonoBehaviour
    {
        [SerializeField] private float heightOverPlane;

        [SerializeField] private Transform[] triangleVertices;
        private Vector3[] _triangleSides = new Vector3[3];
        

        private bool IsTriangle => triangleVertices.Length == 3;

        private void Start()
        {
            SetVectors();
        }

        private void SetVectors()
        {
            _triangleSides[0] = triangleVertices[1].position - triangleVertices[0].position;
            _triangleSides[1] = triangleVertices[2].position - triangleVertices[0].position;
            _triangleSides[2] = triangleVertices[1].position - triangleVertices[2].position;
        }

        public Vector3 GetRandomPoint()
        {
            float randomA = Random.value;
            float randomB = Random.value;
            
            if ((randomA + randomB) > 1)
            {
                randomA = 1 - randomA;
                randomB = 1 - randomB;
            }
            
            return (_triangleSides[0] * randomA + _triangleSides[1] * randomB ) + triangleVertices[0].position;
        }

        public Vector3 GetNormal(){
            return Vector3.Cross(_triangleSides[0], _triangleSides[1]).normalized;
        }

        private void OnDrawGizmos()
        {
            if (!IsTriangle) return;
            SetVectors();

            foreach (var vertex in triangleVertices)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(vertex.position, 0.1f);
            }
            
            Gizmos.DrawLine(triangleVertices[0].position, triangleVertices[1].position);
            Gizmos.DrawLine(triangleVertices[1].position, triangleVertices[2].position);
            Gizmos.DrawLine(triangleVertices[2].position, triangleVertices[0].position);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(triangleVertices[0].position, GetNormal());
        }

    }
}