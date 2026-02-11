using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : Singleton<BackgroundManager>
{
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] private float xBuffer = 3f;

    private Transform lastBackground;
    private Renderer lastRenderer;
    private float backgroundWidth;

    private float nextSpawnAtCamRightX; // world X where camera-right must reach to spawn again

    //Object pool
    private List<GameObject> backgrounds = new();
    private const int OBJECT_POOL_SIZE = 3;

    private void Start()
    {
        if (!cam) cam = Camera.main;

        GameObject go;
        for (int i = 0; i < OBJECT_POOL_SIZE; i++)
        {
            go = Instantiate(backgroundPrefab, this.transform);
            go.SetActive(false);
            backgrounds.Add(go);
        }

        lastBackground = GetNextObject().transform;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        backgroundWidth = lastRenderer.bounds.size.x;
        lastRenderer.sortingOrder = 0;

        // Set first trigger based on the first background's right edge
        UpdateNextSpawnTrigger();
    }

    private void Update()
    {
        float halfCamWidth = cam.orthographicSize * cam.aspect;
        float camRightEdge = cam.transform.position.x + halfCamWidth;

        if (camRightEdge >= nextSpawnAtCamRightX)
        {
            SpawnNextToRight();
            UpdateNextSpawnTrigger();
        }
    }

    //GetNext
    private GameObject GetNextObject()
    {
        foreach (GameObject background in backgrounds)
        {
            if (!background.activeSelf)
            {
                background.SetActive(true);
                return background;
            }
        }

        return null;
    }

    //Return to pool
    private void ReturnToPool(GameObject background)
    {
        //Gameobject = inactive
        background.SetActive(false);
    }

    //Return all to pool
    private void ReturnAllToPool()
    {
        foreach (GameObject background in backgrounds)
        {
            ReturnToPool(background);
        }
    }

    //Reset backgrounds
    public void ResetBackground()
    {
        //"Turn off" all of the backgrounds
        ReturnAllToPool();

        //Reset the position of all of the backgrounds
        foreach (GameObject background in backgrounds)
        {
            background.transform.position = transform.position;
        }

        //Update the last background and renderer to the new(original) starting position
        lastBackground = GetNextObject().transform;
        lastRenderer = lastBackground.GetComponent<Renderer>();

        UpdateNextSpawnTrigger();
    }

    private void SpawnNextToRight()
    {
        Vector3 spawnPos = lastBackground.position;
        spawnPos.x += backgroundWidth;

        if (GetNextObject() == null)
        {
            float prevDist = 0.0f;
            float dist = 0.0f;
            GameObject objectToReturn = null;

            foreach (GameObject bg in backgrounds)
            {
                dist = Vector3.Distance(bg.transform.position, cam.transform.position);

                if (dist > prevDist)
                {
                    prevDist = dist;
                    objectToReturn = bg;
                }
            }

            ReturnToPool(objectToReturn);
        }

        //lastBackground = Instantiate(backgroundPrefab, spawnPos, Quaternion.identity, transform).transform;
        lastBackground = GetNextObject().transform;
        lastBackground.position = spawnPos;
        lastRenderer = lastBackground.GetComponent<Renderer>();
        lastRenderer.sortingOrder = 0;
    }

    private void UpdateNextSpawnTrigger()
    {
        // Spawn again only after camera reaches the (new) last background's right edge
        nextSpawnAtCamRightX = lastRenderer.bounds.max.x - xBuffer;
    }
}
