/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 4
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerValue;
    [SerializeField] private float gameplayTimer;

    public void Initialize()
    {
        gameplayTimer = 0.0f;
        StartCoroutine(TimerCoroutine());
    }

    public void ResetTimer()
    {
        gameplayTimer = 0.0f;
        UpdateTimerUi(gameplayTimer);
    }

    public void StopTimer()
    {
        StopCoroutine(TimerCoroutine());
    }

    private void UpdateTimerUi(float elapsedTime)
    {
        timerValue.text = GetElapsedTimeFormatted(elapsedTime);
    }

    private string GetElapsedTimeFormatted(float elapsedTime)
    {
        int minutes = (int)elapsedTime / 60;
        float seconds = elapsedTime % 60.0f;
        return $"{minutes:00}:{seconds:00.00}";
    }

    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            //Control whether or not the timer is counting
            //Only count time when in the 'InPlay' state
            yield return new WaitUntil(() => GameManager.Instance.GetGameState() == EGameState.InPlay);

            //These lines only fire when in the play state
            gameplayTimer += Time.deltaTime;
            UpdateTimerUi(gameplayTimer);
        }
    }
}
