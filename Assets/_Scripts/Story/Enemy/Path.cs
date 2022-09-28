using UnityEngine;

namespace _Scripts.Story.Enemy
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float radius;
        public Transform[] GetPath()
        {
            return points;
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