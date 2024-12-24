using System.Collections;
using UnityEngine;

public class ParkourSystem : MonoBehaviour
{
    Animator animator;
    PlayerController playerController;
    ObstacleDetector obstacleDetector;
    ObstacleData obstacleData;

    [Header("Actions")]
    [SerializeField] ParkourAction[] parkourActions;

    bool isJumping;

    public bool CanPerformAction { get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        obstacleDetector = GetComponent<ObstacleDetector>();
    }

    void Update()
    {
        CanPerformAction = true;

        if (!isJumping && Input.GetButtonDown("Jump"))
        {
            obstacleData = obstacleDetector.GetObstaclesData();

            if (obstacleData.forwardHit)
            {
                foreach (ParkourAction parkourAction in parkourActions)
                {
                    if (parkourAction.IsActionPossible(obstacleData, transform))
                    {
                        StartCoroutine(HasJumped(parkourAction));
                        break;
                    }
                }

                CanPerformAction = false;
            }
            else
            {
                // Jump Up
                playerController.IsJumping = true;
                animator.SetBool("isJumping", true);
                animator.Play("Jump");
            }
        }
    }

    IEnumerator HasJumped(ParkourAction parkourAction)
    {
        isJumping = true;
        playerController.SetControl(false);

        animator.Play(parkourAction.Name);
        animator.applyRootMotion = true;
        yield return null;

        float animationDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;

            if (parkourAction.EnableTargetMatching)
                MatchTarget(parkourAction);

            yield return null;
        }

        isJumping = false;
        animator.applyRootMotion = false;
        playerController.SetControl(true);
    }

    void MatchTarget(ParkourAction parkourAction)
    {
        if (animator.isMatchingTarget)
            return;
            
        animator.MatchTarget(parkourAction.MatchPos, transform.rotation, parkourAction.PartToMatch, new MatchTargetWeightMask(Vector3.up, 0), parkourAction.MatchStartTime, parkourAction.MatchTargetTime);
    }
}