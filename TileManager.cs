using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    private Transform playerTransform;

    //where first tile appears
    private float spawnZ = -5.0f;

    //this made need adjusting to the size of the model 
    private float tileLength = 10.0f;

    //tiles on screen 
    private int amnTilesOnScreen = 10;

    //last prefab on screen
    private int lastPrefabIndex = 0; 

    //Safezone
    private float safeZone = 15.0f; 


    private List<GameObject> activeTiles; 


	// Use this for initialization
	private void Start ()
    {
        activeTiles = new List<GameObject>(); 

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
            SpawnTile();
        }
    }
	
	// Update is called once per frame
	private void Update ()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
	}

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject; 
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go); 
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);

    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {

            randomIndex = Random.Range(0, tilePrefabs.Length); 
        }

        lastPrefabIndex = randomIndex;
        return randomIndex; 


    }

}
