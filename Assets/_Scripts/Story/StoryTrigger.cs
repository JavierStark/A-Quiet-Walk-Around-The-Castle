using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Story
{
    public class StoryTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent toActivate;

        private void OnTriggerEnter(Collider other)
        {
            toActivate.Invoke();
            gameObject.SetActive(false);
        }
    }
}
