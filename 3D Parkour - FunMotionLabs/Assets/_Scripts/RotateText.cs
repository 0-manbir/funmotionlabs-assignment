using UnityEngine;

public class RotateText : MonoBehaviour
{
    PlayerController player;

    Vector3 playerPos;

    void Start()
    {
        player = PlayerController.Instance;
    }

    void Update()
    {
        playerPos = player.transform.position;
        transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
        transform.Rotate(180f * Vector3.up);
    }
}
