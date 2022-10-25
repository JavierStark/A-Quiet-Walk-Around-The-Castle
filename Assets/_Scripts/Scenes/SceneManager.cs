using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.Scenes
{
    public class SceneManager : MonoBehaviour
    {
        [Range(0, 3)] [SerializeField] private float fadeTime;
        [Range(0, 3)] [SerializeField] private float loadingTime;

        [SerializeField] private Button[] menuButtons;
        private RawImage _fadeImage;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _fadeImage = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
            SetupScene();
        }

        public void ChangeScene(int index)
        {
            DeactivateButtons();
            Debug.Log("Change");
            StartCoroutine(ChangeSceneCoroutine(index));
        }

        private IEnumerator ChangeSceneCoroutine(int index)
        {
            yield return StartCoroutine(FadeScene(false));
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(index);
            
            yield return new WaitForSeconds(loadingTime);
            SetupScene();
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

        private void SetupScene()
        {
            GetSceneButtons();

            SetCursor(menuButtons.Length > 0);
        }

        private void SetCursor(bool isVisible)
        {
            Cursor.lockState = isVisible? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isVisible;
        }

        private void GetSceneButtons()
        {
            menuButtons = FindObjectsOfType<Button>();
        }

        private void DeactivateButtons()
        {
            foreach (Button b in menuButtons)
            {
                b.interactable = false;
            }
        }

        private float Map( float value, float leftMin, float leftMax, float rightMin, float rightMax )
        {
            return rightMin + ( value - leftMin ) * ( rightMax - rightMin ) / ( leftMax - leftMin );
        }
    }
}
