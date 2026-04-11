/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;
    [SerializeField] private string titleSceneName;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string gameOverSceneName;

    public Leaderboard Leaderboard => FindFirstObjectByType<Leaderboard>();

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

    private SaveSystem saveSystem;
    public SaveSystem SaveSystem
    {
        get
        {
            if (saveSystem == null)
            {
                saveSystem = FindFirstObjectByType<SaveSystem>();
            }

            return saveSystem;
        }
        private set
        {
            saveSystem = value;
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
        //True when we load the title scene
        if (scene == SceneManager.GetSceneByName(titleSceneName))
        {
            TitleSceneStart();
        }
        //True when we load the game scene
        else if (scene == SceneManager.GetSceneByName(gameSceneName))
        {
            GameSceneStart();
        }
        //True when we load the game over scene
        else if (scene == SceneManager.GetSceneByName(gameOverSceneName))
        {
            GameOverSceneStart();
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
        SaveSystem.SaveTimer(Timer.GetCurrentElapsedTime());

        Player.OnGameOver();
        SegmentSpawner.OnGameOver();

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
        Timer.ResetTimer();
        Player.ResetPlayer();
        SegmentSpawner.ResetSegments();
        BackgroundManager.ResetBackground();

        SetGameState(EGameState.InMenu);
    }

    //Scene change functions
    public void TitleSceneStart()
    {
        //Loads the volume settings from the last run
        SoundManager.ChangeMusicVolume(SaveSystem.LoadVolume(EAudioType.Music));
        SoundManager.ChangeSfxVolume(SaveSystem.LoadVolume(EAudioType.Sfx));
    }

    public void GameSceneStart()
    {
        SoundManager.ChangeMusicVolume(SaveSystem.LoadVolume(EAudioType.Music));
        SoundManager.ChangeSfxVolume(SaveSystem.LoadVolume(EAudioType.Sfx));

        UiManager.Initialize();
        Timer.Initialize();
    }

    public void GameOverSceneStart()
    {
        SoundManager.ChangeMusicVolume(SaveSystem.LoadVolume(EAudioType.Music));
        SoundManager.ChangeSfxVolume(SaveSystem.LoadVolume(EAudioType.Sfx));

        Leaderboard.Initialize(SaveSystem.GetScores());
    }
}
