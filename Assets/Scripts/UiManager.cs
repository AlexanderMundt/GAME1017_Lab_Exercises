using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject startButton, restartButton, gameOverButton;

    private void Start()
    {
        Initialize();
    }

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

    public void OnRestartPressed()
    {
        Initialize();
    }
}
