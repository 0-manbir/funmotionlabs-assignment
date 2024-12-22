using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float amplitude;

    [SerializeField] float rotateSpeed;

    void Update ()
    {
        // Move up & down
        transform.position += Mathf.Sin(Time.time * speed) * amplitude * Vector3.up;

        // Rotate
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
    }
}
