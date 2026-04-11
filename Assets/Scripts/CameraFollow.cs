/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using Unity.VisualScripting;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private float xOffset, fixedY, fixedZ;

    private void Start()
    {
        xOffset = transform.position.x;
        fixedY = transform.position.y;
        fixedZ = transform.position.z;

        if (player == null)
        {
            player = GameObject.FindFirstObjectByType(typeof(PlayerController)).GameObject();
        }
    }

    private void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        float newXPosition = player.transform.position.x + xOffset;

        transform.position = new Vector3(newXPosition, fixedY, fixedZ);
    }
}
