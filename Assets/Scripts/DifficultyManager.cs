using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float startingSpeed;
    [SerializeField] private float speedLimit;
    [SerializeField] private int difficultyIncreaseTimeInterval;
    [SerializeField] private float difficultySpeedIncreaseValue;

    public void Initialize()
    {
        StartCoroutine(DifficultyAdjustCoroutine());
    }

    public void ResetDifficultyAdjust()
    {
        speedLimit = startingSpeed;
    }

    public void StopDifficultyAdjust()
    {
        StopCoroutine(DifficultyAdjustCoroutine());
    }

    IEnumerator DifficultyAdjustCoroutine()
    {
        while (true)
        {
            //This is set up similarly to the timer coroutine
            yield return new WaitUntil(() => GameManager.Instance.GetGameState() == EGameState.InPlay);
            yield return new WaitForSeconds(difficultyIncreaseTimeInterval);

            //Protects the IncreaseSpeedLimit function from firing if the InPlay state was changed during the
            //above WaitForSeconds interval
            if (GameManager.Instance.GetGameState() == EGameState.InPlay)
            {
                IncreaseSpeedLimit(difficultySpeedIncreaseValue);
            }
        }
    }

    //Getters
    public float GetStartingSpeed()
    {
        return startingSpeed;
    }

    public float GetSpeedLimit()
    {
        return speedLimit;
    }

    //Setters
    public void SetSpeedLimit(float newSpeed)
    {
        speedLimit = newSpeed;
    }

    public void IncreaseSpeedLimit(float speedIncrease)
    {
        speedLimit += speedIncrease;
    }
}
