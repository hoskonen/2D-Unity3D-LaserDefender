using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float moveSpeed;
    int waypointIndex;

    private void Awake()
    {
        moveSpeed = 10f;
        waypointIndex = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Take position of the first waypoint node in our List
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        MoveToWayPoints();
    }

    private void MoveToWayPoints()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
                Debug.Log($"<color=green>{waypointIndex}</color>");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}