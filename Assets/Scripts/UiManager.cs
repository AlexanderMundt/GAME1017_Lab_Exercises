/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton, restartButton, gameOverButton;
    [SerializeField] private TMP_Text timerValue;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        startButton.SetActive(true);
        restartButton.SetActive(false);
        gameOverButton.SetActive(false);
    }

    public void OnStartPressed()
    {
        startButton.SetActive(false);
        restartButton.SetActive(true);
        gameOverButton.SetActive(true);
    }

    public void OnRestartPressed()
    {
        Initialize();
    }

    public void UpdateTimerUi(float elapsedTime)
    {
        timerValue.text = GetElapsedTimeFormatted(elapsedTime);
    }

    private string GetElapsedTimeFormatted(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
