/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string gameOverSceneName;
    [SerializeField] private float elapsedTimeSeconds;

    private SoundManager soundManager;
    public SoundManager SoundManager
    {
        get
        {
            if (soundManager == null)
            {
                soundManager = FindFirstObjectByType<SoundManager>();
            }

            return soundManager;
        }
        private set
        {
            soundManager = value;
        }
    }

    private SegmentSpawner segmentSpawner;
    public SegmentSpawner SegmentSpawner
    {
        get
        {
            if (segmentSpawner == null)
            {
                segmentSpawner = FindFirstObjectByType<SegmentSpawner>();
            }

            return segmentSpawner;
        }
        private set
        {
            segmentSpawner = value;
        }
    }

    private BackgroundManager backgroundManager;
    public BackgroundManager BackgroundManager
    {
        get
        {
            if (backgroundManager == null)
            {
                backgroundManager = FindFirstObjectByType<BackgroundManager>();
            }

            return backgroundManager;
        }
        private set
        {
            backgroundManager = value;
        }
    }

    private PlayerController player;
    public PlayerController Player
    {
        get
        {
            if (player == null)
            {
                player = FindFirstObjectByType<PlayerController>();
            }

            return player;
        }
        private set
        {
            player = value;
        }
    }

    private UiManager uiManager;
    public UiManager UiManager
    {
        get
        {
            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UiManager>();
            }

            return uiManager;
        }
        private set
        {
            uiManager = value;
        }
    }

    private void Start()
    {
        SetGameState(EGameState.InMenu);
    }

    //States
    public void GameOver()
    {
        SetGameState(EGameState.InGameOver);
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
        SetGameState(EGameState.InMenu);
    }

    //Fired from game scene
    public void StartGame()
    {
        SetGameState(EGameState.InPlay);

        BackgroundManager.Initialize();
        SegmentSpawner.Initialize();
        Player.Initialize();

        UiManager.OnStartPressed();
    }

    public void RestartGame()
    {
        Player.ResetPlayer();
        SegmentSpawner.ResetSegments();
        BackgroundManager.ResetBackground();

        UiManager.OnRestartPressed();

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
