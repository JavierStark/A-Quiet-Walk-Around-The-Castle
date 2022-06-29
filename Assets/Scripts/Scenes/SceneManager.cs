using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes
{
    public class SceneManager : MonoBehaviour
    {
        [Range(0, 3)] [SerializeField] private float fadeTime;
        [Range(0, 3)] [SerializeField] private float loadingTime;
        private RawImage _fadeImage;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _fadeImage = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
        }

        public void ChangeScene(int index)
        {
            Debug.Log("Change");
            StartCoroutine(ChangeSceneCoroutine(index));
        }
        
        private IEnumerator ChangeSceneCoroutine(int index)
        {
            yield return StartCoroutine(FadeScene(false));
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);

            yield return new WaitForSeconds(loadingTime);
            
            yield return StartCoroutine(FadeScene(true));
        }
        
        private IEnumerator FadeScene(bool fadeIn)
        {
            if (fadeIn)
            {
                for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
                {
                    float alpha = Map(i, 0, fadeTime, 0, 1);
                    _fadeImage.color = new Color(0, 0, 0, alpha);
                    yield return null;
                }
            }
            else
            {
                for (float i = 0; i <= fadeTime; i += Time.deltaTime)
                {
                    float alpha = Map(i, 0, fadeTime, 0, 1);
                    _fadeImage.color = new Color(0, 0, 0, alpha);
                    yield return null;
                }
            }
        }
        
        private float Map( float value, float leftMin, float leftMax, float rightMin, float rightMax )
        {
            return rightMin + ( value - leftMin ) * ( rightMax - rightMin ) / ( leftMax - leftMin );
        }
    }
}
