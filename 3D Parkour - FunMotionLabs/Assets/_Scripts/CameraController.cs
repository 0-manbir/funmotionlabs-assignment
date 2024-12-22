using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform cameraPivot;

    [SerializeField] float mouseSensitivityX = 100f;
    [SerializeField] float mouseSensitivityY = 100f;
    
    [SerializeField] bool invertX = false;
    [SerializeField] bool invertY = false;

    [SerializeField] float minVerticalAngle = -10f;
    [SerializeField] float maxVerticalAngle = 50f;

    [SerializeField] bool showCursor = false;

    float pitch = 0f;

    void Start ()
    {
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showCursor;
    }

    void Update ()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime * (invertX ? -1f : 1f);
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime * (invertY ? -1f : 1f);
        
        player.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        cameraPivot.localEulerAngles = Vector3.right * pitch;
    }
}