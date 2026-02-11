using UnityEngine;
using UnityEngine.UI;

public class GameManagerButton : MonoBehaviour
{
    [SerializeField] private EGameManagerAction gameState;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(PerformGameManagerAction);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(PerformGameManagerAction);
    }

    private void PerformGameManagerAction()
    {
        switch (gameState)
        {
            case EGameManagerAction.Play:
                GameManager.Instance.PlayGame();
                break;

            case EGameManagerAction.GameOver:
                GameManager.Instance.GameOver();
                break;

            case EGameManagerAction.Restart:
                GameManager.Instance.RestartGame();
                break;
        }
    }
}
