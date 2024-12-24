using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] StopWatch stopWatch;

    void Start()
    {
        stopWatch.StartStopwatch();
    }
}