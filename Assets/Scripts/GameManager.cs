/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 3
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private EGameState CurrentGameState;

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

    private void Start()
    {
        SetGameState(EGameState.InMenu);
    }

    //States
    public void GameOver()
    {
        SetGameState(EGameState.InGameOver);
        SceneManager.LoadScene("LE3_GameOver");
    }

    public void PlayGame()
    {
        //Only switch to InPlay if we are not already playing
        if (CurrentGameState != EGameState.InPlay)
        {
            SetGameState(EGameState.InPlay);

            Player.Initialize();
            BackgroundManager.Initialize();
            SegmentSpawner.Initialize();
        }
    }

    public void RestartGame()
    {
        //Only reset stuff if we are in play
        if (CurrentGameState == EGameState.InPlay)
        {
            Player.ResetPlayer();
            BackgroundManager.ResetBackground();
            SegmentSpawner.ResetSegments();
        }

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
