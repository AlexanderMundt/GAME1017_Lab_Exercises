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
            float distancePerFrame = speed * Time.deltaTime;
            transform.Translate(distancePerFrame, 0, 0);
        }
    }

    public void ResetPlayer()
    {
        //Load the player's starting position
        transform.position = startPosition;
    }
}
