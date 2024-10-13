using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;
public class Spawning : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int poolSize;
    }

    public int groupSizeMax;
    public int groupSizeMin;

    public List<Pool> pools;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera vcam;

    [HideInInspector]
    public GameObject playerActor;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    public static Spawning Instance;

    //NavMeshHit hit;

    /*private void Awake()
    {
        Instance = this;
    }*/

    //public GameObject playerSpawner;
    //public GameObject enemySpawner;

    public GameObject enemySpawnZone;
    //private BoxCollider2D bxCol;
    //private Vector2 spawnMin;
    //private Vector2 spawnMax;

    //public GameObject playerCharacter;
    //public GameObject enemy;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        playerActor = Instantiate(playerPrefab);
        playerActor.SetActive(false);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject parent = new();
            parent.name = pool.tag + "s";

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);   
        }
        //playerSpawner = GameObject.Find("PlayerSpawner");
        //enemySpawner = GameObject.Find("enemySpawner");
        //enemySpawnZone = GameObject.Find("enemySpawnZone");
        /*bxCol = enemySpawnZone.GetComponent<BoxCollider2D>();
        Vector2 spawnMin = bxCol.bounds.min;
        Vector2 spawnMax = bxCol.bounds.max;

        Instantiate(playerCharacter, playerSpawner.transform.position, playerSpawner.transform.rotation);
        Instantiate(enemy, enemySpawner.transform.position, enemySpawner.transform.rotation);

        Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));
        Instantiate(enemy, new Vector3(randomPos.x, randomPos.y, 0), Quaternion.identity);*/

        //actorSpawner(playerCharacter, playerSpawner);
        //actorSpawner(enemy, enemySpawner);
        //actorSpawnerZone(enemy, enemySpawnZone, 5);
        print("SPAWN AWAKE");
    }

    public GameObject spawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objToSpawn);
        print("SPAWN POOL");
        return objToSpawn;
        //Instantiate(actor, spawner.transform.position, spawner.transform.rotation);
        
    }

    public void spawnPlayer(Vector3 position, Quaternion rotation)
    {
        playerActor.transform.position = position;
        playerActor.transform.rotation = rotation;
        playerActor.SetActive(true);
        vcam.Follow = playerActor.transform;
    }

    public void actorSpawnerZone(string tag, GameObject spawnZone, int actorCount)
    {
        BoxCollider2D bxCol = spawnZone.GetComponent<BoxCollider2D>();
        Vector2 spawnMin = bxCol.bounds.min;
        Vector2 spawnMax = bxCol.bounds.max;

        for(int i = 1; i <= actorCount; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(spawnMin.x, spawnMax.x), Random.Range(spawnMin.y, spawnMax.y));

            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
                //return null;
            }

            GameObject objToSpawn = poolDictionary[tag].Dequeue();
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = randomPos;
            objToSpawn.transform.rotation = Quaternion.identity;

            poolDictionary[tag].Enqueue(objToSpawn);

            //return objToSpawn;

            //Instantiate(actor, new Vector3(randomPos.x, randomPos.y, 0), Quaternion.identity);
        }
        //return null;
    }

    public void createEnemySpawnZone(Vector3 pos)
    {
        GameObject spawnZone = Instantiate(enemySpawnZone, pos, Quaternion.identity);

        int groupSize = Random.Range(groupSizeMin, groupSizeMax);
        //Debug.Log(groupSize);
        actorSpawnerZone("Slime", spawnZone, groupSize);
    }
}
