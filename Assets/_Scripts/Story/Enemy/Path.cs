using _Scripts.Interactables;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Story.Enemy
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float radius;

        [SerializeField] private UnityEvent onPathFinished;
        public Transform[] GetPath()
        {
            return points;
        }

        public void PathFinished()
        {
            if (onPathFinished != null)
            {
                onPathFinished.Invoke();
            }
        }
        
        private void OnDrawGizmos()
        {
            if (points.Length == 0) return;
            for (int i = 0; i < points.Length; i++)
            {
                if (i == 0) Gizmos.color = Color.green;
                else Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(points[i].position, radius);

                if (i == points.Length - 1) continue;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(points[i].position, points[i + 1].position);

            }
        }
    }
}