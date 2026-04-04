/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 5
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton, restartButton, gameOverButton;

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

    public void ResetUiButtons()
    {
        Initialize();
    }
}
