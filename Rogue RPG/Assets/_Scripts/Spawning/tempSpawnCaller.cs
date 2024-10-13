using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSpawnCaller : MonoBehaviour
{

    public Spawning spawningScript;

    // Start is called before the first frame update
    void Start()
    {

        //Spawning.Instance.spawnPlayer(Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //Spawning.Instance.spawnFromPool("Slime", transform.position, Quaternion.identity);
    }
}
