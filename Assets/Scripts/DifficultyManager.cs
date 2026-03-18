/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 4
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float startingSpeed;
    [SerializeField] private float speedLimit;
    [SerializeField] private int difIncreaseTimeInterval;
    [SerializeField] private float difSpeedIncreaseValue;

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
            yield return new WaitForSeconds(difIncreaseTimeInterval);

            //Protects the IncreaseSpeedLimit function from firing if the InPlay state was changed during the
            //above WaitForSeconds interval
            if (GameManager.Instance.GetGameState() == EGameState.InPlay)
            {
                IncreaseSpeedLimit(difSpeedIncreaseValue);
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

    //Increase difficulty
    public void IncreaseSpeedLimit(float speedIncrease)
    {
        speedLimit += speedIncrease;
    }
}
