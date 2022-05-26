using System;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Input = InputSystem.Input;

namespace Player
{
    public class Leaning : MonoBehaviour
    {
        private Input _input;
        [SerializeField] private Transform leanPivot;

        private float _lean = 0;
        [Range(0,90)] [SerializeField] private float maxLean;
        [SerializeField] private float leanPerSecond;

        private Vector3 _pivotRotation = new Vector3(0, 0, 0);
        
        void Start()
        {
            _input = GetComponent<Input>();
        }

        void Update()
        {
            _lean = _input.lean;

            Lean();
        }

        private void Lean()
        {
            Vector3 targetRotation = new Vector3(0, 0, _lean * maxLean);
            
            _pivotRotation = Vector3.Lerp(_pivotRotation, targetRotation, Time.deltaTime * leanPerSecond);
            
            leanPivot.localRotation = Quaternion.Euler(_pivotRotation);
        }
    }
}
