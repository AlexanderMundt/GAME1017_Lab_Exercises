/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 3
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
        ////Destroying the game manager here makes sure that we create a
        ////new fresh one when we need it in the actual game scene
        //Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(sceneToLoad);
    }
}
