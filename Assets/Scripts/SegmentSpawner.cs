/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 5
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] segmentPrefabs;
    [SerializeField] private float maxDistanceFromPlayer;

    [SerializeField] private List<GameObject> segments = new();
    [SerializeField] private int segmentListSize;

    [SerializeField] private GameObject lastSegment, currentSegment;
    [SerializeField] private Renderer lastRenderer, currentRenderer;

    [SerializeField] private PlayerController player;

    [Tooltip("Represents the min (x) and max (y) distance that segments can spawn from each other")]
    [SerializeField] private Vector2 gapRange;
    [SerializeField] private Vector2 heightRange;

    private int lastIndex;

    public void Initialize()
    {
        player = GameManager.Instance.Player;

        //Segment 1
        lastSegment = Instantiate(segmentPrefabs[0], new Vector3(player.transform.position.x, player.transform.position.y - 1, 0), Quaternion.identity, transform);
        lastRenderer = lastSegment.GetComponent<Renderer>();
        segments.Add(lastSegment);

        //Segment 2
        currentSegment = Instantiate(segmentPrefabs[1], new Vector3(player.transform.position.x + gapRange.x, player.transform.position.y - 1, 0), Quaternion.identity, transform);
        currentRenderer = currentSegment.GetComponent<Renderer>();
        segments.Add(currentSegment);

        float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapRange.x;
        currentSegment.transform.position = new Vector3(xSpawnPos, player.transform.position.y - 1, 0);

        lastSegment = currentSegment;
        lastRenderer = currentRenderer;

        lastIndex = 1;
    }

    private void Update()
    {
        if (lastRenderer == null || player == null)
        {
            return;
        }

        if (lastRenderer.bounds.max.x < player.transform.position.x + maxDistanceFromPlayer)
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        //Randomize the gap size
        float gapSize = Random.Range(gapRange.x, gapRange.y);

        //Randomize the height
        float heightOffset = Random.Range(heightRange.x, heightRange.y);

        List<int> possibleIndices = new();

        //Obstacle [2] and [3] cannot be beside each other
        if (lastIndex == 2 || lastIndex == 3)
        {
            possibleIndices.Add(0);
            possibleIndices.Add(1);
        }
        else
        {
            for (int i = 0; i < segmentPrefabs.Length; i++)
            {
                //Same object cannot spawn twice in a row
                if (lastIndex == i) continue;

                possibleIndices.Add(i);
            }
        }

        int index = possibleIndices[Random.Range(0, possibleIndices.Count)];

        currentSegment = Instantiate(segmentPrefabs[index], transform);
        currentRenderer = currentSegment.GetComponent<Renderer>();

        float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
        currentSegment.transform.position = new Vector3(xSpawnPos, heightOffset, 0);

        segments.Add(currentSegment);

        if (segments.Count > segmentListSize)
        {
            Destroy(segments[0]);
            segments.RemoveAt(0);
        }

        lastSegment = currentSegment;
        lastRenderer = currentRenderer;

        lastIndex = index;
    }

    //Reset segments
    public void ResetSegments()
    {
        lastSegment = null;
        lastRenderer = null;

        lastIndex = 0;

        currentSegment = null;
        currentRenderer = null;

        foreach (GameObject seg in segments)
        {
            Destroy(seg);
        }

        segments.Clear();
    }
}
