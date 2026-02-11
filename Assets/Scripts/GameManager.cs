using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;

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
        BackgroundManager.Instance.ResetBackground();
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
