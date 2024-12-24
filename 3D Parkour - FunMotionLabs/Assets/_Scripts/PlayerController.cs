using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Singleton

    public static PlayerController Instance;

    void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    CharacterController cc;
    Animator anim;

    [SerializeField] float playerSpeed;
    [SerializeField] float gravity;

    [Space]
    [Header("Sprint")]
    [SerializeField] Scrollbar sprintBar;
    [SerializeField] float sprintSpeed;
    [SerializeField] float sprintDuration = 5f;
    [SerializeField] float sprintCooldown = 0.5f;

    [Space]
    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    [Space]
    [SerializeField] float smoothTime = 0.1f;

    float speedVelocity = 0f;
    Vector3 velocity;
    [SerializeField] bool isGrounded;

    float sprintRemaining;
    float sprintCooldownReset;
    bool isSprinting = false;
    bool isSprintCooldown = false;

    bool isSliding = false;
    bool isJumping = false;

    Vector3 platformVelocity;

    bool hasControl = true;

    void Start ()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        sprintRemaining = sprintDuration;
        sprintCooldownReset = sprintCooldown;
    }

    void Update ()
    {
        CheckGround();

        if (hasControl)
            Move();

        Sprint();
    }

    void FixedUpdate()
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift) && sprintRemaining > 0f && !isSprintCooldown;
    }

    #region Movement

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("isJumping", !isGrounded);
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;

        if (moveDir == Vector3.zero) isSprinting = false;

        // Sliding
        if (Input.GetKeyDown(KeyCode.C) && !isSliding && !isJumping && moveDir != Vector3.zero)
        {
            isSliding = true;
            StartCoroutine(Slide());
        }

        // Jumping
        if (isJumping && isGrounded)
        {
            isJumping = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        float targetSpeed = moveDir.magnitude;
        float smoothedSpeed = Mathf.SmoothDamp(anim.GetFloat("speed"), targetSpeed, ref speedVelocity, smoothTime);
        anim.SetFloat("speed", smoothedSpeed);

        if (!isJumping) velocity.x = moveDir.x * (isSprinting ? sprintSpeed : playerSpeed);
        if (!isJumping) velocity.z = moveDir.z * (isSprinting ? sprintSpeed : playerSpeed);
        if (!isGrounded) velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }

    #endregion

    #region Sprinting

    void Sprint()
    {
        if(isSprinting)
        {
            sprintRemaining -= Time.deltaTime;
            if (sprintRemaining <= 0)
            {
                isSprinting = false;
                isSprintCooldown = true;
            }
        }
        else
            sprintRemaining = Mathf.Clamp(sprintRemaining += 1 * Time.deltaTime, 0, sprintDuration);

        if(isSprintCooldown)
        {
            sprintCooldown -= Time.deltaTime;
            if (sprintCooldown <= 0)
                isSprintCooldown = false;
        }
        else
        {
            sprintCooldown = sprintCooldownReset;
        }

        float sprintRemainingPercent = sprintRemaining / sprintDuration;
        sprintBar.size = sprintRemainingPercent;
    }

    #endregion

    #region Sliding
    
    IEnumerator Slide()
    {
        SetControl(false);
        anim.Play("Slide");
        anim.applyRootMotion = true;
        yield return null;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        SetControl(true);
        isSliding = false;
        anim.applyRootMotion = false;

        Move();
    }

    #endregion

    #region Collisions Manager

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("MovingPlatform"))
        {
            Obstacles platform = collider.GetComponent<Obstacles>();
            platformVelocity = platform.PlatformVelocity;

            // set the player as a child of the platform
            transform.SetParent(collider.transform);
        }
        else if (collider.CompareTag("FinishLine"))
        {
            GameManager.Instance.WinGame();
        }
        else if (collider.CompareTag("Void"))
        {
            GameManager.Instance.LoseGame();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("MovingPlatform"))
        {
            Obstacles platform = collider.GetComponent<Obstacles>();

            platformVelocity = platform.PlatformVelocity;
            GetComponent<CharacterController>().Move(platformVelocity * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            platformVelocity = Vector3.zero;
        }
    }

    #endregion

    public void SetControl(bool hasControl)
    {
        this.hasControl = hasControl;
        cc.enabled = hasControl;

        if (!hasControl)
        {
            anim.SetFloat("speed", 0);
        }
    }

    public bool IsJumping { set { isJumping = value; } }
}