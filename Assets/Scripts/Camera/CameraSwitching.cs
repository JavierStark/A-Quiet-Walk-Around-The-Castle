using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Camera
{
    public class CameraSwitching : MonoBehaviour
    {

        private Animator _animator;
        private CinemachineStateDrivenCamera _stateDrivenCamera;
        
        const int DoorCameraIndex = 2;
        
        private static readonly int InSpider = Animator.StringToHash("InSpider");
        private static readonly int Fade = Animator.StringToHash("Fade");

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
        }

        public void SwitchState(bool inSpider)
        {
            _animator.SetBool(InSpider, inSpider);
        }


        public void FadeTrigger()
        {
            _animator.SetTrigger(Fade);
        }

        public void SetDoorCamera(Transform doorTransform)
        {
            
            _stateDrivenCamera.ChildCameras[DoorCameraIndex].transform.position = doorTransform.position;
            _stateDrivenCamera.ChildCameras[DoorCameraIndex].transform.rotation = doorTransform.rotation;
            _stateDrivenCamera.ChildCameras[DoorCameraIndex].Follow = doorTransform;

        }
    }
}
