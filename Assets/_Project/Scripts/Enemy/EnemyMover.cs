using System;
using UnityEngine;

namespace LastLineDefense.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2f;

        private Transform[] waypoints;
        private int currentWaypointIndex;

        public event Action OnReachedEnd;

        public void SetWaypoints(Transform[] points)
        {
            waypoints = points;
            currentWaypointIndex = 0;

            if (waypoints != null && waypoints.Length > 0)
                transform.position = waypoints[0].position;
        }

        public void SetSpeed(float speed)
        {
            moveSpeed = speed;
        }

        private void Update()
        {
            if (waypoints == null || waypoints.Length == 0) return;
            if (currentWaypointIndex >= waypoints.Length) return;

            Transform target = waypoints[currentWaypointIndex];
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentWaypointIndex++;

                if (currentWaypointIndex >= waypoints.Length)
                {
                    OnReachedEnd?.Invoke();
                }
            }
        }
    }
}
