/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 4
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

    private Timer timer;
    public Timer Timer
    {
        get
        {
            if (timer == null)
            {
                timer = FindFirstObjectByType<Timer>();
            }

            return timer;
        }
        private set
        {
            timer = value;
        }
    }

    private DifficultyManager difficultyManager;
    public DifficultyManager DifficultyManager
    {
        get
        {
            if (difficultyManager == null)
            {
                difficultyManager = FindFirstObjectByType<DifficultyManager>();
            }

            return difficultyManager;
        }
        private set
        {
            difficultyManager = value;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //True when we load the game scene
        if (scene == SceneManager.GetSceneByName(gameSceneName))
        {
            GameSceneStart();
        }
        Debug.Log(scene.name);
    }

    //Getter
    public EGameState GetGameState()
    {
        return CurrentGameState;
    }

    //Setter
    private void SetGameState(EGameState state)
    {
        CurrentGameState = state;
    }

    //States
    public void GameOver()
    {
        Timer.StopTimer();
        DifficultyManager.StopDifficultyAdjust();

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
        UiManager.ResetUiButtons();
        DifficultyManager.ResetDifficultyAdjust();
        Timer.ResetTimer();
        Player.ResetPlayer();
        SegmentSpawner.ResetSegments();
        BackgroundManager.ResetBackground();

        SetGameState(EGameState.InMenu);
    }

    //Scene change function
    public void GameSceneStart()
    {
        UiManager.Initialize();
        Timer.Initialize();
        DifficultyManager.Initialize();
    }
}
