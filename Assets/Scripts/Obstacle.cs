/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 4
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If an obstacle collides with the player...
        if (other.GetComponent<PlayerController>())
        {
            //...trigger gameover
            GameManager.Instance.GameOver();
        }
    }
}
