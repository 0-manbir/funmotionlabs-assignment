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

    void Start()
    {
        camera1.gameObject.SetActive(false);
        camera2.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        movieFrame.SetActive(true);

        StartCinematic();
    }

    void StartCinematic()
    {
        StartCoroutine(CinematicSequence());
    }

    IEnumerator CinematicSequence()
    {
        camera1.gameObject.SetActive(true);
        yield return new WaitForSeconds(topDownDuration);
        camera1.gameObject.SetActive(false);

        camera2.gameObject.SetActive(true);
        yield return new WaitForSeconds(finishLineDuration);
        camera2.gameObject.SetActive(false);

        StartCoroutine(TransitionToPlayerCamera());
    }

    IEnumerator TransitionToPlayerCamera()
    {
        yield return new WaitForSeconds(1f);

        playerCamera.gameObject.SetActive(true);
        movieFrame.SetActive(false);

        OnCinematicEnd();
    }

    void OnCinematicEnd()
    {
        GameManager.Instance.StartGame();
    }
}
