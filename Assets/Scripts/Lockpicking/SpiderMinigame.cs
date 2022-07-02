using Camera;
using Environment;
using UnityEngine;
using UnityEngine.Rendering;

namespace Lockpicking
{
    public class SpiderMinigame : MonoBehaviour
    {
        [SerializeField] private CameraSwitching cameraSwitching;
        [SerializeField] private Pipe pipe;
        private LockPicking.LockPicking _lockPickingManager;
        
        public void StartMinigame(int difficulty, Door door, LockPicking.LockPicking lockPickingManager)
        {
            _lockPickingManager = lockPickingManager;
            cameraSwitching.SetDoorCamera(door.GetCameraTransform());
            cameraSwitching.SwitchState(true);
            pipe.Setup(difficulty, this);
        }

        public void FinishMinigame(bool completed)
        {
            cameraSwitching.SwitchState(false);
            if (completed) _lockPickingManager.OpenDoor();
            else _lockPickingManager.Deactivate();
        }
    }
}
