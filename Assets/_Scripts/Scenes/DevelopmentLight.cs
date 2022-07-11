using System;
using UnityEngine;

namespace Scenes
{
    [ExecuteInEditMode]
    public class DevelopmentLight : MonoBehaviour
    {
        [SerializeField] private bool inPlayMode = false;
        private bool _isPlaying = false;
        private void Update()
        {
            if (inPlayMode == Application.isPlaying) return;
            if (_isPlaying != Application.isPlaying)
            {
                _isPlaying = !_isPlaying;
                transform.GetChild(0).gameObject.SetActive(!_isPlaying);
            }
        }
    }
}
