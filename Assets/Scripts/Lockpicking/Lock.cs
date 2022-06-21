using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace LockPicking
{
    public class Lock : MonoBehaviour
    {
        private Transform _lockPick;
        private LockPicking _caller;

        [Header("Angles Limits")]
        [Range(0, 180)][SerializeField] private float minMaxAngle = 90;
        [Range(-90, 0)] [SerializeField] private float maxOpeningAngle = -90;
        [SerializeField] private float openingThreshold = 2;
        
        [Header("Velocity")]
        [SerializeField] private float lockPickRotationMultiplier = 0.8f;
        [SerializeField] private float openVelocity;
        
        [Header("Shaking")]
        [SerializeField] private float shakeDuration = 0.3f;
        [SerializeField] private float shakeMagnitude = 0.5f;
        [SerializeField] private float delayBetweenShakes = 0.1f;
        
        private float _lockPickAngle;
        private float _lockAngle;

        private Vector3 _lockPickStartPosition;

        private float _threshold;
        private float _targetAngle;
        
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _lockPick = transform.GetChild(1);
            _lockPickStartPosition = _lockPick.transform.localPosition;
            _targetAngle = Random.Range(-minMaxAngle, minMaxAngle);
            Debug.Log(_targetAngle);
        }

        private void Open()
        {
            //_caller.OpenDoor();
        }

        public void Setup(LockPicking caller, float threshold)
        {
            _caller = caller;
            SetDifficulty(threshold);
        }
        private void SetDifficulty(float threshold)
        {
            _threshold = threshold;
        }

        public void RotateLockPick(float rotationAdded)
        {
            if (_lockAngle > 0.2 || _lockAngle < -0.2) return;
            
            rotationAdded *= lockPickRotationMultiplier;
            _lockPickAngle += rotationAdded;
            _lockPickAngle = Mathf.Clamp(_lockPickAngle, -minMaxAngle, minMaxAngle);
            var localRotation = _lockPick.localRotation;
            localRotation = Quaternion.Euler(localRotation.x, localRotation.y, _lockPickAngle);
            _lockPick.localRotation = localRotation;
        }

        public void OpenLock(float opening)
        {
            var targetRotation = GetTargetRotation();
            var angleToReach = maxOpeningAngle * (targetRotation == 0? 0:1) * opening;
            
            _lockAngle = Mathf.Lerp(_lockAngle, angleToReach, Time.deltaTime * openVelocity);
            _lockAngle = Mathf.Clamp(_lockAngle, targetRotation, 0);
            
            transform.localRotation = Quaternion.Euler(0f,0f,_lockAngle);
            if (_lockAngle < maxOpeningAngle+openingThreshold)
            {
                Open();
            }
            if(targetRotation != 0 && _lockAngle == targetRotation) BeginShake();
        }

        private float GetTargetRotation()
        {
            if (_lockPickAngle > _targetAngle + _threshold || _lockPickAngle < _targetAngle - _threshold) return 0;
            if (_lockPickAngle == _targetAngle) return maxOpeningAngle;
            if (_lockPickAngle < _targetAngle)
                return Map(_lockPickAngle, _targetAngle , _targetAngle-_threshold, maxOpeningAngle, 0);
            
            return Map(_lockPickAngle, _targetAngle + _threshold, _targetAngle, 0, maxOpeningAngle);
        }

        private void BeginShake()
        {
            _lockPick.transform.localPosition = _lockPickStartPosition;
            StopAllCoroutines();
            StartCoroutine(Shake());
        }
        private IEnumerator Shake()
        {
            float timer = 0f;
            
 
            while (timer < shakeDuration)
            {
                timer += Time.deltaTime;
 
                Vector3 randomPos = _lockPickStartPosition + (Random.insideUnitSphere * shakeMagnitude);
 
                _lockPick.transform.localPosition = randomPos;
 
                if (delayBetweenShakes > 0f)
                {
                    yield return new WaitForSeconds(delayBetweenShakes);
                }
                else
                {
                    yield return null;
                }
            }
 
            _lockPick.transform.localPosition = _lockPickStartPosition;
        }
        
        private float Map( float value, float leftMin, float leftMax, float rightMin, float rightMax )
        {
            return rightMin + ( value - leftMin ) * ( rightMax - rightMin ) / ( leftMax - leftMin );
        }
    }
}
