using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] Obstacle obstacleType;

    [Space]
    [Header("Properties")]
    [SerializeField] float speed;
    [SerializeField] float amplitude;

    void Update ()
    {
        switch (obstacleType)
        {
            case Obstacle.MoveLeftRight:
                transform.position += Mathf.Sin(Time.time * speed) * amplitude * Vector3.forward;
                break;
            case Obstacle.MoveUpDown:
                break;
            case Obstacle.Rotate:
                break;

            default:
                Debug.LogWarning("Obstacle type is not selected");
                break;
        }
    }
}

enum Obstacle
{
    MoveLeftRight,
    MoveUpDown,
    Rotate,
}