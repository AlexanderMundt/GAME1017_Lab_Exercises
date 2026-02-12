/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;

    [Header("Managers")]
    [SerializeField] public SoundManager soundManager;
    [SerializeField] private BackgroundManager backgroundManager;

    private void Start()
    {
        SetGameState(EGameState.InMenu);
    }

    //States
    public void GameOver()
    {
        SetGameState(EGameState.InGameOver);
    }

    public void PlayGame()
    {
        SetGameState(EGameState.InPlay);
    }

    public void RestartGame()
    {
        FindFirstObjectByType<PlayerController>().ResetPlayer();
        backgroundManager.ResetBackground();
        SetGameState(EGameState.InMenu);
    }

    //Setter
    private void SetGameState(EGameState state)
    {
        CurrentGameState = state;
    }

    //Getter
    public EGameState GetGameState()
    {
        return CurrentGameState;
    }
}
