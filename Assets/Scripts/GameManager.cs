/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;
    [SerializeField] private string gameSceneName;
    [SerializeField] private string gameOverSceneName;
    [SerializeField] private int difficultyIncreaseInterval;

    private float difficultyIncreaseIntervalTimer;
    private float gameplayTimer;

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
        StopAllCoroutines();
        ResetTimers();

        SetGameState(EGameState.InGameOver);
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
        SetGameState(EGameState.InMenu);

        //Start coroutines
        StartCoroutine(TimerCoroutine());
        StartCoroutine(DifficultyAdjustCoroutine());
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

        //Timers
        ResetTimers();
        UiManager.UpdateTimerUi(gameplayTimer);

        SetGameState(EGameState.InMenu);
    }

    private void ResetTimers()
    {
        ResetGameplayTimer();
        ResetDifficultyTimer();
    }

    private void ResetGameplayTimer()
    {
        gameplayTimer = 0.0f;
    }

    private void ResetDifficultyTimer()
    {
        difficultyIncreaseIntervalTimer = 0.0f;
    }

    //Coroutines
    IEnumerator TimerCoroutine()
    {
        while (true)
        {
            //Control whether or not the timer is counting
            //Only count time when in the 'InPlay' state
            yield return new WaitUntil(() => CurrentGameState == EGameState.InPlay);

            gameplayTimer += Time.deltaTime;
            UiManager.UpdateTimerUi(gameplayTimer);
        }
    }

    IEnumerator DifficultyAdjustCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => CurrentGameState == EGameState.InPlay);
            yield return new WaitForSeconds(difficultyIncreaseInterval);

            Player.IncreaseSpeedLimit();

            //Gets whole seconds from the elapsedTime value
            //difficultyIncreaseIntervalTimer = TimeSpan.FromSeconds(gameplayTimer).Seconds;

            //if (difficultyIncreaseIntervalTimer == difficultyIncreaseInterval)
            //{
            //    Player.IncreaseSpeedLimit();
            //    difficultyIncreaseIntervalTimer -= difficultyIncreaseInterval;
            //}
        }
    }
}
