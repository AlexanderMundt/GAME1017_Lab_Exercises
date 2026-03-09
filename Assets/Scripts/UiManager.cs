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

    private bool isTimerRunning;
    private float elapsedTime;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUi();
        }
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

        //Run the timer
        isTimerRunning = true;
    }

    public void OnRestartPressed()
    {
        Initialize();

        //Stop the timer, reset the time value, then send that value to the ui
        isTimerRunning = false;
        elapsedTime = 0.0f;
        UpdateTimerUi();
    }

    private string GetElapsedTimeFormatted()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        return $"{minutes:00}:{seconds:00}";
    }

    private void UpdateTimerUi()
    {
        timerValue.text = GetElapsedTimeFormatted();
    }
}
