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
