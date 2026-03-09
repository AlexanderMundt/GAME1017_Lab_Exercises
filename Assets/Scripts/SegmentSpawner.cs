/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using System.Collections.Generic;

public class SegmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private float maxDistanceFromPlayer;
    [SerializeField] private float gapSize = 0.5f;

    //Object pool
    [SerializeField] private List<GameObject> segments = new();
    [SerializeField] private int segmentPoolSize;

    [SerializeField] private GameObject lastSegment, currentSegment;
    [SerializeField] private Renderer lastRenderer, currentRenderer;

    [SerializeField] private GameObject player;

    private void Start()
    {
        if (!player) player = GameManager.Instance.Player.gameObject;

        GameObject go;
        for (int i = 0; i < segmentPoolSize; i++)
        {
            go = Instantiate(segmentPrefab, this.transform);
            go.SetActive(false);
            segments.Add(go);
        }
    }

    public void Initialize()
    {
        //Set up variables and spawn in the first platform the player will start on
        lastSegment = GetNextObject();
        lastSegment.transform.position = new Vector3(0f, player.transform.position.y - 1, 0f);
        lastRenderer = lastSegment.GetComponent<Renderer>();

        //Set up variables and spawn in the second platform
        currentSegment = GetNextObject();
        currentRenderer = currentSegment.GetComponent<Renderer>();

        float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
        currentSegment.transform.position = new Vector3(xSpawnPos, player.transform.position.y - 1, 0f);

        //"Shift over" the references so that our most recently spawned platform is our 'lastSegment'
        lastSegment = currentSegment;
        lastRenderer = currentRenderer;
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

    //GetNext
    private GameObject GetNextObject()
    {
        foreach (GameObject seg in segments)
        {
            if (!seg.activeSelf)
            {
                seg.SetActive(true);
                return seg;
            }
        }

        return null;
    }

    private bool IsNextObjectAvailable()
    {
        foreach (GameObject seg in segments)
        {
            if (!seg.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    //Return to pool
    private void ReturnToPool(GameObject seg)
    {
        //Gameobject = inactive
        seg.SetActive(false);
    }

    //Return all to pool
    private void ReturnAllToPool()
    {
        foreach (GameObject seg in segments)
        {
            ReturnToPool(seg);
        }
    }

    //Reset segments
    public void ResetSegments()
    {
        //"Turn off" all of the segments
        ReturnAllToPool();

        //Reset the position of all of the segments
        foreach (GameObject seg in segments)
        {
            seg.transform.position = transform.position;
        }

        lastSegment = null;
        lastRenderer = null;

        currentSegment = null;
        currentRenderer = null;
    }

    private void SpawnPlatform()
    {
        //Make sure that there is a new segment to be spawned up ahead
        if (!IsNextObjectAvailable())
        {
            DespawnFurthestPlatform();
        }

        //Randomize the gap size
        gapSize = Random.Range(0.5f, 1.5f);

        //Randomize the height
        float heightOffset = Random.Range(-1.2f, 1.2f);

        currentSegment = GetNextObject();
        currentRenderer = currentSegment.GetComponent<Renderer>();

        float xSpawnPos = lastRenderer.bounds.max.x + (currentRenderer.bounds.size.x / 2) + gapSize;
        currentSegment.transform.position = new Vector3(xSpawnPos, lastSegment.transform.position.y + heightOffset, 0);

        lastSegment = currentSegment;
        lastRenderer = currentRenderer;
    }

    private void DespawnFurthestPlatform()
    {
        float prevDist = 0.0f;
        float dist = 0.0f;
        GameObject farthestBehindSegment = null;

        foreach (GameObject seg in segments)
        {
            dist = Vector3.Distance(seg.transform.position, player.transform.position);

            if (dist > prevDist)
            {
                prevDist = dist;
                farthestBehindSegment = seg;
            }
        }

        ReturnToPool(farthestBehindSegment);
    }
}
