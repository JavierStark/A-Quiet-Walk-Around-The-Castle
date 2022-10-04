using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Story
{
    public class StoryTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTrigger;

        private void OnTriggerEnter(Collider other)
        {
            onTrigger.Invoke();
            gameObject.SetActive(false);
        }
    }
}
