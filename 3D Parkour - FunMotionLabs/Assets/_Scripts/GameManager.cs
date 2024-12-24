using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] StopWatch stopWatch;

    [SerializeField] GameObject losePanel;

    [Space]
    [Header("Win Panel")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject[] stars;
    [SerializeField] TMP_Text winText;

    int coinsCollected = 0;

    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void StartGame()
    {
        stopWatch.StartStopwatch();
    }

    public void CoinCollected ()
    {
        coinsCollected++;
    }

    #region Win/Lose

    public void WinGame()
    {
        stopWatch.StopStopwatch();
        winPanel.SetActive(true);

        winText.text = "Time: " + stopWatch.GetFormattedTime + "\nCoins Collected: " + coinsCollected + "/2";

        Color starColorEnable = stars[0].GetComponent<Image>().color;
        starColorEnable.a = 1;
        Color starColorDisable = starColorEnable;
        starColorDisable.a = 0.1f;
        
        stars[0].GetComponent<Image>().color = stopWatch.GetTime < 120f ? starColorEnable : starColorDisable;
        stars[1].GetComponent<Image>().color = coinsCollected > 0 ? starColorEnable : starColorDisable;
        stars[2].GetComponent<Image>().color = coinsCollected > 1 ? starColorEnable : starColorDisable;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #endregion

    #region UI Functions

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion
}