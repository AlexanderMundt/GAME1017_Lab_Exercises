/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnTitleButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(ReturnToTitle);
    }

    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(ReturnToTitle);
    }

    private void ReturnToTitle()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
