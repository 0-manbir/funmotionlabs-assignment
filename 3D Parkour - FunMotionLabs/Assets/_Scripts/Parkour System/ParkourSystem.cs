using UnityEngine;

public class ParkourSystem : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] LayerMask obstaclesMask;
    [SerializeField] float rayOffsetY = 2.5f;
    [SerializeField] float rayDistance = 0.75f;

    void Start()
    {

    }

    void Update()
    {
        CheckObstacles();
    }

    void CheckObstacles()
    {
        Vector3 rayOrigin = transform.position + rayOffsetY * Vector3.up;

        bool isObstacle = Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hitInfo, rayDistance, obstaclesMask);

        Debug.DrawRay(rayOrigin, transform.forward, isObstacle ? Color.red : Color.white);
    }
}