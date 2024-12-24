using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] Obstacle obstacleType;

    [Space]
    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] float amplitude;
    [SerializeField] float rotateSpeed;

    Vector3 previousPosition;
    Vector3 platformVelocity;

    void Update ()
    {
        switch (obstacleType)
        {
            case Obstacle.MoveLeftRight:
            case Obstacle.MoveUpDown:
                Vector3 newPosition = transform.position + Mathf.Sin(Time.time * speed) * amplitude * (obstacleType == Obstacle.MoveLeftRight ? transform.forward : transform.up);
                platformVelocity = (newPosition - previousPosition) / Time.deltaTime;
                transform.position = newPosition;
                previousPosition = newPosition;
                break;
            case Obstacle.Rotate:
                transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
                break;

            default:
                Debug.LogWarning("Obstacle type is not selected");
                break;
        }
    }

    public Obstacle ObstacleType { get { return obstacleType; } }
    public Vector3 PlatformVelocity { get { return platformVelocity; } }
}

public enum Obstacle
{
    MoveLeftRight,
    MoveUpDown,
    Rotate,
}