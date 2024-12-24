using UnityEngine;
using System.Collections;
using Cinemachine;

public class LevelIntroduction : MonoBehaviour
{
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public CinemachineVirtualCamera playerCamera;
    public GameObject movieFrame;
    public float topDownDuration = 5f;
    public float finishLineDuration = 5f;

    int defaultPriority = 10;
    int inactivePriority = 0;

    void Start()
    {
        camera1.Priority = inactivePriority;
        camera2.Priority = inactivePriority;
        playerCamera.Priority = inactivePriority;

        movieFrame.SetActive(true);
        StartCinematic();
    }

    void StartCinematic()
    {
        StartCoroutine(CinematicSequence());
    }

    IEnumerator CinematicSequence()
    {
        // camera1.Priority = defaultPriority;
        // yield return new WaitForSeconds(topDownDuration);

        camera1.Priority = inactivePriority;
        camera2.Priority = defaultPriority;
        yield return new WaitForSeconds(finishLineDuration);

        TransitionToPlayerCamera();
    }

    void TransitionToPlayerCamera()
    {
        playerCamera.Priority = defaultPriority;

        StartCoroutine(DisableMovieFrameAfterBlend());
    }

    IEnumerator DisableMovieFrameAfterBlend()
    {
        yield return new WaitForSeconds(2f);

        movieFrame.SetActive(false);
        OnCinematicEnd();
    }

    void OnCinematicEnd()
    {
        GameManager.Instance.StartGame();
    }
}
