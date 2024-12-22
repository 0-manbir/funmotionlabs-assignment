using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    CharacterController cc;
    Animator anim;

    [SerializeField] float playerSpeed;
    [SerializeField] float gravity;

    void Start ()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.forward * v + transform.right * h;
        Vector3 velocity = moveDir * playerSpeed + gravity * Vector3.up;

        anim.SetFloat("speed", Mathf.Clamp(moveDir.magnitude * playerSpeed, 0, 1));

        cc.Move(velocity * Time.deltaTime);
    }
}