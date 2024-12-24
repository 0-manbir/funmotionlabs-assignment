using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] LayerMask obstaclesLayer;

    [Space]
    [Header("Forward Ray")]
    [SerializeField] float forwardRayOffsetY = 2.5f;
    [SerializeField] float forwardRayDistance = 0.75f;

    [Header("Height Ray")]
    [SerializeField] float heightRayLength = 10f;

    public ObstacleData GetObstaclesData()
    {
        ObstacleData data = new ();

        Vector3 forwardRayOrigin = transform.position + forwardRayOffsetY * Vector3.up;
        data.forwardHit = Physics.Raycast(forwardRayOrigin, transform.forward, out data.forwardHitInfo, forwardRayDistance, obstaclesLayer);

        Debug.DrawRay(forwardRayOrigin, transform.forward, data.forwardHit ? Color.red : Color.white);

        if (data.forwardHit)
        {
            Vector3 heightRayOrigin = data.forwardHitInfo.point + Vector3.up * heightRayLength;
            data.heightHit = Physics.Raycast(heightRayOrigin, Vector3.down, out data.heightHitInfo, heightRayLength, obstaclesLayer);

            Debug.DrawRay(heightRayOrigin, Vector3.down * heightRayLength, Color.red);
        }

        return data;
    }
}

public struct ObstacleData
{
    public bool forwardHit;
    public bool heightHit;

    public RaycastHit forwardHitInfo;
    public RaycastHit heightHitInfo;
}