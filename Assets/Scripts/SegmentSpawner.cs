/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] segmentPrefabs;
    [SerializeField] private float maxDistanceFromPlayer;

    //Object pool <--- not anymore in LE4
    [SerializeField] private List<GameObject> segments = new();
    [SerializeField] private int segmentListSize;

    [SerializeField] private GameObject lastSegment, currentSegment;
    [SerializeField] private Renderer lastRenderer, currentRenderer;

    [SerializeField] private GameObject player;

    [Tooltip("Represents the min (x) and max (y) distance that segments can spawn from each other")]
    [SerializeField] private Vector2 gapRange;
    [SerializeField] private Vector2 heightRange;

    private int lastIndex;

    public void Initialize()
    {
        player = GameManager.Instance.Player.gameObject;

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
        //"Turn off" all of the segments
        //ReturnAllToPool();

        //Reset the position of all of the segments
        //foreach (GameObject seg in segments)
        //{
        //    seg.transform.position = transform.position;
        //}

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

    //----Old and unused, saving for later if needed---
    //private void Start()
    //{
    //    if (!player) player = GameManager.Instance.Player.gameObject;

    //    GameObject go;
    //    for (int i = 0; i < segmentPoolSize; i++)
    //    {
    //        go = Instantiate(segmentPrefab, this.transform);
    //        go.SetActive(false);
    //        segments.Add(go);
    //    }
    //}

    //---Old object pool way---
    //public void Initialize()
    //{
    //    //Set up variables and spawn in the first platform the player will start on
    //    lastSegment = GetNextObject();
    //    lastSegment.transform.position = new Vector3(0f, player.transform.position.y - 1, 0f);
    //    lastRenderer = lastSegment.GetComponent<Renderer>();

    //    //Set up variables and spawn in the second platform
    //    currentSegment = GetNextObject();
    //    currentRenderer = currentSegment.GetComponent<Renderer>();

    //    float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
    //    currentSegment.transform.position = new Vector3(xSpawnPos, player.transform.position.y - 1, 0f);

    //    //"Shift over" the references so that our most recently spawned platform is our 'lastSegment'
    //    lastSegment = currentSegment;
    //    lastRenderer = currentRenderer;
    //}

    //GetNext
    //private GameObject GetNextObject()
    //{
    //    foreach (GameObject seg in segments)
    //    {
    //        if (!seg.activeSelf)
    //        {
    //            seg.SetActive(true);
    //            return seg;
    //        }
    //    }

    //    return null;
    //}

    //private bool IsNextObjectAvailable()
    //{
    //    foreach (GameObject seg in segments)
    //    {
    //        if (!seg.activeSelf)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    //Return to pool
    //private void ReturnToPool(GameObject seg)
    //{
    //    //Gameobject = inactive
    //    seg.SetActive(false);
    //}

    //Return all to pool
    //private void ReturnAllToPool()
    //{
    //    foreach (GameObject seg in segments)
    //    {
    //        ReturnToPool(seg);
    //    }
    //}

    //---Old object pool way---
    //private void SpawnPlatform()
    //{
    //    //Make sure that there is a new segment to be spawned up ahead
    //    //if (!IsNextObjectAvailable())
    //    //{
    //    //    DespawnFurthestPlatform();
    //    //}

    //    //Randomize the gap size
    //    gapSize = Random.Range(0.5f, 1.5f);

    //    //Randomize the height
    //    float heightOffset = Random.Range(-1.2f, 1.2f);

    //    currentSegment = GetNextObject();
    //    currentRenderer = currentSegment.GetComponent<Renderer>();

    //    float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
    //    currentSegment.transform.position = new Vector3(xSpawnPos, lastSegment.transform.position.y + heightOffset, 0);

    //    lastSegment = currentSegment;
    //    lastRenderer = currentRenderer;
    //}

    //private void DespawnFurthestPlatform()
    //{
    //    float prevDist = 0.0f;
    //    float dist = 0.0f;
    //    GameObject farthestBehindSegment = null;

    //    foreach (GameObject seg in segments)
    //    {
    //        dist = Vector3.Distance(seg.transform.position, player.transform.position);

    //        if (dist > prevDist)
    //        {
    //            prevDist = dist;
    //            farthestBehindSegment = seg;
    //        }
    //    }

    //    ReturnToPool(farthestBehindSegment);
    //}
}
