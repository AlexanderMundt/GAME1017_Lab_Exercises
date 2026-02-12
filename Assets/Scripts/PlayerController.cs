/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 startPosition;

    private void Start()
    {
        //Save the player's starting position
        startPosition = transform.position;
    }

    void Update()
    {
        //When we are InPlay...
        if (GameManager.Instance.GetGameState() == EGameState.InPlay)
        {
            //...move right
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(distancePerFrame, 0, 0);
    }

    public void ResetPlayer()
    {
        //Load the player's starting position
        transform.position = startPosition;
    }
}
