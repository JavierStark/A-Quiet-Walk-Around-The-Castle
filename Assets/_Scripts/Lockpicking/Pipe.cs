using System;
using System.Collections;
using UnityEngine;
using InputSystem;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine.ProBuilder;
using Random = UnityEngine.Random;

namespace Lockpicking
{
    [RequireComponent(typeof(BoxCollider))]
    public class Pipe : MonoBehaviour
    {
        [SerializeField] private InputSystem.Input input;
        private SpiderMinigame _minigameManager;
        private BoxCollider _collider;

        [SerializeField] private float rotationVelocity = 0.5f;
        [SerializeField] private float moveVelocity = 1f;

        [SerializeField] private GameObject pipeSegment;
        [SerializeField] private GameObject pipeSegmentWithPin;
        [SerializeField] private GameObject pipeFinalWall;


        private bool _minigameActive = false;
        private bool _pipeGenerated = false;
        private int _pipeLength = 8;
        private int _initialPipeSegments = 2;
        private int _pinAmount = 0;
        private int _pinsTriggered = 0;
        private int _currentPipePos;

        private float _movementCounter = 0;

        private IEnumerator pipeGeneratorIEnumerator;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        void Update()
        {
            if (!_pipeGenerated || !_minigameActive) return;
            RotatePipe();
            MovePipe();
        }

        public void Setup(int difficulty, SpiderMinigame minigameManager)
        {
            _pinAmount = difficulty;
            _minigameManager = minigameManager;
            SwitchState();
            GenerateInitialPipe();
        }

        private void RotatePipe()
        {
            float currentVelocity = input.spiderMinigameMove * rotationVelocity;
            transform.Rotate(currentVelocity, 0, 0);
        }

        private void MovePipe()
        {
            transform.Translate(-moveVelocity * Time.deltaTime, 0, 0);
            _movementCounter -= moveVelocity * Time.deltaTime;

            if(-_movementCounter >= _pipeLength*2)
            {
                _movementCounter = 0;
                Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);
                pipeGeneratorIEnumerator.MoveNext();
            }
        }

        private void SwitchState()
        {
            _minigameActive = !_minigameActive;
        }

        private void GenerateInitialPipe()
        {
            
            int initialLength = _initialPipeSegments * _pipeLength;
            for (int i = 0; i < initialLength; i += _pipeLength)
            {
                Vector3 newRotation = new Vector3(0, 0, 90);
                Vector3 newPipePos = new Vector3(i, 0, 0);
                Instantiate(pipeSegment, newPipePos, Quaternion.Euler(newRotation), transform);
            }

            _currentPipePos = initialLength;

            pipeGeneratorIEnumerator = GeneratePipe();

            for(int i = 0; i < 5; i++){
                pipeGeneratorIEnumerator.MoveNext();
            }
            
            
            _pipeGenerated = true;

            // int totalPipeLength = initialLength + _pinAmount * _pipeLength * 2;
            // for (int i = initialLength; i < initialLength + _pinAmount * _pipeLength * 2; i += _pipeLength * 2)
            // {
            //     int randomXRotation = (int) (Random.Range(0, 10)) * 36;
            //     Vector3 newRotation = new Vector3(randomXRotation, 0, 90);
            //
            //     Vector3 newPipePos = new Vector3(i, 0, 0);
            //     Instantiate(pipeSegment, newPipePos, Quaternion.Euler(newRotation), transform);
            //
            //     Vector3 newPipeWithPinsPos = new Vector3(i + _pipeLength, 0, 0);
            //     var currentPipeWithPin = Instantiate(pipeSegmentWithPin, newPipeWithPinsPos,
            //         Quaternion.Euler(newRotation), transform);
            //     
            //     
            //     currentPipeWithPin.transform.GetChild(0).GetComponent<Pin>().Setup(this);
            // }

            // var finalWall = Instantiate(pipeFinalWall, new Vector3(totalPipeLength - 4, 0, 0),
            //     pipeFinalWall.transform.rotation, transform);
            // _collider.size = finalWall.transform.localScale;
            // _collider.center = finalWall.transform.position;
        }

        private IEnumerator GeneratePipe()
        {
            int counter = 0;
            while (_minigameActive)
            {
                GeneratePipePiece();
                yield return counter;
            }
        }

        private void GeneratePipePiece()
        {
            var pipePos = transform.position;
            
            int randomXRotation = Random.Range(0, 10) * 36;
            Vector3 newRotation = new Vector3(randomXRotation, 0, 90);
            newRotation += transform.rotation.eulerAngles;
            Vector3 newPipePos = new Vector3(_currentPipePos, 0, 0);
            var currentPipe = Instantiate(pipeSegment, newPipePos + pipePos, Quaternion.Euler(newRotation), transform);

            Vector3 newPipeWithPinsPos = new Vector3(_currentPipePos + _pipeLength, 0, 0);
            var currentPipeWithPin = Instantiate(pipeSegmentWithPin, newPipeWithPinsPos + pipePos,
                Quaternion.Euler(newRotation), transform);
                
                
            currentPipeWithPin.transform.GetChild(0).GetComponent<Pin>().Setup(this);

            _currentPipePos += _pipeLength * 2;
        }
        public void PinTriggered()
        {
            _pinsTriggered++;

            if (_pinsTriggered >= _pinAmount)
            {
                MinigameFinished(true);
            }
        }

        private void MinigameFinished(bool completed)
        {
            _minigameActive = false;
            _minigameManager.FinishMinigame(completed);
        }

        private void OnTriggerEnter(Collider other)
        {
            MinigameFinished(false);
        }
    }
}